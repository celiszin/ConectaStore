using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConectaStore.API.Migrations
{
    /// <inheritdoc />
    public partial class InicialCriacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Cor = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    Qtde = table.Column<int>(type: "int", nullable: false),
                    ValorCusto = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    ValorVenda = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Destaque = table.Column<bool>(type: "bit", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Cor", "Foto", "Nome" },
                values: new object[,]
                {
                    { 1, "#FF5733", null, "Smartphones" },
                    { 2, "#33FF57", null, "Notebooks" },
                    { 3, "#3357FF", null, "SmartWatches" },
                    { 4, "#FF33A1", null, "Fones de Ouvido" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaId", "Descricao", "Destaque", "Foto", "Nome", "Qtde", "ValorCusto", "ValorVenda" },
                values: new object[,]
                {
                    { 1, 1, "O iPhone 17 Pro é o mais recente lançamento da Apple, trazendo um design elegante e recursos avançados. Com uma tela Super Retina XDR de 6,1 polegadas, o dispositivo oferece uma experiência visual imersiva. Equipado com o chip A17 Bionic, o iPhone 17 Pro proporciona desempenho excepcional e eficiência energética. O sistema de câmera tripla inclui uma lente principal de 12 MP, uma lente ultra-angular de 12 MP e uma lente telefoto de 12 MP, permitindo fotos e vídeos de alta qualidade. Além disso, o iPhone 17 Pro suporta carregamento rápido e possui resistência à água e poeira IP68.", true, "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/iphone-17-pro-gold-select?wid=940&hei=1112&fmt=png-alpha&.v=1700792800", "iPhone 17 Pro", 0, 1000m, 10000.50m },
                    { 2, 2, "O MacBook Air é o notebook mais leve e portátil da Apple, ideal para uso diário e produtividade. Com o chip M2, oferece desempenho excepcional e eficiência energética. A tela Retina de 13,6 polegadas proporciona uma experiência visual imersiva. O MacBook Air é leve, silencioso e oferece até 18 horas de autonomia.", true, "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/macbook-air-m2-select?wid=940&hei=1112&fmt=png-alpha&.v=1700792800", "MacBook Air", 0, 2000m, 20000.99m },
                    { 3, 3, "O Apple Watch Series 9 é o smartwatch mais avançado da Apple, oferecendo uma combinação perfeita de estilo e funcionalidade. Com um design elegante e uma tela Retina sempre ativa, o Series 9 é ideal para monitorar sua saúde e fitness. Ele possui recursos como monitoramento de frequência cardíaca, rastreamento de atividades físicas, detecção de quedas e suporte a aplicativos de terceiros. O Apple Watch Series 9 é resistente à água e oferece uma bateria de longa duração para mantê-lo conectado durante todo o dia.", true, "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/apple-watch-series-9-gps-select?wid=940&hei=1112&fmt=png-alpha&.v=1700792800", "Apple Watch Series 9", 0, 300m, 2500.00m },
                    { 4, 4, "Os AirPods Pro são os fones de ouvido sem fio premium da Apple, oferecendo uma experiência de áudio imersiva e cancelamento ativo de ruído. Com um design compacto e confortável, os AirPods Pro se encaixam perfeitamente nos ouvidos. Eles possuem um modo de transparência que permite ouvir o ambiente ao seu redor quando necessário. Os AirPods Pro são resistentes à água e suor, tornando-os ideais para atividades físicas. Com uma bateria de longa duração e suporte a carregamento sem fio, os AirPods Pro são a escolha perfeita para quem busca qualidade de áudio e conveniência.", true, "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/MWP22?wid=940&hei=1112&fmt=png-alpha&.v=1700792800", "AirPods Pro", 0, 700m, 5600.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
