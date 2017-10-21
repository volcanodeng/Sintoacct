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

        public List<WorkOrder> GetMyWorkOrders(int pageIndex,int pageSize)
        {
            string curUser = _identity.GetUserName();
            return _context.WorkOrders
                           .Include("WorkOrderItems").Include("WorkOrderItems.BizItem")
                           .Where(p => p.Creator == curUser)
                           .OrderByDescending(p => p.WoId)
                           .Skip(pageIndex * pageSize)
                           .Take(pageSize)
                           .ToList();
        }

        public List<WorkOrder> GetMyWorkOrders()
        {
            return this.GetMyWorkOrders(0, 50);
        }

        public WorkOrder GetWorkOrder(long bizId)
        {
            return _context.WorkOrders.Where(p => p.WoId == bizId).FirstOrDefault();
        }

        public WorkOrder GetMyWorkOrder(long bizId)
        {
            return _context.WorkOrders.Where(p => p.Creator == _identity.GetUserName() && p.WoId == bizId).FirstOrDefault();
        }

        public WorkOrder SaveWorkOrder(WorkOrderViewModel workOrder)
        {
            WorkOrder wo = null;
            if (workOrder.WoId > 0)
            {
                wo = this.GetWorkOrder(workOrder.WoId);
            }
            else
            {
                wo = new WorkOrder();
                wo.CreateTime = DateTime.Now;
                wo.Creator = _identity.GetUserName();
            }

            //业务项目
            WorkOrderItem[] woItems = wo.WorkOrderItems.ToArray();
            for (int i = woItems.Count() - 1; i >= 0; i--)
            {
                //先删除旧记录
                wo.WorkOrderItems.Remove(woItems[i]);
            }
            string[] items = workOrder.BizItemIds.Split(',');
            foreach(string i in items)
            {
                //添加新记录
                WorkOrderItem woi = new WorkOrderItem();
                woi.WorkOrder = wo;
                woi.BizItem = _setting.GetBizItem(Convert.ToInt32(i));
                wo.CommercialExpense += woi.BizItem.ServicePrice;
                wo.WorkOrderItems.Add(woi);
            }

            //校验客户编号的有效性
            wo.Customer = _customer.GetCustomer(workOrder.CusId);
            wo.ContractTime = workOrder.ContractTime;
            wo.Remark = workOrder.Remark;
            wo.BizManager = workOrder.BizManager;
            wo.BizOperations = workOrder.BizOperations;
            wo.Recommend = workOrder.Recommend;
            wo.PreferentialAmount = workOrder.PreferentialAmount;

            _context.WorkOrders.AddOrUpdate(wo);
            _context.SaveChanges();

            return wo;
        }
    }
}