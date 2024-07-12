using GustoGlide.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GustoGlide.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Coupon> Coupons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "SALE10",
                DiscountAmount = 10,
                MinAmount = 30
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "SALE20",
                DiscountAmount = 20,
                MinAmount = 40
            });
        }
    }
}
