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
                cfg.CreateMap<BizItems, BizItemViewModel>();
                cfg.CreateMap<BizSteps, BizStepsViewModel>();

                cfg.CreateMap<Customers, BizCustomerViewModel>();
                cfg.CreateMap<BizProgress, BizProgressViewModel>();

            });
        }
    }

    
}