using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Sintoacct.Progress.Models;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public class BizProgressService : IBizProgressService
    {
        private readonly BizProgressContext _context;
        private readonly ClaimsIdentity _identity;
        private readonly IBizCustomer _customer;
        private readonly IBizSetting _setting;

        public BizProgressService(BizProgressContext progContext, 
                                  HttpContextBase context,
                                  IBizCustomer customer,
                                  IBizSetting setting)
        {
            _context = progContext;
            _identity = context.User.Identity as ClaimsIdentity;
            _customer = customer;
            _setting = setting;
        }

        public List<WorkOrder> GetMyBizProgresses(int pageIndex,int pageSize)
        {
            return _context.BizProgress.Where(p => p.Creator == _identity.GetUserName()).Skip(pageIndex * pageSize).Take(pageSize).OrderByDescending(p => p.WoId).ToList();
        }

        public List<WorkOrder> GetMyBizProgresses()
        {
            return this.GetMyBizProgresses(0, 50);
        }

        public WorkOrder GetBizProgress(long bizId)
        {
            return _context.BizProgress.Where(p => p.WoId == bizId).FirstOrDefault();
        }

        public WorkOrder GetMyBizProgress(long bizId)
        {
            return _context.BizProgress.Where(p => p.Creator == _identity.GetUserName() && p.WoId == bizId).FirstOrDefault();
        }

        public WorkOrder SaveProgress(BizProgressViewModel bizProg)
        {
            WorkOrder prog = null;
            if (bizProg.BizId > 0)
            {
                prog = this.GetBizProgress(bizProg.BizId);
            }
            else
            {
                prog = new WorkOrder();
                prog.CreateTime = DateTime.Now;
                prog.Creator = _identity.GetUserName();
            }

            //校验客户编号的有效性
            prog.Customer = _customer.GetCustomer(bizProg.CusId);
            prog.ContractTime = bizProg.ContractTime;

            //prog.BizCategory = _setting.GetBizCategory(bizProg.CateId);

            //prog.BizItem = _setting.GetBizItem(bizProg.ItemId);

            //prog.BizStep = _setting.GetStep(bizProg.StepId);

            //prog.ProgressDesc = bizProg.ProgressDesc;
#warning 上传文件保存逻辑（本地文件或七牛文件服务器）

            prog.Remark = bizProg.Remark;

            prog.BizManager = bizProg.BizManager;
            prog.BizOperations = bizProg.BizOperations;

            _context.BizProgress.AddOrUpdate(prog);
            _context.SaveChanges();

            return prog;
        }
    }
}