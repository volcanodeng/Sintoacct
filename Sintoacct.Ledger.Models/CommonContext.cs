using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sintoacct.Ledger.Models
{
    public class CommonContext : DbContext
    {
        public CommonContext() : base("name=DefaultConnection") { }

        public DbSet<ExceptionLog> Exceptions { get; set; }

        
    }
}
