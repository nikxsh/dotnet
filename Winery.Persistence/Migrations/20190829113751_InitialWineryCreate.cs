using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WineryStore.Persistence.Migrations
{
    public partial class InitialWineryCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wineries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wineries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Color = table.Column<int>(nullable: false),
                    Vintage = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    IssueDate = table.Column<string>(nullable: true),
                    Rank = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    WineryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wines_Wineries_WineryId",
                        column: x => x.WineryId,
                        principalTable: "Wineries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Wineries",
                columns: new[] { "Id", "Country", "Name", "Region" },
                values: new object[,]
                {
                    { new Guid("b3dc052c-c3a2-4607-9ca5-20e6deda3fc2"), "California", "Duckhorn", "Napa" },
                    { new Guid("0a634ffd-874b-4796-8875-02181b0ebcc8"), "France", "Château Coutet", "Bordeaux" },
                    { new Guid("b39c7530-622d-456e-afb6-760f3653f076"), "Italy", "Casanova di Neri", "Tuscany" },
                    { new Guid("2e085a4d-a5d8-4352-8908-1b2103437885"), "Spain", "Bodegas Godeval", "Spain" },
                    { new Guid("d2835e85-89ab-4b23-a5d5-48db93f778dd"), "India", "Sula", "Nashik" }
                });

            migrationBuilder.InsertData(
                table: "Wines",
                columns: new[] { "Id", "Color", "IssueDate", "Name", "Note", "Price", "Rank", "Score", "Vintage", "WineryId" },
                values: new object[,]
                {
                    { new Guid("9293677d-a36b-46ce-b2a0-9173858156eb"), 0, "Nov 30, 2017", "Merlot Napa Valley Three Palms Vineyard", "A powerful red, with concentrated flavors of red plum, cherry and boysenberry that are layered with plenty of rich spice and mineral accents. Touches of slate and cardamom make for a complex finish. Drink now through 2023. 3,170 cases made.", 98, 1, 95, "2014", new Guid("b3dc052c-c3a2-4607-9ca5-20e6deda3fc2") },
                    { new Guid("961bd0d0-a0eb-43c8-b076-441e47fdc847"), 0, "Jul 31, 1989", "Cabernet Sauvignon Napa Valley", "On first sniff, this reveals itself as the complex wine it is, with smooth texture and deep plum, cherry, tar and vanilla flavors, long and perfumed. Drinkable now. 4,000 cases made.", 18, 32, 94, "1986", new Guid("b3dc052c-c3a2-4607-9ca5-20e6deda3fc2") },
                    { new Guid("18cc07e8-712e-4830-bceb-26f75f2ad3ab"), 4, "Mar 31, 2017", "Barsac", "This shows the vivid, racy side of Barsac, with streaming flavors of pineapple, yellow apple, green plum and white ginger, displaying lovely energy from start to finish. Ends with enough honeysuckle and orange blossom notes to balance the richness. Best from 2020 through 2035. 4,000 cases made.", 37, 3, 96, "2014", new Guid("0a634ffd-874b-4796-8875-02181b0ebcc8") },
                    { new Guid("05e45cf9-fefc-408c-97a0-81b3598ef415"), 0, "Jun 15, 2017", "Brunello di Montalcino", "Effusive aromas and flavors of raspberry, cherry, floral, mineral and tobacco are at the center of this linear, vibrant red. Well-structured, this offers terrific length on the sinewy finish. Best from 2020 through 2035. 6,054 cases made.", 65, 4, 95, "2012", new Guid("b39c7530-622d-456e-afb6-760f3653f076") },
                    { new Guid("9c2e678d-9af2-48fa-a2be-40b6238c5dce"), 1, "Jun 15, 2015", "Valdeorras ViÃ±a Godeval Cepas Vellas", "This alluring white delivers a broad range of flavors in a pillowy texture, while crisp, well-integrated acidity maintains the focus. Melon, coconut, spice and smoke flavors mingle harmoniously on the plush palate. The mineral element is fresh and long. Godello. Drink now through 2018. 1,800 cases imported.", 20, 36, 92, "2013", new Guid("2e085a4d-a5d8-4352-8908-1b2103437885") },
                    { new Guid("dc8fdc32-b96b-4ea6-a4c9-f2514b183a53"), 1, "Sep 30, 2016", "Valdeorras ViÃ±a Godeval Cepas Vellas", "Pear, peach and quince flavors mingle in this expressive white, while notes of mineral, tangerine and ginger add complexity. Shows depth and focus, with a clean, juicy finish. Drink now. 1,800 cases imported.", 20, 75, 91, "2014", new Guid("2e085a4d-a5d8-4352-8908-1b2103437885") },
                    { new Guid("a05164df-2bdb-4675-9afa-9f5cbd9e69bd"), 0, "Jun 15, 2018", "Rasa Shiraz", "It is a complex wine, with power and finesse. Crafted from handpicked grapes from our own estate vineyards, Rasa Shiraz is aged for 12 months in premium French oak barrels and then further matured in the bottle before release.", 20, 36, 92, "2015", new Guid("d2835e85-89ab-4b23-a5d5-48db93f778dd") },
                    { new Guid("60e79b55-b2e8-45f7-9b89-6f9de315d12e"), 1, "Sep 30, 2017", "Dindori Reserve Viognier", "Dindori Reserve Viognier is an exotic elixir of peach and lychee flavours. Floral, spicy, stunning. Great as an aperitif and terrific with spicy food. Serve well chilled.", 20, 75, 91, "2014", new Guid("d2835e85-89ab-4b23-a5d5-48db93f778dd") },
                    { new Guid("8610c9aa-704e-4a10-99f3-086b980a8c4f"), 2, "Sep 30, 2016", "Zinfandel Rosé", "Zinfandel Rose is ripe, fresh and fruity, with abundant aromas of cranberries and fresh strawberries. A versatile “anytime” wine great for picnics, parties and hot summer days. Lovely with poultry and spicy dishes. Serve well chilled.", 16, 100, 91, "2015", new Guid("d2835e85-89ab-4b23-a5d5-48db93f778dd") },
                    { new Guid("d9272018-dae4-4f1a-badf-b195c27bb057"), 5, "Sep 30, 2019", "Brut", "A complex blend of Chenin Blanc, Chardonnay, Viognier, Pinot Noir, Riesling and Shiraz. This is one of the few “Méthode Champenoise” wines in the world to be crafted from six different grapes, resulting in something remarkable", 41, 12, 91, "2017", new Guid("d2835e85-89ab-4b23-a5d5-48db93f778dd") },
                    { new Guid("9fb0680a-582c-4aa8-ae7f-21464b5bc5ca"), 4, "Sep 30, 2019", "Late Harvest Chenin Blanc", "Abounding with aromas of mango, honey and tropical fruit, our award winning Late Harvest Chenin Blanc is the perfect close to a delicious meal, but is also an elegant aperiti", 15, 5, 91, "2019", new Guid("d2835e85-89ab-4b23-a5d5-48db93f778dd") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wines_WineryId",
                table: "Wines",
                column: "WineryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wines");

            migrationBuilder.DropTable(
                name: "Wineries");
        }
    }
}
