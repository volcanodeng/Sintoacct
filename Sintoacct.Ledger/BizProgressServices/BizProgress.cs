using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Sintoacct.Progress.Models;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public class BizProgressService
    {
        private readonly BizProgressContext _context;
        private readonly ClaimsIdentity _identity;

        public BizProgressService(BizProgressContext progContext, HttpContextBase context)
        {
            _context = progContext;
            _identity = context.User.Identity as ClaimsIdentity;
        }

        public List<BizProgress> GetMyBizProgresses(int pageIndex,int pageSize)
        {
            return _context.BizProgress.Where(p => p.Creator == _identity.GetUserName()).Skip(pageIndex * pageSize).Take(pageSize).OrderByDescending(p => p.BizId).ToList();
        }

        public List<BizProgress> GetMyBizProgresses()
        {
            return this.GetMyBizProgresses(0, 50);
        }

        public BizProgress GetBizProgress(long bizId)
        {
            return _context.BizProgress.Where(p => p.BizId == bizId).FirstOrDefault();
        }

        public BizProgress GetMyBizProgress(long bizId)
        {
            return _context.BizProgress.Where(p => p.Creator == _identity.GetUserName() && p.BizId == bizId).FirstOrDefault();
        }

        public BizProgress AddOrUpdate(BizProgressViewModel bizProg)
        {
            BizProgress prog = null;
            if (bizProg.BizId > 0)
            {
                prog = this.GetBizProgress(bizProg.BizId);
            }
            else
            {
                prog = new BizProgress();
            }

            prog.CusId = bizProg.CusId;

            return prog;
        }
    }
}