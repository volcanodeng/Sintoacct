using System.Web.Http.ModelBinding;
using log4net;

namespace Sintoacct.Ledger.Controllers
{
    public class ModelValidation : IModelValidation
    {
        private readonly ILog _log;

        public ModelValidation(ILog log)
        {
            _log = log;
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
    }

    public interface IModelValidation : IDependency
    {
        bool Valid(ModelStateDictionary modelState, out string err);
    }
}