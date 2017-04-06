using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Sintoacct.Ledger.Models;
using AutoMapper;
using System.Data.Entity;

namespace Sintoacct.Ledger.Services
{
    public class VoucherHelper : IVoucherHelper
    {
        private readonly LedgerContext _ledger;
        private readonly ICacheHelper _cache;
        private readonly IAccountBookHelper _acctBook;
        private readonly HttpContextBase _context;
        private readonly IAccountHelper _account;

        public VoucherHelper(LedgerContext ledger,
                             ICacheHelper cache,
                             IAccountBookHelper acctBook,
                             HttpContextBase context,
                             IAccountHelper account)
        {
            _ledger = ledger;
            _cache = cache;
            _acctBook = acctBook;
            _context = context;
            _account = account;
        }

        public Voucher GetMyVoucher(long vid)
        {
            Guid abid = _cache.GetUserCache().AccountBookID;

            return _ledger.Vouchers.Where(v => v.AbId == abid && v.VId == vid)
                                   .Include(v => v.VoucherDetails)
                                   .Include(v => v.CertificateWord)
                                   .FirstOrDefault();
        }

        public List<Voucher> GetMyUnauditVouchers(int pageSize)
        {
            Guid abid = _cache.GetUserCache().AccountBookID;

            //仅返回最新的pageSize个未审核的凭证
            return _ledger.Vouchers.Where(v => v.AbId == abid && v.State == VoucherState.PaddingAudit)
                                   .OrderByDescending(v => v.VId)
                                   .Include(v => v.VoucherDetails)
                                   .Include(v => v.CertificateWord)
                                   .Take(pageSize)
                                   .ToList();
        }

        public Voucher Save(VoucherViewModel vmVoucher)
        {
            Voucher voucher = new Voucher();
            if (vmVoucher.VId > 0)
            {
                voucher = this.GetMyVoucher(vmVoucher.VId);
                if (voucher == null) return null;

                voucher.CertificateWord = _ledger.CertificateWords.Where(cw => cw.CwId == vmVoucher.CwId).FirstOrDefault();
                voucher.CertWordSN = vmVoucher.CertWordSN;
                voucher.VoucherDate = vmVoucher.VoucherDate;
                voucher.PaymentTerms = string.Format("{0}年第{1}期", voucher.VoucherDate.Year, voucher.VoucherDate.Month);
                voucher.InvoiceCount = vmVoucher.InvoiceCount;

                foreach (VoucherDetailViewModel vd in vmVoucher.VoucherDetails)
                {
                    VoucherDetail vDetail = new VoucherDetail();

                    if (vd.VdId > 0) vDetail = voucher.VoucherDetails.Where(d => d.VdId == vd.VdId).FirstOrDefault();
                    else
                        voucher.VoucherDetails.Add(vDetail);

                    vDetail.Abstract = vd.Abstract;
                    vDetail.Account = _account.GetAccount(vd.AccId);
                    vDetail.AccountCode = vDetail.Account.AccCode;
                    vDetail.AccountName = vDetail.Account.AccName;
                    vDetail.Quantity = vd.Quantity;
                    vDetail.Price = vd.Price;
                    vDetail.Debit = vd.Debit;
                    vDetail.Credit = vd.Credit;
                }
            }
            else
            {
                voucher.CertificateWord = _ledger.CertificateWords.Where(cw => cw.CwId == vmVoucher.CwId).FirstOrDefault();
                voucher.CertWordSN = vmVoucher.CertWordSN;
                voucher.VoucherDate = vmVoucher.VoucherDate;
                voucher.PaymentTerms = string.Format("{0}年第{1}期", voucher.VoucherDate.Year, voucher.VoucherDate.Month);
                voucher.InvoiceCount = vmVoucher.InvoiceCount;
                voucher.State = VoucherState.PaddingAudit;
                voucher.AccountBook = _acctBook.GetCurrentBook();
                voucher.Creator = ((ClaimsIdentity)_context.User.Identity).GetUserName();
                voucher.CreateTime = DateTime.Now;
                voucher.VoucherDetails = new List<VoucherDetail>();

                foreach (VoucherDetailViewModel vd in vmVoucher.VoucherDetails)
                {
                    VoucherDetail vDetail = new VoucherDetail();
                    vDetail.Abstract = vd.Abstract;
                    vDetail.Account = _account.GetAccount(vd.AccId);
                    vDetail.AccountCode = vDetail.Account.AccCode;
                    vDetail.AccountName = vDetail.Account.AccName;
                    vDetail.Quantity = vd.Quantity;
                    vDetail.Price = vd.Price;
                    vDetail.Debit = vd.Debit;
                    vDetail.Credit = vd.Credit;

                    voucher.VoucherDetails.Add(vDetail);
                }

                _ledger.Vouchers.Add(voucher);
            }

            if (_ledger.SaveChanges() > 0)
            {
                return voucher;
            }

            return null;
        } 

        public void Delete(long vid)
        {
            Voucher delVoucher = GetMyVoucher(vid);

            if (delVoucher == null) throw new ArgumentNullException("找不到要删除的凭证");

            if (delVoucher.State == VoucherState.Audited) throw new InvalidOperationException("凭证已审核，若要删除请先弃审");

            _ledger.Vouchers.Remove(delVoucher);
        }

