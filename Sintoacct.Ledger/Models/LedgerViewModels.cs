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
        public object State { get; set; }
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
    /// 账套删除
    /// </summary>
    public class AcctBookDelViewModel
    {
        public string AbId { get; set; }
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

    /// <summary>
    /// 辅助核算保存
    /// </summary>
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

    /// <summary>
    /// 辅助核算删除
    /// </summary>
    public class AuxiliaryDeleteViewModel
    {
        /// <summary>
        /// 辅助核算编号
        /// </summary>
        public long AuxId { get; set; }
    }

    /// <summary>
    /// 科目类别保存、显示
    /// </summary>
    public class AccountCategoryViewModel
    {
        public int AcId { get; set; }

        public int? ParentAcId { get; set; }

        [MaxLength(50), Required]
        public string CategoryName { get; set; }
    }

    /// <summary>
    /// 科目保存
    /// </summary>
    public class AccountViewModel
    {
        public long AccId { get; set; }

        [MaxLength(20), Required]
        public string AccCode { get; set; }

        [MaxLength(20)]
        public string ParentAccCode { get; set; }

        public int AcId { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }

        [Required, MaxLength(50)]
        public string AccName { get; set; }

        [Required, MaxLength(5)]
        public string Direction { get; set; }

        public bool IsAuxiliary { get; set; }

        [MaxLength(200)]
        public string AuxTypeIds { get; set; }

        [MaxLength(200)]
        public string AuxTypeNames { get; set; }

        public bool IsQuantity { get; set; }

        [MaxLength(5)]
        public string Unit { get; set; }

        public decimal? InitialQuantity { get; set; }

        public decimal? InitialBalance { get; set; }

        public decimal? YtdDebitQuantity { get; set; }

        public decimal? YtdDebit { get; set; }

        public decimal? YtdCreditQuantity { get; set; }

        public decimal? YtdCredit { get; set; }

        public decimal? YtdBeginBalanceQuantity { get; set; }

        public decimal? YtdBeginBalance { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountState State { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 科目删除
    /// </summary>
    public class AccountDeleteViewModel
    {
        public long AccId { get; set; }
    }

    public class AccountInitViewModel
    {
        public List<AccountViewModel> Accounts { get; set; }
    }

    /// <summary>
    /// 返回Account
    /// </summary>
    public class AccountControllerViewModel
    {
        public List<AccountCategoryViewModel> AccountCategorys { get; set; }

        public List<AuxiliaryTypeViewModel> AuxiliaryTypes { get; set; }

        public List<int> AccountCategoriesWithQuantity { get; set; }

        public List<AuxiliaryViewModel> AuxCustom { get; set; }
        public List<AuxiliaryViewModel> AuxSuppliers { get; set; }
        public List<AuxiliaryViewModel> AuxEmployee { get; set; }
        public List<AuxiliaryViewModel> AuxProject { get; set; }
        public List<AuxiliaryViewModel> AuxSector { get; set; }
        public List<AuxiliaryViewModel> AuxInventory { get; set; }

    }

    /// <summary>
    /// 辅助核算明细保存
    /// </summary>
    public class AuxiliaryAccountViewModel
    {
        public long AccId
        {
            get; set;
        }

        public long? Custom
        {
            get; set;
        }

        public long? Suppliers
        {
            get; set;
        }

        public long? Employee
        {
            get; set;
        }

        public long? Project
        {
            get; set;
        }

        public long? Sector
        {
            get; set;
        }

        public long? Inventory
        {
            get; set;
        }
    }

    /// <summary>
    /// 平衡试算表
    /// </summary>
    public class TrialBalanceViewModel
    {
        /// <summary>
        /// 项目名称。如：期初余额、累计发生额
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 借方金额
        /// </summary>
        public decimal DebitBalance { get; set; }

        /// <summary>
        /// 贷方金额
        /// </summary>
        public decimal CreditBalance { get; set; }

        /// <summary>
        /// 差额
        /// </summary>
        public decimal Imbalance { get; set; }
    }

    /// <summary>
    /// 凭证主信息
    /// </summary>
    public class VoucherViewModel
    {
        public VoucherViewModel()
        {
            VoucherDetails = new List<VoucherDetailViewModel>();
        }

        public long VId { get; set; }

        public int CwId { get; set; }

        public string CertWord { get; set; }

        public int CertWordSN { get; set; }

        public DateTime VoucherDate { get; set; }

        public string PaymentTerms { get; set; }

        public int InvoiceCount { get; set; }

        public int State { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public string Review { get; set; }

        public DateTime? ReviewTime { get; set; }

        public ICollection<VoucherDetailViewModel> VoucherDetails { get; set; }
    }

    /// <summary>
    /// 凭证明细
    /// </summary>
    public class VoucherDetailViewModel
    {
        public long VdId { get; set; }

        public string Abstract { get; set; }

        public long AccId { get; set; }

        public string AccountCode { get; set; }

        public string AccountName { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? Price { get; set; }

        public double Debit { get; set; }

        public double Credit { get; set; }
    }

    /// <summary>
    /// 凭证查询、删除、审核
    /// </summary>
    public class VoucherIdViewModel
    {
        public long VId { get; set; }
    }

    /// <summary>
    /// 摘要。保存、删除。
    /// </summary>
    public class AbstractViewModel
    {
        public int AbsId { get; set; }

        public string Abstract { get; set; }
    }
}