namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToAspNetMvcIdentityProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles_backup",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            Sql("INSERT INTO AspNetRoles_backup SELECT * FROM AspNetRoles");
            DropTable("AspNetRoles");

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            Sql("INSERT INTO AspNetRoles (Id, Name) SELECT Id, Name FROM AspNetRoles_backup");
            DropTable("AspNetRoles_backup");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.AspNetRoles_backup",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            Sql("INSERT INTO AspNetRoles_backup SELECT * FROM AspNetRoles");
            DropTable("AspNetRoles");

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            Sql("INSERT INTO AspNetRoles (Id, Name, Discriminator) SELECT Id, Name, '' FROM AspNetRoles_backup");
            DropTable("AspNetRoles_backup");
        }
    }
}
