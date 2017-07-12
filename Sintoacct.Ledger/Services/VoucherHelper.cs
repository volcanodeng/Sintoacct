using AutoMapper;
using Microsoft.AspNet.Identity;
using Sintoacct.Ledger.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;

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

        /// <summary>
        /// 重算所有科目的期初余额、累计金额、累计数量和年初余额。
        /// </summary>
        /// <returns>重算的科目数量</returns>
        private int RecalculateAllAccount()
        {
            Guid abid = _cache.GetUserCache().AccountBookID;
            
            var vouchers = _ledger.Vouchers.Where(v => v.AbId == abid && v.VoucherYear == DateTime.Now.Year)
                                   .Include(v => v.VoucherDetails)
                                   .Include("VoucherDetails.Account")
                                   .OrderBy(v=>v.VoucherYear).ThenBy(v=>v.VoucherMonth)
                                   .ToList();

            int period = 0,c=0;
            List<long> accids = new List<long>(); 
            foreach(Voucher v in vouchers)
            {
                foreach(VoucherDetail vd in v.VoucherDetails)
                {
                    if(!accids.Contains(vd.Account.AccId))
                    {
                        vd.Account.YtdCredit = 0;
                        vd.Account.YtdDebit = 0;
                        accids.Add(vd.Account.AccId);
                    }

                    vd.InitialBalance = vd.Account.InitialBalance;
                    vd.InitialQuantity = vd.Account.InitialQuantity;
                    if (vd.Account.Direction == "借")
                    {
                        vd.YtdDebit = vd.Account.YtdDebit + vd.Debit;
                        vd.Account.YtdDebit = vd.YtdDebit;

                        vd.YtdCredit = vd.Account.YtdCredit + vd.YtdCredit * (-1);
                        vd.Account.YtdCredit = vd.YtdCredit;
                    }
                    else
                    {
                        vd.YtdDebit = vd.Account.YtdDebit + vd.Debit * (-1);
                        vd.Account.YtdDebit = vd.YtdDebit;

                        vd.YtdCredit = vd.Account.YtdCredit + vd.Credit;
                        vd.Account.YtdCredit = vd.YtdCredit;
                    }

                    c++;
                }

                if (period != v.VoucherMonth) period = v.VoucherMonth;
            }

            _ledger.SaveChanges();

            return c;
        }

        //private async Task<int> Recalculate()
        //{
        //    return await Task.Run(()=> {
        //        return RecalculateAllAccount();
        //    });
        //}

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
                                   .Include(v => v.Invoices)
                                   .Include("VoucherDetails.Account")
                                   .Take(pageSize)
                                   .ToList();
        }

        public Voucher Save(VoucherViewModel vmVoucher)
        {
            Voucher voucher = new Voucher();
            if (vmVoucher.VId > 0)
            {
                //修改凭证
                voucher = this.GetMyVoucher(vmVoucher.VId);
                if (voucher == null) return null;

                voucher.CertificateWord = _ledger.CertificateWords.Where(cw => cw.CwId == vmVoucher.CwId).FirstOrDefault();
                voucher.CertWordSN = vmVoucher.CertWordSN;
                voucher.VoucherYear = vmVoucher.VoucherDate.Year;
                voucher.VoucherMonth = vmVoucher.VoucherDate.Month;
                voucher.PaymentTerms = string.Format("{0}{1:D2}", voucher.VoucherYear, voucher.VoucherMonth);
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
                    if (vDetail.Account.Direction == "借")
                    {
                        vDetail.Debit = vd.Debit;
                        vDetail.Credit = vd.Credit * (-1);
                    }
                    else
                    {
                        vDetail.Debit = vd.Debit * (-1);
                        vDetail.Credit = vd.Credit;
                    }

                    vDetail.InitialBalance = vDetail.Account.InitialBalance;
                    vDetail.InitialQuantity = vDetail.Account.InitialQuantity;
                    vDetail.YtdBeginBalance = vDetail.Account.YtdBeginBalance;
                    vDetail.YtdBeginBalanceQuantity = vDetail.Account.YtdBeginBalanceQuantity;
                    if (vDetail.Account.Direction == "借")
                    {
                        vDetail.YtdDebit = vDetail.Account.YtdDebit + vDetail.Debit;
                        vDetail.YtdDebitQuantity = vDetail.Account.YtdDebitQuantity + vDetail.Quantity;
                    }
                    else
                    {
                        vDetail.YtdCredit = vDetail.Account.YtdCredit + vDetail.Credit;
                        vDetail.YtdCreditQuantity = vDetail.Account.YtdCreditQuantity + vDetail.Quantity;
                    }

                }
            }
            else
            {
                //新增凭证
                voucher.CertificateWord = _ledger.CertificateWords.Where(cw => cw.CwId == vmVoucher.CwId).FirstOrDefault();
                voucher.CertWordSN = vmVoucher.CertWordSN;
                voucher.VoucherYear = vmVoucher.VoucherDate.Year;
                voucher.VoucherMonth = vmVoucher.VoucherDate.Month;
                voucher.PaymentTerms = string.Format("{0}{1:D2}", voucher.VoucherYear, voucher.VoucherMonth);
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
                    //vDetail.Debit = vd.Debit;
                    //vDetail.Credit = vd.Credit;
                    if (vDetail.Account.Direction == "借")
                    {
                        vDetail.Debit = vd.Debit;
                        vDetail.Credit = vd.Credit * (-1);
                    }
                    else
                    {
                        vDetail.Debit = vd.Debit * (-1);
                        vDetail.Credit = vd.Credit;
                    }

                    vDetail.InitialBalance = vDetail.Account.InitialBalance;
                    vDetail.InitialQuantity = vDetail.Account.InitialQuantity;
                    vDetail.YtdBeginBalance = vDetail.Account.YtdBeginBalance;
                    vDetail.YtdBeginBalanceQuantity = vDetail.Account.YtdBeginBalanceQuantity;
                    if (vDetail.Account.Direction == "借")
                    {
                        vDetail.YtdDebit = vDetail.Account.YtdDebit + vDetail.Debit;
                        vDetail.YtdDebitQuantity = vDetail.Account.YtdDebitQuantity + vDetail.Quantity;
                    }
                    else
                    {
                        vDetail.YtdCredit = vDetail.Account.YtdCredit + vDetail.Credit;
                        vDetail.YtdCreditQuantity = vDetail.Account.YtdCreditQuantity + vDetail.Quantity;
                    }

                    voucher.VoucherDetails.Add(vDetail);
                }

                _ledger.Vouchers.Add(voucher);
            }

            if (_ledger.SaveChanges() > 0)
            {
                //科目统计数据
                this.RecalculateAllAccount();

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
                vouchers = vouchers.Where(v => v.PaymentTerms.CompareTo(condition.StartPeriod) >= 0);
            }
            if (!string.IsNullOrEmpty(condition.EndPeriod))
            {
                vouchers = vouchers.Where(v => v.PaymentTerms.CompareTo(condition.EndPeriod) <= 0);
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
                    sv.VoucherDate = v.CreateTime;
                    sv.CertWord = string.Format("{0}-{1}", v.CertificateWord.CertWord, v.CertWordSN);
                    sv.Abstract = vd.Abstract;
                    sv.Account = string.Format("{0}  {1}", vd.Account.AccCode, vd.Account.AccName); 
                    sv.Debit = vd.Debit;
                    sv.Credit = vd.Credit;
                    sv.Creator = v.Creator;
                    sv.Review = string.IsNullOrEmpty(v.Review) ? "<未审核>" : v.Review;
                    sv.MergeIndex = j;
                    searchVouchers.Add(sv);
                    i++;
                }
                searchVouchers[j].RowSpan = i - j;
            }

            return searchVouchers;
        }

        public void SetInvoicePath(long vid,string path)
        {
            Voucher voucher = this.GetMyVoucher(vid);
            if (voucher != null && File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                FileInfo fi = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(path));

                SourceDocument invoice = _ledger.SourceDocument.Where(sd => sd.SourceFileName == fi.FullName).FirstOrDefault();
                if (invoice != null) return;

                invoice = new SourceDocument();
                invoice.SourceFileName = fi.FullName;
                invoice.RelateFileName = path;
                invoice.RelatePath = path.Replace(fi.Name, "");
                invoice.FileSize = fi.Length;
                voucher.Invoices.Add(invoice);

                voucher.InvoicePath = path.Replace(fi.Name,"");

                _ledger.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("地址不存在或保存失败");
            }
        }

        public int GetMaxCertWordSn(DateTime voucherDate, int certWordId)
        {
            int y = voucherDate.Year, m = voucherDate.Month;
            Guid abid = _cache.GetUserCache().AccountBookID;
            int cwSn = 0;
            var voucher = _ledger.Vouchers.Where(v => v.VoucherYear == y && v.VoucherMonth == m && v.CertificateWord.CwId == certWordId && v.AbId == abid).ToList();
            if (voucher.Count() > 0) cwSn = voucher.Max(v => v.CertWordSN);
            return cwSn;
        }

        /// <summary>
        /// 校验录入借贷是否平衡
        /// </summary>
        /// <param name="vmVoucher">凭证借贷额</param>
        /// <returns>空为校验通过，否则为出错原因</returns>
        public string IsVoucherBalance(VoucherViewModel vmVoucher)
        {
            decimal Debit = 0, Credit = 0;
            foreach (VoucherDetailViewModel d in vmVoucher.VoucherDetails)
            {
                Account acc = _account.GetAccount(d.AccId);
                if (acc == null) return string.Format("科目无效({1})。摘要：{0}", d.Abstract, d.AccId);

                if(acc.Direction=="借")
                {
                    Debit += d.Debit;
                    Debit += d.Credit * (-1);
                }
                else
                {
                    Credit += d.Credit;
                    Credit += d.Debit * (-1);
                }
            }

            if (Debit != Credit) return "录入借贷不平";

            return string.Empty;
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

        void SetInvoicePath(long vid, string path);

        List<AbstractTemp> GetMyAbstracts();

        void DeleteAbstract(int absId);

        List<Voucher> SearchVoucher(SearchConditionViewModel condition);

        List<SearchVoucherViewModel> VoucherToSearchVoucherViewModel(List<Voucher> vouchers);

        int GetMaxCertWordSn(DateTime voucherDate, int certWordId);

        string IsVoucherBalance(VoucherViewModel vmVoucher);
    }
}