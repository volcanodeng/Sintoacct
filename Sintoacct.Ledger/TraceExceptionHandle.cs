using System.Web.Http.ExceptionHandling;
using System.Diagnostics;

namespace Sintoacct.Ledger
{
    public class TraceExceptionHandle : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            Trace.TraceError(context.ExceptionContext.Exception.ToString());
        }
    }
}