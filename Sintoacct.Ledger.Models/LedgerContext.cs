using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Sintoacct.Ledger.Models
{
    public class LedgerContext : DbContext
    {
        public LedgerContext() : base("name=DefaultConnection") {}

        public DbSet<AccountBook> AccountBooks { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountCategory> AccountCategories { get; set; }

        public DbSet<CertificateWord> CertificateWords { get; set; }

        public DbSet<AuxiliaryType> AuxiliaryType { get; set; }

        public DbSet<Auxiliary> Auxiliarys { get; set; }

        public DbSet<AbstractTemp> AbstractTemps { get; set; }

        public DbSet<UserBook> UserBooks { get; set; }

        public DbSet<Company> Companys { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<SourceDocument> SourceDocument { get; set; }
    }
}
