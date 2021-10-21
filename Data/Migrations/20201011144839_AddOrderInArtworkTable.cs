using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webgalleriet.Data.Migrations
{
    public partial class AddOrderInArtworkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>("DisplayOrder", "Artwork", nullable: false, defaultValue: false);           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("DisplayOrder", "Artwork");              
        }
    }
}