        public void Audit(long vid)
        {
            Voucher voucher = GetMyVoucher(vid);
            if (voucher.State != VoucherState.PaddingAudit) throw new InvalidOperationException("凭证状态不能被审核");

            decimal debit = 0, credit = 0;
            debit = voucher.VoucherDetails.Sum(vd => vd.Debit);
            credit = voucher.VoucherDetails.Sum(vd => vd.Credit);
            if (debit != credit) throw new InvalidOperationException(string.Format("借方（{0}）贷方（{1}）不平！", debit, credit));

            voucher.State = VoucherState.Audited;
            _ledger.SaveChanges();
        }

        public VoucherViewModel CopyNew(long vid)
        {
            Voucher voucher = GetMyVoucher(vid);

            VoucherViewModel vmVoucher = Mapper.Map<VoucherViewModel>(voucher);
            vmVoucher.VId = 0;
            foreach(VoucherDetail vd in voucher.VoucherDetails)
            {
                VoucherDetailViewModel vmDetail = Mapper.Map<VoucherDetailViewModel>(vd);
                vmDetail.VdId = 0;
                vmVoucher.VoucherDetails.Add(vmDetail);
            }

            return vmVoucher;
        }

        public AbstractTemp SaveAbstract(AbstractViewModel abs)
        {
            AbstractTemp absTemp = new AbstractTemp();
            if(abs.AbsId>0)
            {
                absTemp = _ledger.AbstractTemps.Where(ab => ab.AbsId == abs.AbsId).FirstOrDefault();
            }
            else
            {
                absTemp.AbId = _cache.GetUserCache().AccountBookID;
                _ledger.AbstractTemps.Add(absTemp);
            }
            absTemp.Abstract = abs.Abstract;
            
            if(_ledger.SaveChanges()>0)
            {
                return absTemp;
            }

            return null;
        }

        public List<AbstractTemp> GetMyAbstracts()
        {
            Guid abid = _cache.GetUserCache().AccountBookID;

            return _ledger.AbstractTemps.Where(ab => ab.AbId == abid).ToList();
        }

        public void DeleteAbstract(int absId)
        {
            var absTemp = _ledger.AbstractTemps.Where(abs => abs.AbsId == absId).FirstOrDefault();

            _ledger.AbstractTemps.Remove(absTemp);

            _ledger.SaveChanges();
        }

        public List<Voucher> SearchVoucher(SearchConditionViewModel condition)
        {
            Guid abid = _cache.GetUserCache().AccountBookID;

            //生成可查询的凭证集合
            var vouchers = _ledger.Vouchers.Where(v => v.AbId == abid);

            if (!string.IsNullOrEmpty(condition.StartPeriod))
            {
                vouchers = vouchers.Where(v => v.PaymentTerms.CompareTo(condition.StartPeriod) > 0);
            }
            if (!string.IsNullOrEmpty(condition.EndPeriod))
            {
                vouchers = vouchers.Where(v => v.PaymentTerms.CompareTo(condition.EndPeriod) < 0);
            }

            if (!string.IsNullOrEmpty(condition.CertWord))
            {
                vouchers = vouchers.Where(v => v.CertificateWord.CertWord == condition.CertWord);
            }

            vouchers = vouchers.OrderByDescending(v => v.VId)
                                .Include(v => v.VoucherDetails)
                                .Include(v => v.CertificateWord)
                                .Include("VoucherDetails.Account");

            return vouchers.ToList();
        }

        public List<SearchVoucherViewModel> VoucherToSearchVoucherViewModel(List<Voucher> vouchers)
        {
            List<SearchVoucherViewModel> searchVouchers = new List<SearchVoucherViewModel>();
            int i = 0, j = 0;
            foreach (Voucher v in vouchers)
            {
                j = i;
                foreach(VoucherDetail vd in v.VoucherDetails)
                {
                    SearchVoucherViewModel sv = new SearchVoucherViewModel();
                    sv.VId = v.VId;
                    sv.VoucherDate = v.VoucherDate;
                    sv.CertWord = string.Format("{0}-{1}", v.CertificateWord.CertWord, v.CertWordSN);
                    sv.Abstract = vd.Abstract;
                    sv.Account = string.Format("{0}  {1}", vd.Account.AccCode, vd.Account.AccName); 
                    sv.Debit = vd.Debit;
                    sv.Credit = vd.Credit;
                    sv.Creator = v.Creator;
                    sv.Review = v.Review;
                    sv.MergeIndex = j;
                    searchVouchers.Add(sv);
                    i++;
                }
                searchVouchers[j].RowSpan = i - j;
            }

            return searchVouchers;
        }
    }

    public interface IVoucherHelper : IDependency
    {
        Voucher GetMyVoucher(long vid);

        List<Voucher> GetMyUnauditVouchers(int pageSize);

        Voucher Save(VoucherViewModel vmVoucher);

        void Delete(long vid);

        void Audit(long vid);

        VoucherViewModel CopyNew(long vid);

        AbstractTemp SaveAbstract(AbstractViewModel abs);

        List<AbstractTemp> GetMyAbstracts();

        void DeleteAbstract(int absId);

        List<Voucher> SearchVoucher(SearchConditionViewModel condition);

        List<SearchVoucherViewModel> VoucherToSearchVoucherViewModel(List<Voucher> vouchers);
    }
}