using GustoGlide.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GustoGlide.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Мясная с аджикой",
                Description = "Баварские колбаски , соус аджика, острые колбаски чоризо , цыпленок , пикантная пепперони , моцарелла, фирменный томатный соус",
                CategoryName = "Pizza",
                ImageUrl = "https://placehold.co/603x403",
                Price = 500
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Креветки со сладким чилий",
                Description = "Креветки, ананасы, соус сладкий чили, сладкий перец, моцарелла, фирменный соус альфредо",
                CategoryName = "Pizza",
                ImageUrl = "https://placehold.co/603x403",
                Price = 550
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Сырная",
                Description = "Моцарелла, сыры чеддер и пармезан, фирменный соус альфредо",
                CategoryName = "Pizza",
                ImageUrl = "https://placehold.co/603x403",
                Price = 300
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Двойной цыпленок",
                Description = "Цыпленок, моцарелла, фирменный соус альфредо",
                CategoryName = "Pizza",
                ImageUrl = "https://placehold.co/603x403",
                Price = 420
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "Ветчина и сыр",
                Description = "Ветчина, моцарелла, фирменный соус альфредо",
                CategoryName = "Pizza",
                ImageUrl = "https://placehold.co/603x403",
                Price = 420
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 6,
                Name = "Чоризо фреш",
                Description = "Острые колбаски чоризо, сладкий перец, моцарелла, фирменный томатный соус",
                CategoryName = "Pizza",
                ImageUrl = "https://placehold.co/603x403",
                Price = 420
            });
        }
    }
}
