using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Sintoacct.Ledger.Models;
using Sintoacct.Progress.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public class ReportService : IReportService
    {
        private readonly BizProgressContext _context;

        public ReportService(BizProgressContext progContext)
        {
            _context = progContext;
        }

        public List<ProgressListViewModel> GetProgressList(ProgressSearchViewModel condition)
        {
            List<SqlParameter> parameters = new List<SqlParameter>(); 
            string sql = "select top 500 o.[ContractTime],o.[BizManager],o.[BizOperations],o.[Recommend],o.[CommercialExpense],c.[CustomerName],i.ItemName,s.StepName,p.CompletedTime,p.ResultDesc,p.CreateTime,p.Creator,c.Contacts " +
                         " from T_Prog_WorkOrder o left join T_Prog_Customers c on o.CusId=c.CusId left join T_Prog_WorkProgress p on o.WoId=p.WoId left join T_Prog_BizItems i on p.ItemId=i.ItemId left join T_Prog_BizSteps s on p.StepId=s.StepId" +
                         " where o.State>0 and p.ResultDesc is not null";
            if (condition.sCreate.HasValue) {
                sql += " and p.CompletedTime >= @sCreate";
                parameters.Add(new SqlParameter("@sCreate", condition.sCreate));
            }
            if (condition.eCreate.HasValue)
            {
                sql += " and p.CompletedTime <= @eCreate";
                parameters.Add(new SqlParameter("@eCreate", condition.eCreate));
            }
            if(!string.IsNullOrEmpty( condition.CustomerName))
            {
                sql += " and c.CusId = @CustomerName";
                parameters.Add(new SqlParameter("@CustomerName",  condition.CustomerName));
            }
            if(!string.IsNullOrEmpty(condition.Creator))
            {
                sql += " and p.Creator like @Creator";
                parameters.Add(new SqlParameter("@Creator", string.Format("%{0}%", condition.Creator)));
            }
            if(!string.IsNullOrEmpty(condition.Contacts))
            {
                sql += " and c.Contacts like @Contacts";
                parameters.Add(new SqlParameter("@Contacts", string.Format("%{0}%", condition.Contacts)));
            }
            sql += " order by  p.CreateTime desc";

            List<ProgressListViewModel> progs = _context.Database.SqlQuery<ProgressListViewModel>(sql, parameters.ToArray()).ToList();

            return progs;
        }

        public List<string> GetProgressCreators()
        {
            List<string> proCreators = _context.WorkProgress.GroupBy(p => p.Creator).Select(p => p.Key).ToList();
            return proCreators;
        }

        public List<string> GetContacts()
        {
            List<string> proContacts = _context.Customers.GroupBy(c => c.Contacts).Select(c => c.Key).ToList();
            return proContacts;
        }
    }
}