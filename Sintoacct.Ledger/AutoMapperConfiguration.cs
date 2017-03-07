using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<AccountBook, AcctBookListViewModels>()
                .ForMember(dest=>dest.CompanyName,opt=>opt.MapFrom(src=>src.Company.ComName));
                
            });
        }
    }

    
}