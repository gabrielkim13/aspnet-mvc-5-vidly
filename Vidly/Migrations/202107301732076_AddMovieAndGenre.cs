namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieAndGenre : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id);

            Sql(@"
                INSERT INTO 
                    Genres 
                VALUES
                    (1, 'Action'),
                    (2, 'Comedy'),
                    (3, 'Family'),
                    (4, 'Romance')
            ");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "GenreId", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "GenreId" });
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
        }
    }
}
