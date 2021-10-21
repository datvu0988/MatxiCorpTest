using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webgalleriet.Data.Migrations
{
    public partial class Initital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultMax",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 18, 40, 58, 212, DateTimeKind.Local).AddTicks(8129)),
                    Type = table.Column<string>(maxLength: 50, nullable: true),
                    MaxValue = table.Column<int>(nullable: false),
                    GalleryTypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultMax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 18, 40, 58, 211, DateTimeKind.Local).AddTicks(6540)),
                    Type = table.Column<string>(maxLength: 50, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    ExternalWebsiteURL = table.Column<string>(nullable: true),
                    MaxGalleriesNum = table.Column<int>(nullable: false, defaultValue: 1),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artist_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 18, 40, 58, 212, DateTimeKind.Local).AddTicks(9585)),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    MaxRoomNumber = table.Column<int>(nullable: false, defaultValue: 3),
                    TypeId = table.Column<Guid>(nullable: false),
                    ArtistId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gallery_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gallery_GalleryType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "GalleryType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 18, 40, 58, 213, DateTimeKind.Local).AddTicks(1987)),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    MaxPhotoNum = table.Column<int>(nullable: false, defaultValue: 15),
                    GalleryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Room_Gallery_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Gallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artwork",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 18, 40, 58, 213, DateTimeKind.Local).AddTicks(4233)),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    DeliveryInformation = table.Column<string>(maxLength: 500, nullable: true),
                    IsForFrontpage = table.Column<bool>(nullable: false, defaultValue: false),
                    Photo = table.Column<byte[]>(nullable: true),
                    RoomId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artwork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artwork_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DefaultMax",
                columns: new[] { "Id", "Created", "GalleryTypeId", "MaxValue", "Type" },
                values: new object[,]
                {
                    { new Guid("71b41826-abf2-45d8-9dd0-2efb2e0d5bd1"), new DateTime(2020, 9, 17, 18, 40, 58, 215, DateTimeKind.Local).AddTicks(4689), null, 1, "Gallery" },
                    { new Guid("bc20fc64-20a2-478e-bc13-ae3170966883"), new DateTime(2020, 9, 17, 18, 40, 58, 215, DateTimeKind.Local).AddTicks(6972), new Guid("5a7b2e33-a96b-423b-96c9-3678a3772f51"), 3, "Room" },
                    { new Guid("5ad98f84-b048-4c2f-8a79-c70658e28e7d"), new DateTime(2020, 9, 17, 18, 40, 58, 215, DateTimeKind.Local).AddTicks(7095), new Guid("5a7b2e33-a96b-423b-96c9-3678a3772f51"), 15, "ArtWork" },
                    { new Guid("9daac1d8-7643-49df-8676-8c19fbe2c655"), new DateTime(2020, 9, 17, 18, 40, 58, 215, DateTimeKind.Local).AddTicks(7099), new Guid("4a535a0d-6435-4e44-97d7-421adff35c74"), 3, "Room" },
                    { new Guid("a5a8983e-7498-4055-a447-c07c23dfa22a"), new DateTime(2020, 9, 17, 18, 40, 58, 215, DateTimeKind.Local).AddTicks(7101), new Guid("4a535a0d-6435-4e44-97d7-421adff35c74"), 15, "ArtWork" },
                    { new Guid("ed78a3ad-2df7-4cb9-af74-187f5623811a"), new DateTime(2020, 9, 17, 18, 40, 58, 215, DateTimeKind.Local).AddTicks(7104), new Guid("7bc5eb67-6c4b-4305-a41c-a8d11bb2b32c"), 3, "Room" },
                    { new Guid("a4ae6e76-759b-4353-aaaa-510c91a813c1"), new DateTime(2020, 9, 17, 18, 40, 58, 215, DateTimeKind.Local).AddTicks(7106), new Guid("7bc5eb67-6c4b-4305-a41c-a8d11bb2b32c"), 15, "ArtWork" }
                });

            migrationBuilder.InsertData(
                table: "GalleryType",
                columns: new[] { "Id", "Active", "Created", "Order", "Type" },
                values: new object[,]
                {
                    { new Guid("5a7b2e33-a96b-423b-96c9-3678a3772f51"), true, new DateTime(2020, 9, 17, 18, 40, 58, 213, DateTimeKind.Local).AddTicks(9884), 1, "Fotografi" },
                    { new Guid("4a535a0d-6435-4e44-97d7-421adff35c74"), false, new DateTime(2020, 9, 17, 18, 40, 58, 214, DateTimeKind.Local).AddTicks(1833), 2, "Maleri" },
                    { new Guid("7bc5eb67-6c4b-4305-a41c-a8d11bb2b32c"), false, new DateTime(2020, 9, 17, 18, 40, 58, 214, DateTimeKind.Local).AddTicks(1870), 3, "Keramikk" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artist_UserId",
                table: "Artist",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_RoomId",
                table: "Artwork",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_ArtistId",
                table: "Gallery",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_TypeId",
                table: "Gallery",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_GalleryId",
                table: "Room",
                column: "GalleryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artwork");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DefaultMax");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Gallery");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "GalleryType");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
