namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberAvailableToMovies : DbMigration
    {
        public override void Up()
        {
            // AddColumn("dbo.Movies", "NumberAvailable", c => c.Int(nullable: false));

            Sql(@"ALTER TABLE Movies ADD COLUMN NumberAvailable INT NOT NULL DEFAULT 0");
            Sql(@"UPDATE Movies SET NumberAvailable = NumberInStock");
        }

        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvailable");
        }
    }
}
