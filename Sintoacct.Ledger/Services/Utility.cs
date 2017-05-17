using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;
using System.Data.SqlClient;
using System.Data;
using AutoMapper;

namespace Sintoacct.Ledger.Services
{
    public static class Utility
    {
        /// <summary>
        /// 递归便利科目信息，构建树形数据。
        /// </summary>
        /// <param name="accounts">科目数据</param>
        /// <param name="tree">树模型</param>
        public static void AccountRecursion(List<Account> accounts, TreeViewModel<AccountViewModel> tree)
        {
            List<Account> subAccounts = accounts.Where(a => a.ParentAccCode == tree.attributes.AccCode).ToList();
            foreach (Account a in subAccounts)
            {
                TreeViewModel<AccountViewModel> accNode = new TreeViewModel<AccountViewModel>();
                accNode.id = a.AccId.ToString();
                accNode.text = a.AccName;
                accNode.state = "open";
                accNode.@checked = false;
                accNode.attributes = Mapper.Map<AccountViewModel>(a);
                tree.children.Add(accNode);

                AccountRecursion(accounts.Where(acc => acc.ParentAccCode == a.AccCode).ToList(), accNode);
            }

        }

        /// <summary>
        /// 统一生成查询参数。
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns>参数接口</returns>
        public static IDataParameter NewParameter(string name,object value)
        {
            return new SqlParameter(name, value);
        }

        /// <summary>
        /// 生成SQL参数名称。
        /// </summary>
        /// <param name="pName">纯参数名，不带参数符号</param>
        /// <returns>SQL参数名</returns>
        public static string ParameterNameString(string pName)
        {
            return string.Format("@{0}", pName);
        }
    }
}