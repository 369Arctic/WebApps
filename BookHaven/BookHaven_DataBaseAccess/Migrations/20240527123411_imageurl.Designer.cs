﻿// <auto-generated />
using BookHaven_DatabaseAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookHaven_DataBaseAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240527123411_imageurl")]
    partial class imageurl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookHaven_Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "History"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Detective"
                        });
                });

            modelBuilder.Entity("BookHaven_Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<int>("YearOfPublishing")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "George Orwell",
                            CategoryId = 1,
                            Description = "The main character, Winston Smith, lives in London and works in the Ministry of Truth. Smith is a citizen of the government where all people are under total surveillance and any free-thinking is punished. Winston does not share these ideas, but he carefully hides it, because anyone who is uncovered undergoes an unavoidable 'healing' equivalent to mental death.",
                            ISBN = "978-5-17-150507-3",
                            ImageURL = "",
                            ListPrice = 99.0,
                            Name = "1984",
                            Price = 300.0,
                            Price50 = 250.0,
                            YearOfPublishing = 1949
                        },
                        new
                        {
                            Id = 2,
                            Author = "Haruki Murakami",
                            CategoryId = 2,
                            Description = "The book tells the story of Toru Watanabe, a university student in Tokyo, who reflects on his youth in the 1960s. The main themes of the book are love, loss, loneliness, and the search for meaning in life. Throughout the narrative, Toru experiences complicated relationships with two different women - Naoko, a mysterious and troubled girl with a dark past, and Midori, a vibrant and lively young woman. The novel \"Norwegian Wood\" is considered one of Haruki Murakami's most well-known and popular works, attracting readers with its depth and poignant sensibility.",
                            ISBN = "9780099448822",
                            ImageURL = "",
                            ListPrice = 149.0,
                            Name = "Norwegian Wood",
                            Price = 140.0,
                            Price50 = 125.0,
                            YearOfPublishing = 1987
                        },
                        new
                        {
                            Id = 3,
                            Author = "Thomas Keneally",
                            CategoryId = 3,
                            Description = "A stunning novel based on the true story of how German war profiteer and factory director Oskar Schindler came to save more Jews from the gas chambers than any other single person during World War II. In this milestone of Holocaust literature, Thomas Keneally, author of Daughter of Mars, uses the actual testimony of the Schindlerjuden—Schindler’s Jews—to brilliantly portray the courage and cunning of a good man in the midst of unspeakable evil.",
                            ISBN = "0671880314",
                            ImageURL = "",
                            ListPrice = 499.0,
                            Name = "Schindler's List",
                            Price = 490.0,
                            Price50 = 470.0,
                            YearOfPublishing = 1993
                        });
                });

            modelBuilder.Entity("BookHaven_Models.Product", b =>
                {
                    b.HasOne("BookHaven_Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
