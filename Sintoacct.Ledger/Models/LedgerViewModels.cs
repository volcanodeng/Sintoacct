using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;

namespace Sintoacct.Ledger.Models
{
    /// <summary>
    /// Ajax响应消息
    /// </summary>
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
            msg.StatusCode = HttpStatusCode.OK;
            msg.Content = new StringContent(JsonConvert.SerializeObject(msg.MessageContent), Encoding.UTF8, "application/json");
            throw new HttpResponseException(msg);
        }

        public static void Fail(HttpStatusCode code, string reason)
        {
            ResMessage msg = new ResMessage();
            msg.MessageContent.message = reason;
            msg.MessageContent.IsSuccess = false;
            msg.StatusCode = code;
            msg.Content = new StringContent(JsonConvert.SerializeObject(msg.MessageContent), Encoding.UTF8, "application/json");
            throw new HttpResponseException(msg);
        }
    }
    /// <summary>
    /// Ajax响应消息的内容
    /// </summary>
    public class ResMessageContent
    {
        public bool IsSuccess { get; set; }
        public string message { get; set; }
    }

    /// <summary>
    /// 账套列表
    /// </summary>
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
    /// <summary>
    /// 账套保存
    /// </summary>
    public class AcctBookViewModels
    {
        public string AbId { get; set; }

        [Required, MaxLength(50)]
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

    /// <summary>
    /// 凭证字保存
    /// </summary>
    public class CertWordViewModel
    {
        public int CwId { get; set; }

        [MaxLength(50), Required]
        public string CertWord { get; set; }

        [MaxLength(50)]
        public string PrintTitle { get; set; }

        public bool IsDefault { get; set; }
    }
    /// <summary>
    /// 凭证字删除及设置默认
    /// </summary>
    public class CertWordDeleteViewModel
    {
        public int CwId { get; set; }
    }

    /// <summary>
    /// 辅助核算类型保存、删除和显示
    /// </summary>
    public class AuxiliaryTypeViewModel
    {
        public int AtId { get; set; }

        /// <summary>
        /// 核算类型名称
        /// </summary>
        [MaxLength(20),Required]
        public string AuxType { get; set; }
    }

    /// <summary>
    /// 辅助核算明细列表
    /// </summary>
    public class AuxiliaryListViewModel
    {
        public int AuxType { get; set; }

        public List<AuxiliaryTypeViewModel> AuxTypes { get; set; }
    }

    public class AuxiliaryViewModel
    {
        
        /// <summary>
        /// 辅助核算编号
        /// </summary>
        public long AuxId { get; set; }

        /// <summary>
        /// 辅助核算编码
        /// </summary>
        [MaxLength(20), Required]
        public string AuxCode { get; set; }

        /// <summary>
        /// 辅助核算名称
        /// </summary>
        [MaxLength(20), Required]
        public string AuxName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AuxiliaryState AuxiliaryState { get; set; }

        /// <summary>
        /// 核算类型编号
        /// </summary>
        public int AtId { get; set; }

        /// <summary>
        /// 核算类型名称
        /// </summary>
        [MaxLength(20)]
        public string AuxType { get; set; }

        /// <summary>
        /// 创建人名称。userid在审计表记录。
        /// </summary>
        [MaxLength(50)]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }

    public class AuxiliaryDeleteViewModel
    {
        /// <summary>
        /// 辅助核算编号
        /// </summary>
        public long AuxId { get; set; }
    }
}