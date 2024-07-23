using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookHaven_DataBaseAccess.Migrations
{
    /// <inheritdoc />
    public partial class addProductsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfPublishing = table.Column<int>(type: "int", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Name", "Price", "Price50", "YearOfPublishing" },
                values: new object[,]
                {
                    { 1, "George Orwell", "The main character, Winston Smith, lives in London and works in the Ministry of Truth. Smith is a citizen of the government where all people are under total surveillance and any free-thinking is punished. Winston does not share these ideas, but he carefully hides it, because anyone who is uncovered undergoes an unavoidable 'healing' equivalent to mental death.", "978-5-17-150507-3", 99.0, "1984", 300.0, 250.0, 1949 },
                    { 2, "Haruki Murakami", "The book tells the story of Toru Watanabe, a university student in Tokyo, who reflects on his youth in the 1960s. The main themes of the book are love, loss, loneliness, and the search for meaning in life. Throughout the narrative, Toru experiences complicated relationships with two different women - Naoko, a mysterious and troubled girl with a dark past, and Midori, a vibrant and lively young woman. The novel \"Norwegian Wood\" is considered one of Haruki Murakami's most well-known and popular works, attracting readers with its depth and poignant sensibility.", "9780099448822", 149.0, "Norwegian Wood", 140.0, 125.0, 1987 },
                    { 3, "Thomas Keneally", "A stunning novel based on the true story of how German war profiteer and factory director Oskar Schindler came to save more Jews from the gas chambers than any other single person during World War II. In this milestone of Holocaust literature, Thomas Keneally, author of Daughter of Mars, uses the actual testimony of the Schindlerjuden—Schindler’s Jews—to brilliantly portray the courage and cunning of a good man in the midst of unspeakable evil.", "0671880314", 499.0, "Schindler's List", 490.0, 470.0, 1993 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
