using System.Web.Mvc;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using Sintoacct.Ledger.BizProgressServices;
using Sintoacct.Ledger.Models;
using System.Web;

namespace Sintoacct.Ledger.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IReportService _report;

        public ReportController(IReportService report)
        {
            _report = report;
        }

        [ClaimsAuthorize("role", "report")]
        public ActionResult ReportProgress()
        {
            return View();
        }

        [ClaimsAuthorize("role", "report")]
        public ActionResult ExportExcelProgress(ProgressSearchViewModel condition)
        {
            string s = ",";
            //标题头
            List<string> columnNames = new List<string>() { "客户名称", "项目名称", "工作步骤", "进度记录", "完成时间", "记录人", "业务费用", "合同时间" };
            string colNames = "";
            foreach (string c in columnNames)
            {
                if (colNames != "") colNames += s;

                colNames += c;
            }

            //数据内容
            string data = "";
            var progs = _report.GetProgressList(condition);
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms,Encoding.Default))
                {
                    sw.WriteLine(colNames);

                    foreach (ProgressListViewModel pl in progs)
                    {
                        data += (pl.CustomerName + s);
                        data += (pl.ItemName + s);
                        data += (pl.StepName + s);
                        data += (pl.ResultDesc + s);
                        if(pl.CompletedTime.HasValue)data += (pl.CompletedTime.Value.ToString("yyyy-MM-dd") + s);
                        data += pl.Creator + s;
                        data += pl.CommercialExpense.ToString() + s;
                        data += pl.ContractTime.ToString("yyyy-MM-dd") ;

                        sw.WriteLine(data);
                        data = "";
                    }
                    sw.Flush();

                    Response.Clear();
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + string.Format("进度记录{0}.csv", DateTime.Now.ToString("yyyy-MM-dd")));
                    Response.AppendHeader("Content-Length", "" + ms.Length);

                    byte[] bts = ms.ToArray();
                    Response.OutputStream.Write(bts, 0, bts.Length);
                    Response.OutputStream.Flush();

                    sw.Close();
                    ms.Close();
                }
            }

            return null;
        }

        [ClaimsAuthorize("role", "report")]
        public JsonResult GetProgressCreators()
        {
            List<string> creators = _report.GetProgressCreators();
            List<ComboboxViewModel> cbCreators = new List<ComboboxViewModel>();
            foreach(string s in creators)
            {
                cbCreators.Add(new ComboboxViewModel() { text = s, val = s });
            }
            return Json(cbCreators);
        }

        [ClaimsAuthorize("role", "report")]
        public JsonResult GetProgressContacts()
        {
            List<string> contacts = _report.GetContacts();
            List<ComboboxViewModel> cbContacts = new List<ComboboxViewModel>();
            foreach (string s in contacts)
            {
                cbContacts.Add(new ComboboxViewModel() { text = s, val = s });
            }
            return Json(cbContacts);
        }
    }
}