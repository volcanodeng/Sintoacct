﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using Sintoacct.Ledger.Models;
using Newtonsoft.Json;

namespace Sintoacct.Ledger
{
    /// <summary>
    /// 异常处理（web api和controller出现异常会执行这里）
    /// </summary>
    public class ExceptionHandleAttribute : ActionFilterAttribute, IExceptionFilter
    {
        

        public void OnException(ExceptionContext filterContext)
        {
            LedgerContext context = new LedgerContext();

            ExceptionLog exception = new ExceptionLog();
            exception.RequestUrl = filterContext.HttpContext.Request.RawUrl;
            exception.RequestDetail = JsonConvert.SerializeObject(filterContext.HttpContext.Request.Params);
            exception.ExceptionMessage = filterContext.Exception.Message;
            exception.ExceptionDetail = JsonConvert.SerializeObject(filterContext.Exception);
            exception.LogTime = System.DateTime.Now;
            context.Exceptions.Add(exception);
            context.SaveChanges();
            
        }
    }
}