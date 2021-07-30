namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationAttributes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies_backup",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 2147483647),
                    GenreId = c.Byte(nullable: false),
                    ReleaseDate = c.DateTime(),
                    NumberInStock = c.Int(nullable: false),
                    DateAdded = c.DateTime(nullable: false, defaultValueSql: "datetime('now')"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            Sql("INSERT INTO Movies_backup SELECT * FROM Movies");
            DropTable("Movies");

            CreateTable(
                "dbo.Movies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 2147483647),
                    GenreId = c.Byte(nullable: false),
                    ReleaseDate = c.DateTime(nullable: false),
                    NumberInStock = c.Int(nullable: false),
                    DateAdded = c.DateTime(nullable: false, defaultValueSql: "datetime('now')"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            Sql("INSERT INTO Movies SELECT * FROM Movies_backup");
            DropTable("Movies_backup");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Movies_backup",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 2147483647),
                    GenreId = c.Byte(nullable: false),
                    ReleaseDate = c.DateTime(nullable: false),
                    NumberInStock = c.Int(nullable: false),
                    DateAdded = c.DateTime(nullable: false, defaultValueSql: "datetime('now')"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            Sql("INSERT INTO Movies_backup SELECT * FROM Movies");
            DropTable("Movies");

            CreateTable(
                "dbo.Movies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 2147483647),
                    GenreId = c.Byte(nullable: false),
                    ReleaseDate = c.DateTime(),
                    NumberInStock = c.Int(nullable: false),
                    DateAdded = c.DateTime(nullable: false, defaultValueSql: "datetime('now')"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            Sql("INSERT INTO Movies SELECT * FROM Movies_backup");
            DropTable("Movies_backup");
        }
    }
}
