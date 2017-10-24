using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using Sintoacct.Progress.Models;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.BizProgressServices
{
    public class BizCustomer : IBizCustomer
    {
        private readonly BizProgressContext _context;

        public BizCustomer(BizProgressContext progContext)
        {
            _context = progContext;
        }

        public List<Customers> GetCustomers(BizCustomerConditionViewModel condition)
        {
            if(condition.CusId.HasValue)
            return _context.Customers.Where(c=>condition.CusId.Value==c.CusId).ToList();

            return _context.Customers.Where(c => (string.IsNullOrEmpty(condition.CustomerName) || c.CustomerName.Contains(condition.CustomerName)) && 
                                                 (string.IsNullOrEmpty(condition.Phone) || c.Phone.Contains(condition.Phone))).ToList();
        }

        public Customers GetCustomer(long cusId)
        {
            return _context.Customers.Where(c => c.CusId == cusId).FirstOrDefault();
        }

        public Customers SaveCustomer(BizCustomerViewModel customer)
        {
            Customers cust = null;
            if(customer.CusId > 0)
            {
                //推荐人不能被修改
                cust = this.GetCustomer(customer.CusId);
            }
            else
            {
                cust = new Customers();
                cust.PromId = customer.PromId;
                cust.PromName = customer.PromName;
                cust.State = CustomerState.Normal;
            }
            cust.CustomerName = customer.CustomerName;
            cust.CustomerAddress = customer.CustomerAddress;
            cust.BusinessAddress = customer.BusinessAddress;
            cust.Contacts = customer.Contacts;
            cust.Phone = customer.Phone;
            cust.Level = customer.Level;

            _context.Customers.AddOrUpdate(cust);
            _context.SaveChanges();

            return cust;
        }

        public void DeleteCustomer(long cusId)
        {
            Customers cus = this.GetCustomer(cusId);
            if(cus==null)
            {
                throw new NullReferenceException("找不到要删除的客户信息：" + cusId);
            }

            cus.State = CustomerState.Deleted;
            _context.SaveChanges();
        }


        #region 推广人

        public BizPromotion GetPromotion(long promId)
        {
            return _context.BizPromotions.Where(p => p.PromId == promId).FirstOrDefault();
        }

        public BizPromotion SavePromotion(BizPromotionViewModel promotion)
        {
            BizPromotion prom = null;
            if(promotion.PromId > 0)
            {
                prom = this.GetPromotion(promotion.PromId);
            }
            else
            {
                prom = new BizPromotion();
            }

            
            if (!promotion.ParentPromId.HasValue)
            {
                prom.ParentPromId = null;
                prom.PromChain = null;
            }
            else
            {
                BizPromotion parentProm = this.GetPromotion(promotion.ParentPromId.Value);
                prom.ParentPromId = promotion.ParentPromId.Value;
                prom.PromChain = (string.IsNullOrEmpty(parentProm.PromChain) ? parentProm.ParentPromId.Value.ToString() : parentProm.PromChain + "," + parentProm.ParentPromId.Value.ToString());
            }
            prom.OpName = promotion.OpName;
            prom.WeixinOpenId = promotion.WeixinOpenId;
            prom.PromLevel = promotion.PromLevel;

            _context.BizPromotions.AddOrUpdate(prom);
            _context.SaveChanges();

            return null;
        }

        #endregion
    }
}