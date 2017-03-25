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

            return _ledger.Vouchers.Where(v => v.AbId == abid && v.VId == vid).Include(v=>v.VoucherDetails).FirstOrDefault();
        }

        public Voucher Save(VoucherViewModel vmVoucher)
        {
            Voucher voucher = new Voucher();
            if (voucher.VId > 0)
            {
                voucher = this.GetMyVoucher(voucher.VId);
                if (voucher == null) return null;

                voucher.CertificateWord = _ledger.CertificateWords.Where(cw => cw.CwId == vmVoucher.CwId).FirstOrDefault();
                voucher.CertWordSN = vmVoucher.CertWordSN;
                voucher.VoucherDate = vmVoucher.VoucherDate;
                voucher.PaymentTerms = string.Format("{0}年第{1}期", voucher.VoucherDate.Year, voucher.VoucherDate.Month);
                voucher.InvoiceCount = vmVoucher.InvoiceCount;

                foreach (VoucherDetailViewModel vd in vmVoucher.VoucherDetails)
                {
                    VoucherDetail vDetail = voucher.VoucherDetails.Where(d => d.VdId == vd.VdId).FirstOrDefault();
                    if (vDetail == null) continue;

                    vDetail.Abstract = vd.Abstract;
                    vDetail.Account = _account.GetAccount(vd.AccId);
                    vDetail.AccountCode = vDetail.Account.AccCode;
                    vDetail.AccountName = vDetail.Account.AccName;
                    vDetail.Quantity = vDetail.Quantity;
                    vDetail.Price = vDetail.Price;
                    vDetail.Debit = vDetail.Debit;
                    vDetail.Credit = vDetail.Credit;
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

                foreach (VoucherDetailViewModel vd in vmVoucher.VoucherDetails)
                {
                    VoucherDetail vDetail = new VoucherDetail();
                    vDetail.Abstract = vd.Abstract;
                    vDetail.Account = _account.GetAccount(vd.AccId);
                    vDetail.AccountCode = vDetail.Account.AccCode;
                    vDetail.AccountName = vDetail.Account.AccName;
                    vDetail.Quantity = vDetail.Quantity;
                    vDetail.Price = vDetail.Price;
                    vDetail.Debit = vDetail.Debit;
                    vDetail.Credit = vDetail.Credit;

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
    }

    public interface IVoucherHelper : IDependency
    {
        Voucher GetMyVoucher(long vid);

        Voucher Save(VoucherViewModel vmVoucher);

        void Delete(long vid);

        void Audit(long vid);

        VoucherViewModel CopyNew(long vid);
    }
}