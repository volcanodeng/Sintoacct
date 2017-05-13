using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;
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
    }
}