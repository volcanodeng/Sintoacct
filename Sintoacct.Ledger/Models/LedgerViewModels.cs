using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Sintoacct.Ledger.Models
{
    public class ResMessage : HttpResponseMessage
    {
        private ResMessageContent _content;

        public ResMessage()
        {
            this.StatusCode = HttpStatusCode.OK;

            _content = new ResMessageContent();
            _content.message = "成功";
            _content.IsSuccess = true;
        }

        public ResMessageContent MessageContent
        {
            get
            {
                return _content;
            }
        }

        public static ResMessageContent Success()
        {
            ResMessage msg = new ResMessage();
            return msg.MessageContent;
        }

        public static void Fail(string reason)
        {
            ResMessage msg = new ResMessage();
            msg.MessageContent.message = reason;
            msg.MessageContent.IsSuccess = false;
            msg.StatusCode = HttpStatusCode.Forbidden;
            msg.Content = new StringContent(JsonConvert.SerializeObject(msg.MessageContent), Encoding.UTF8, "application/json");
            throw new HttpResponseException(msg);
        }

        public static void Fail(HttpStatusCode code,string reason)
        {
            ResMessage msg = new ResMessage();
            msg.MessageContent.message = reason;
            msg.MessageContent.IsSuccess = false;
            msg.StatusCode = code;
            msg.Content = new StringContent(JsonConvert.SerializeObject(msg.MessageContent), Encoding.UTF8, "application/json");
            throw new HttpResponseException(msg);
        }
    }

    public class ResMessageContent
    {
        public bool IsSuccess { get; set; }
        public string message { get; set; }
    }

    public class AcctBookListViewModels
    {
        public string AbId { get; set; }

        public string Currency { get; set; }

        public int StartYear { get; set; }

        public int StartPeriod { get; set; }

        public string FiscalSystem { get; set; }

        public string CompanyName { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public int State { get; set; }
    }

    public class AcctBookViewModels
    {
        public string AbId { get; set; }

        [Required,MaxLength(50)]
        public string ComapnyName { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; }

        [Required]
        public int StartYear { get; set; }

        [Required]
        public int StartPeriod { get; set; }

        [Required]
        public int FiscalSystem { get; set; }
    }

   
}