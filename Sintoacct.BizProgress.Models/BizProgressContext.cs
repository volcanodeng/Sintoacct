using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sintoacct.BizProgress.Models
{
    public class BizProgressContext : DbContext
    {
        public BizProgressContext() : base("name=BizProgressConnection") { }

        public DbSet<BizProgress> BizProgress { get; set; }

        public DbSet<BizCategory> BizCategories { get; set; }

        public DbSet<BizSteps> BizSteps { get; set; }

        public DbSet<ProgressImage> ProgressImages { get; set; }

        public DbSet<BizPromotion> BizPromotions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BizProgress>().HasRequired(s => s.BizStep).WithMany(p => p.BizProgress).WillCascadeOnDelete(false);
            modelBuilder.Entity<BizProgress>().HasRequired(s => s.SubCategory).WithMany(p => p.BizProgress).WillCascadeOnDelete(false);
            modelBuilder.Entity<BizProgress>().HasRequired(s => s.BizCategory).WithMany(p => p.BizProgress).WillCascadeOnDelete(false);
            modelBuilder.Entity<BizProgress>().HasRequired(s => s.Customer).WithMany(p => p.BizProgress).WillCascadeOnDelete(false);
        }
    }
}
