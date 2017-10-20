using System.Web.Http.ModelBinding;
using log4net;
using Sintoacct.Ledger.Models;
using Sintoacct.Ledger.Services;
using System;
using Sintoacct.Ledger.BizProgressServices;

namespace Sintoacct.Ledger.Controllers
{
    public class ModelValidation : IModelValidation
    {
        private readonly ILog _log;
        private readonly ICompanyHelper _company;
        private readonly IVoucherHelper _voucher;
        private readonly IAccountBookHelper _accountBook;
        private readonly IBizSetting _setting;
        private readonly IBizCustomer _customer;

        public ModelValidation(ILog log,
                               ICompanyHelper company,
                               IVoucherHelper voucher,
                               IAccountBookHelper accountBook,
                               IBizSetting setting,
                               IBizCustomer customer)
        {
            _log = log;
            _company = company;
            _voucher = voucher;
            _accountBook = accountBook;

            _setting = setting;
            _customer = customer;
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

        public bool ValidBizProgress(WorkOrderViewModel workOrder,out string err)
        {
            err = string.Empty;

            if (workOrder.CusId <= 0 )
            {
                err = "必选项无效";
                return false;
            }

            if (_customer.GetCustomer(workOrder.CusId) == null)
            {
                err = "必选项为空";
                return false;
            }

            return true;
        }

        public bool ValidBizCustomer(BizCustomerViewModel customer,out string err)
        {
            err = string.Empty;
            Guid uid;
            if(!Guid.TryParse(customer.PromId,out uid) || _company.GetBizPerson(uid)==null)
            {
                err = "推荐人为空";
                return false;
            }

            return true;
        }
    }

    public interface IModelValidation : IDependency
    {

        bool ValidAccountBookCreate(AcctBookViewModels acctBook, out string err);

        bool ValidVoucher(VoucherViewModel voucher, out string err);

        bool ValidBizProgress(WorkOrderViewModel progress, out string err);

        bool ValidBizCustomer(BizCustomerViewModel customer, out string err);
    }
}