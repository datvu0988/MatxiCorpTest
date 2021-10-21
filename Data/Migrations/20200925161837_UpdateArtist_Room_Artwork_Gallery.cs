using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webgalleriet.Data.Migrations
{
    public partial class UpdateArtist_Room_Artwork_Gallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Increase max length Description
            migrationBuilder.AlterColumn<string>("Description", "Artist", maxLength: 3000, nullable: true);
            migrationBuilder.AlterColumn<string>("Description", "Room", maxLength: 3000, nullable: true);
            migrationBuilder.AlterColumn<string>("Description", "Artwork", maxLength: 3000, nullable: true);

            //Add Description field
            migrationBuilder.AddColumn<string>("Description", "Gallery", maxLength: 3000, nullable: true);

            //Change Title to Name
            migrationBuilder.AddColumn<string>("Name", "Artwork", maxLength: 50, nullable: true);
            migrationBuilder.DropColumn("Title", "Artwork");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Description", "Gallery");
            migrationBuilder.DropColumn("Name", "Artwork");
        }
    }
}
