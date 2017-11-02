﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Sintoacct.Ledger.BizProgressServices;
using AutoMapper;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Controllers.BizProgress
{
    public class BizSettingController : BaseController
    {
        private readonly ClaimsIdentity _identity;
        private readonly IBizSetting _bizSetting;

        public BizSettingController(HttpContextBase context,IBizSetting bizSetting)
        {
            _identity = context.User.Identity as ClaimsIdentity;
            _bizSetting = bizSetting;
        }

        public ActionResult BizConfig()
        {
            return View();
        }

        [ClaimsAuthorize("role", "business")]
        public JsonResult GetBizCategories()
        {
            return Json(Mapper.Map <List<BizCategoryViewModel>>(_bizSetting.GetBizCategories()),JsonRequestBehavior.AllowGet);
        }

        [ClaimsAuthorize("role", "business")]
        public JsonResult GetBizItemInCate(int id)
        {
            return Json(Mapper.Map<List<BizItemViewModel>>(_bizSetting.GetBizItemsInCate(id)),JsonRequestBehavior.AllowGet);
        }

        [ClaimsAuthorize("role", "business")]
        public JsonResult GetAllBizItems()
        {
            return Json(Mapper.Map<List<BizItemViewModel>>(_bizSetting.GetBizItems()));
        }

        [ClaimsAuthorize("role", "business")]
        public JsonResult GetBizStepInItem(int id)
        {
            return Json(Mapper.Map<List<BizStepsViewModel>>(_bizSetting.GetBizStepInItem(id)));
        }
    }
}