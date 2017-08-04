using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger
{
    public class TraceExceptionHandle : ExceptionLogger
    {
        private readonly CommonContext _common;

        public TraceExceptionHandle()
        {
            _common = new CommonContext();
        }

        public override void Log(ExceptionLoggerContext context)
        {
            ExceptionLog exception = new ExceptionLog();
            exception.RequestUrl = context.Request.RequestUri.AbsoluteUri;
            exception.RequestDetail = JsonConvert.SerializeObject(context.Request);
            exception.ExceptionMessage = context.Exception.Message;
            exception.ExceptionDetail = JsonConvert.SerializeObject(context.Exception);
            exception.LogTime = System.DateTime.Now;
            _common.Exceptions.Add(exception);
            _common.SaveChanges();
        }

    }
}