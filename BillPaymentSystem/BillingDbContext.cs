using Microsoft.EntityFrameworkCore;
using static BillPaymentSystem.Models.DataModel;

namespace BillPaymentSystem
{
   
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options) { }

        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Subscriber)
                .WithMany(s => s.Bills)
                .HasForeignKey(b => b.SubscriberId);
            modelBuilder.Entity<Bill>()
            .Property(b => b.TotalAmount)
            .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Subscriber>().HasData(
             new Subscriber { Id = 1, SubscriberNo = "10" },
             new Subscriber { Id = 2, SubscriberNo = "20" },
             new Subscriber { Id = 3, SubscriberNo = "30" },
             new Subscriber { Id = 4, SubscriberNo = "40" },
             new Subscriber { Id = 5, SubscriberNo = "50" },
             new Subscriber { Id = 6, SubscriberNo = "60" },
             new Subscriber { Id = 7, SubscriberNo = "70" }
);
            modelBuilder.Entity<Bill>().HasData(
                new Bill { Id = 1, SubscriberId = 1, IsPaid = true, Month = new DateTime(2024, 1, 1), TotalAmount = 260 },
                new Bill { Id = 2, SubscriberId = 1, IsPaid = false, Month = new DateTime(2024, 2, 1), TotalAmount = 260 },
                new Bill { Id = 3, SubscriberId = 2, IsPaid = true, Month = new DateTime(2024, 1, 1), TotalAmount = 260 },
                new Bill { Id = 4, SubscriberId = 2, IsPaid = false, Month = new DateTime(2024, 2, 1), TotalAmount = 260 },
                new Bill { Id = 5, SubscriberId = 3, IsPaid = false, Month = new DateTime(2024, 2, 1), TotalAmount = 260 },
                new Bill { Id = 6, SubscriberId = 3, IsPaid = true, Month = new DateTime(2024, 1, 1), TotalAmount = 260 },
                new Bill { Id = 7, SubscriberId = 4, IsPaid = false, Month = new DateTime(2024, 2, 1), TotalAmount = 260 },
                new Bill { Id = 8, SubscriberId = 4, IsPaid = true, Month = new DateTime(2024, 1, 1), TotalAmount = 260 },
                new Bill { Id = 9, SubscriberId = 5, IsPaid = false, Month = new DateTime(2024, 2, 1), TotalAmount = 260 },
                new Bill { Id = 10, SubscriberId = 5, IsPaid = true, Month = new DateTime(2024, 1, 1), TotalAmount = 260 },
                new Bill { Id = 11, SubscriberId = 6, IsPaid = false, Month = new DateTime(2024, 2, 1), TotalAmount = 260 },
                new Bill { Id = 12, SubscriberId = 6, IsPaid = true, Month = new DateTime(2024, 1, 1), TotalAmount = 260 },
                new Bill { Id = 13, SubscriberId = 7, IsPaid = false, Month = new DateTime(2024, 2, 1), TotalAmount = 260 },
                new Bill { Id = 14, SubscriberId = 7, IsPaid = true, Month = new DateTime(2024, 1, 1), TotalAmount = 260 },
                new Bill { Id = 15, SubscriberId = 7, IsPaid = true, Month = new DateTime(2024, 3, 1), TotalAmount = 260 }


                );
        }
    }
}


