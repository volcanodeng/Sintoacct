using System.Web.Http.ModelBinding;
using log4net;
using Sintoacct.Ledger.Models;
using Sintoacct.Ledger.Services;

namespace Sintoacct.Ledger.Controllers
{
    public class ModelValidation : IModelValidation
    {
        private readonly ILog _log;
        private readonly ICompanyHelper _company;

        public ModelValidation(ILog log,ICompanyHelper company)
        {
            _log = log;
            _company = company;
        }

        /// <summary>
        /// 根据Model标签约束校验Model有效性
        /// </summary>
        /// <param name="modelState">模型状态</param>
        /// <param name="err">校验失败原因</param>
        /// <returns>校验结果</returns>
        public bool Valid(ModelStateDictionary modelState, out string err)
        {
            err = "";
            if (!modelState.IsValid)
            {
                foreach (var s in modelState)
                {
                    foreach (var e in s.Value.Errors)
                    {
                        err += e.ErrorMessage;
                    }
                }

                _log.Warn(err);
            }

            return modelState.IsValid;
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
    }

    public interface IModelValidation : IDependency
    {
        bool Valid(ModelStateDictionary modelState, out string err);

        bool ValidAccountBookCreate(AcctBookViewModels acctBook, out string err);
    }
}