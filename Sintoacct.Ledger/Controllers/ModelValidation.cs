using System.Web.Http.ModelBinding;
using log4net;
using Sintoacct.Ledger.Models;
using Sintoacct.Ledger.Services;
using System;

namespace Sintoacct.Ledger.Controllers
{
    public class ModelValidation : IModelValidation
    {
        private readonly ILog _log;
        private readonly ICompanyHelper _company;
        private readonly IVoucherHelper _voucher;
        private readonly IAccountBookHelper _accountBook;

        public ModelValidation(ILog log,
                               ICompanyHelper company,
                               IVoucherHelper voucher,
                               IAccountBookHelper accountBook)
        {
            _log = log;
            _company = company;
            _voucher = voucher;
            _accountBook = accountBook;
        }

        public bool ValidAccountBookCreate(AcctBookViewModels acctBook,out string err)
        {
            err = "";

            if(_company.GetCompanyByName(acctBook.ComapnyName)!= null)
            {
                err = "公司名称已存在";
            }

            bool res = string.IsNullOrEmpty(err);
            if(!res) _log.Warn(err);

            return res;
        }

        public bool ValidVoucher(VoucherViewModel voucher,out string err)
        {
            err = string.Empty;

            //账期校验
            AccountBook accBook = _accountBook.GetCurrentBook();
            if(accBook.StartYear<voucher.VoucherDate.Year ||
               (accBook.StartYear==voucher.VoucherDate.Year && accBook.StartPeriod > voucher.VoucherDate.Month))
            {
                err = "凭证日期无效";
                return false;
            }

            //验证凭证字号
            int maxSn = _voucher.GetMaxCertWordSn(voucher.VoucherDate, voucher.CwId);
            if (maxSn + 1 != voucher.CertWordSN) voucher.CertWordSN = maxSn + 1;

            //验证账是否平
            err = _voucher.IsVoucherBalance(voucher);

            return (err == string.Empty ? true : false);
        }
    }

    public interface IModelValidation : IDependency
    {

        bool ValidAccountBookCreate(AcctBookViewModels acctBook, out string err);

        bool ValidVoucher(VoucherViewModel voucher, out string err);
    }
}