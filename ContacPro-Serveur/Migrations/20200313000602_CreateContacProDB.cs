using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContacPro_Serveur.Migrations
{
    public partial class CreateContacProDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expertises",
                columns: table => new
                {
                    Id_expertise = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valeur = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Expertis__9F8DB893847A1F2A", x => x.Id_expertise);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id_tag = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valeur = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tags__2D1436F3C199AA3D", x => x.Id_tag);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    UtilisateurID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    prenom = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    addr_photo = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    mdp = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    courriel = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Institution = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    addr_cv = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.UtilisateurID);
                });

            migrationBuilder.CreateTable(
                name: "Ententes",
                columns: table => new
                {
                    Id_entente = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pro_ID = table.Column<int>(nullable: false),
                    Client_ID = table.Column<int>(nullable: false),
                    Approuve_pro = table.Column<bool>(nullable: false),
                    Approuve_client = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Entente__1CC5D80D0E446E07", x => x.Id_entente);
                    table.ForeignKey(
                        name: "fk_entente_client",
                        column: x => x.Client_ID,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_entente_pro",
                        column: x => x.Pro_ID,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prestations",
                columns: table => new
                {
                    PrestationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Lieu = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Type = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Retribution = table.Column<decimal>(type: "decimal(6, 2)", nullable: true),
                    Date = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Client_Id = table.Column<int>(nullable: false),
                    Pro_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prestati__52B08CDD15B0DEDA", x => x.PrestationID);
                    table.ForeignKey(
                        name: "fk_prest_id_client",
                        column: x => x.Client_Id,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_prest_id_pro",
                        column: x => x.Pro_Id,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProExps",
                columns: table => new
                {
                    UtilisateurID = table.Column<int>(nullable: false),
                    ExpertiseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProExps", x => new { x.ExpertiseID, x.UtilisateurID });
                    table.ForeignKey(
                        name: "fk_pro_exp_Id_exp",
                        column: x => x.ExpertiseID,
                        principalTable: "Expertises",
                        principalColumn: "Id_expertise",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pro_exp_Id_pro",
                        column: x => x.UtilisateurID,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Auteur_Id = table.Column<int>(nullable: false),
                    Destinataire_Id = table.Column<int>(nullable: false),
                    Titre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Contenu = table.Column<string>(type: "text", nullable: false),
                    Lu = table.Column<bool>(nullable: false),
                    Entente_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__0A1524C05E7B23AE", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Messages_Utilisateurs_Auteur_Id",
                        column: x => x.Auteur_Id,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Utilisateurs_Destinataire_Id",
                        column: x => x.Destinataire_Id,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_message_entente",
                        column: x => x.Entente_ID,
                        principalTable: "Ententes",
                        principalColumn: "Id_entente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tags_Prestations",
                columns: table => new
                {
                    PrestationID = table.Column<int>(nullable: false),
                    TagID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags_Prestations", x => new { x.PrestationID, x.TagID });
                    table.ForeignKey(
                        name: "fk_tags_prest",
                        column: x => x.PrestationID,
                        principalTable: "Prestations",
                        principalColumn: "PrestationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tags",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "Id_tag",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ententes_Client_ID",
                table: "Ententes",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Ententes_Pro_ID",
                table: "Ententes",
                column: "Pro_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Auteur_Id",
                table: "Messages",
                column: "Auteur_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Destinataire_Id",
                table: "Messages",
                column: "Destinataire_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Entente_ID",
                table: "Messages",
                column: "Entente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Prestations_Client_Id",
                table: "Prestations",
                column: "Client_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prestations_Pro_Id",
                table: "Prestations",
                column: "Pro_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProExps_UtilisateurID",
                table: "ProExps",
                column: "UtilisateurID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Prestations_TagID",
                table: "Tags_Prestations",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "UQ__Utilisat__049FB202EAE654B1",
                table: "Utilisateurs",
                column: "courriel",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ProExps");

            migrationBuilder.DropTable(
                name: "Tags_Prestations");

            migrationBuilder.DropTable(
                name: "Ententes");

            migrationBuilder.DropTable(
                name: "Expertises");

            migrationBuilder.DropTable(
                name: "Prestations");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
