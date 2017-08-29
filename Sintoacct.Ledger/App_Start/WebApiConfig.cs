using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using System.Web.Http.Controllers;
using Newtonsoft.Json;

namespace Sintoacct.Ledger
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // 模型校验
            config.Filters.Add(new ValidateModelAttribute());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }


    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                string err = "";

                foreach (var s in actionContext.ModelState)
                {
                    foreach (var e in s.Value.Errors)
                    {
                        err += string.Format("{0}。<br>", e.ErrorMessage);
                    }
                }
                MessageContent msg = new MessageContent();
                msg.message = err;
                msg.IsSuccess = false;
                var res = actionContext.Request.CreateResponse(HttpStatusCode.OK);
                res.Content = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");
                actionContext.Response = res;
            }
        }
    }

    /// <summary>
    /// Ajax响应消息的内容
    /// </summary>
    public class MessageContent
    {
        public bool IsSuccess { get; set; }
        public string message { get; set; }
        public object State { get; set; }
    }
}
