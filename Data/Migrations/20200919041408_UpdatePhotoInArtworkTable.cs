using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webgalleriet.Data.Migrations
{
    public partial class UpdatePhotoInArtworkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>("PhotoUrl", "Artwork", nullable: true);
            migrationBuilder.DropColumn("Photo", "Artwork");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
