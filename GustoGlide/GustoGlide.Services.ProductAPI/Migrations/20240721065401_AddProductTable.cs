using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GustoGlide.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Pizza", "Баварские колбаски , соус аджика, острые колбаски чоризо , цыпленок , пикантная пепперони , моцарелла, фирменный томатный соус", "https://placehold.co/603x403", "Мясная с аджикой", 500.0 },
                    { 2, "Pizza", "Креветки, ананасы, соус сладкий чили, сладкий перец, моцарелла, фирменный соус альфредо", "https://placehold.co/603x403", "Креветки со сладким чилий", 550.0 },
                    { 3, "Pizza", "Моцарелла, сыры чеддер и пармезан, фирменный соус альфредо", "https://placehold.co/603x403", "Сырная", 300.0 },
                    { 4, "Pizza", "Цыпленок, моцарелла, фирменный соус альфредо", "https://placehold.co/603x403", "Двойной цыпленок", 420.0 },
                    { 5, "Pizza", "Ветчина, моцарелла, фирменный соус альфредо", "https://placehold.co/603x403", "Ветчина и сыр", 420.0 },
                    { 6, "Pizza", "Острые колбаски чоризо, сладкий перец, моцарелла, фирменный томатный соус", "https://placehold.co/603x403", "Чоризо фреш", 420.0 }
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
