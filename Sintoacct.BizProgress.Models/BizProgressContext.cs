using System.Data.Entity;

namespace Sintoacct.Progress.Models
{
    public class BizProgressContext : DbContext
    {
        public BizProgressContext() : base("name=BizProgressConnection") { }

        public DbSet<WorkOrder> BizProgress { get; set; }

        public DbSet<BizCategory> BizCategories { get; set; }

        public DbSet<BizItems> BizItems { get; set; }

        public DbSet<BizSteps> BizSteps { get; set; }

        public DbSet<ProgressImage> ProgressImages { get; set; }

        public DbSet<BizPromotion> BizPromotions { get; set; }

        public DbSet<Customers> Customers { get; set; }

        public DbSet<WorkOrderPayment> CustomerCost { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<WorkOrder>().HasRequired(s => s.BizStep).WithMany(p => p.BizProgress).WillCascadeOnDelete(false);
            //modelBuilder.Entity<WorkOrder>().HasRequired(s => s.BizItem).WithMany(p => p.BizProgress).WillCascadeOnDelete(false);
            //modelBuilder.Entity<WorkOrder>().HasRequired(s => s.BizCategory).WithMany(p => p.BizProgress).WillCascadeOnDelete(false);
            //modelBuilder.Entity<WorkOrder>().HasRequired(s => s.Customer).WithMany(p => p.BizProgress).WillCascadeOnDelete(false);
        }
    }
}
