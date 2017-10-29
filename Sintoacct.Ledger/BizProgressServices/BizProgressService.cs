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
                           .Include("WorkOrderItems").Include("WorkOrderItems.BizItem").Include("Customer")
                           .Where(p => p.Creator == curUser && p.State != WorkOrderState.Deleted)
                           .OrderByDescending(p => p.WoId)
                           .Skip(pageIndex * pageSize)
                           .Take(pageSize)
                           .ToList();
        }

        public List<WorkOrder> GetMyWorkOrders()
        {
            return this.GetMyWorkOrders(0, 50);
        }

        public WorkOrder GetWorkOrder(long woId)
        {
            return _context.WorkOrders.Include("WorkProgresses").Where(p => p.WoId == woId).FirstOrDefault();
        }

        public WorkOrder GetMyWorkOrder(long woId)
        {
            return _context.WorkOrders.Where(p => p.Creator == _identity.GetUserName() && p.WoId == woId).FirstOrDefault();
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
                wo.State = WorkOrderState.New;
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
                //添加关联项目
                WorkOrderItem woi = new WorkOrderItem();
                woi.WorkOrder = wo;
                woi.BizItem = _setting.GetBizItem(Convert.ToInt32(i));
                wo.CommercialExpense += woi.BizItem.ServicePrice;
                wo.WorkOrderItems.Add(woi);

                //生成进度步骤
                foreach (BizSteps s in woi.BizItem.BizSteps)
                {
                    WorkProgress p = new WorkProgress();
                    p.WorkOrder = wo;
                    p.BizItem = woi.BizItem;
                    p.BizStep = s;
                    p.CompletedTime = null;
                    p.ResultDesc = null;
                    p.AdvanceExpenditure = 0;
                    p.Creator = _identity.GetUserName();
                    p.CreateTime = DateTime.Now;
                    wo.WorkProgresses.Add(p);
                }
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

        public void DeleteWorkOrder(WorkOrderDelViewModel workOrder)
        {
            WorkOrder wo = this.GetWorkOrder(workOrder.WoId);
            if(wo == null)
            {
                throw new NullReferenceException("找不到要删除的工单：" + workOrder.WoId);
            }

            wo.State = WorkOrderState.Deleted;
            _context.SaveChanges();
        }


        public List<WorkProgress> GetWorkProgress(long woId, int itemId)
        {
            return _context.WorkProgress.Include("BizStep").Where(wp => wp.WoId == woId && wp.ItemId == itemId).OrderBy(wp => wp.BizStep.SortIndex).ToList();
        }
    }
}