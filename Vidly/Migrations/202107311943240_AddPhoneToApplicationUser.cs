namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneToApplicationUser : DbMigration
    {
        public override void Up()
        {
            // AddColumn("dbo.AspNetUsers", "Phone", c => c.String(nullable: false, maxLength: 255));

            Sql(@"ALTER TABLE AspNetUsers ADD COLUMN Phone nvarchar(255) NOT NULL DEFAULT ''");
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Phone");
        }
    }
}
