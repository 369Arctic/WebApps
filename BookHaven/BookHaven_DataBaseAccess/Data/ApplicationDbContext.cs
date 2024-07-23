using BookHaven_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookHaven_DatabaseAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext <IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Action",
                    DisplayOrder = 1
                },
                new Category
                {
                    Id = 2,
                    Name = "History",
                    DisplayOrder = 2
                },
                new Category
                {
                    Id = 3,
                    Name = "Detective",
                    DisplayOrder = 3
                });


            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = 1,
                   Name = "1984",
                   Description = "The main character, Winston Smith, lives in London and works in the Ministry of Truth. Smith is a citizen of the government where all people are under total surveillance and any free-thinking is punished. Winston does not share these ideas, but he carefully hides it, because anyone who is uncovered undergoes an unavoidable 'healing' equivalent to mental death.",
                   Author = "George Orwell",
                   ISBN = "978-5-17-150507-3",
                   ImageURL="",
                   YearOfPublishing = 1949,
                   ListPrice = 99,
                   Price = 300,
                   Price50 = 250,
                   CategoryId = 1
               },
               new Product
               {
                   Id = 2,
                   Name = "Norwegian Wood",
                   Description = "The book tells the story of Toru Watanabe, a university student in Tokyo, who reflects on his youth in the 1960s. The main themes of the book are love, loss, loneliness, and the search for meaning in life. Throughout the narrative, Toru experiences complicated relationships with two different women - Naoko, a mysterious and troubled girl with a dark past, and Midori, a vibrant and lively young woman. The novel \"Norwegian Wood\" is considered one of Haruki Murakami's most well-known and popular works, attracting readers with its depth and poignant sensibility.",
                   Author = "Haruki Murakami",
                   ISBN = "9780099448822",
                   ImageURL = "",
                   YearOfPublishing = 1987,
                   ListPrice = 149,
                   Price = 140,
                   Price50 = 125,
                   CategoryId = 2
               },
               new Product
               {
                   Id = 3,
                   Name = "Schindler's List",
                   Description = "A stunning novel based on the true story of how German war profiteer and factory director Oskar Schindler came to save more Jews from the gas chambers than any other single person during World War II. In this milestone of Holocaust literature, Thomas Keneally, author of Daughter of Mars, uses the actual testimony of the Schindlerjuden—Schindler’s Jews—to brilliantly portray the courage and cunning of a good man in the midst of unspeakable evil.",
                   Author = "Thomas Keneally",
                   ISBN = "0671880314",
                   ImageURL = "",
                   YearOfPublishing = 1993,
                   ListPrice = 499,
                   Price = 490,
                   Price50 = 470,
                   CategoryId = 3
               });
        }
    }
}
