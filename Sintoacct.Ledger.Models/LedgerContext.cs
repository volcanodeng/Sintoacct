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
        public LedgerContext() : base("name=DefaultConnection") { }

        public DbSet<AccountBook> AccountBooks { get; set; }

        public DbSet<AbstractTemp> AbstractTemps { get; set; }
    }
}
