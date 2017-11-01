using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Sintoacct.Ledger.Models;
using Sintoacct.Progress.Models;

namespace Sintoacct.Ledger
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<AccountBook, AcctBookListViewModels>()
                .ForMember(dest=>dest.CompanyName,opt=>opt.MapFrom(src=>src.Company.ComName));


                cfg.CreateMap<CertificateWord, CertWordViewModel>();

                cfg.CreateMap<AuxiliaryType, AuxiliaryTypeViewModel>();

                cfg.CreateMap<Auxiliary, AuxiliaryViewModel>();

                cfg.CreateMap<AccountCategory, AccountCategoryViewModel>();

                cfg.CreateMap<Account, AccountViewModel>()
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(src=>src.AccountCategory.CategoryName));

                cfg.CreateMap<Voucher, VoucherViewModel>()
                .ForMember(dest => dest.CertWord, opt => opt.MapFrom(src => src.CertificateWord.CertWord))
                .ForMember(dest => dest.CwId, opt => opt.MapFrom(src => src.CertificateWord.CwId));
                cfg.CreateMap<VoucherDetail, VoucherDetailViewModel>()
                .ForMember(dest => dest.AccId, opt => opt.MapFrom(src => src.AccId));
                cfg.CreateMap<SourceDocument, VoucherInvoiceViewModel>();

                cfg.CreateMap<AbstractTemp, AbstractViewModel>();



                //============BizProgress===============
                cfg.CreateMap<BizCategory, BizCategoryViewModel>();
                cfg.CreateMap<BizItems, BizItemViewModel>()
                .ForMember(dest => dest.CateId, opt => opt.MapFrom(src => src.BizCategory.CateId))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.BizCategory.CategoryName));
                cfg.CreateMap<BizSteps, BizStepsViewModel>();

                cfg.CreateMap<Customers, BizCustomerViewModel>();
                cfg.CreateMap<WorkOrder, WorkOrderViewModel>()
                .ForMember(dest => dest.BizItemNames, opt => opt.ResolveUsing<BizItemNamesResolver>())
                .ForMember(dest => dest.BizItemIds, opt => opt.ResolveUsing<BizItemIdsResolver>())
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName));
                cfg.CreateMap<WorkProgress, WorkProgressViewModel>()
                .ForMember(dest => dest.StepName, opt => opt.MapFrom(src => src.BizStep.StepName))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Images.Count > 0 ? src.Images.FirstOrDefault().Url : ""))
                .ForMember(dest=>dest.FileName,opt=>opt.MapFrom(src=>src.Images.Count>0?src.Images.FirstOrDefault().AliyunKey:""));

            });
        }
    }


    public class BizItemNamesResolver : IValueResolver<WorkOrder, WorkOrderViewModel, string>
    {
        public string Resolve(WorkOrder source, WorkOrderViewModel destination, string destMember, ResolutionContext context)
        {
            string itemNames = "";
            foreach(WorkOrderItem woi in source.WorkOrderItems)
            {
                if (itemNames != "") itemNames += ",<br>";

                itemNames += woi.BizItem.ItemName;
            }
            return itemNames;
        }
    }

    public class BizItemIdsResolver : IValueResolver<WorkOrder, WorkOrderViewModel, string>
    {
        public string Resolve(WorkOrder source, WorkOrderViewModel destination, string destMember, ResolutionContext context)
        {
            string itemIds = "";
            foreach (WorkOrderItem woi in source.WorkOrderItems)
            {
                if (itemIds != "") itemIds += ",";

                itemIds += woi.ItemId.ToString();
            }
            return itemIds;
        }
    }

}