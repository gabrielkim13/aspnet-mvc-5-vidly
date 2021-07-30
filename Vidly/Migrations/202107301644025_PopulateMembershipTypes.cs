namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO 
                    MembershipTypes 
                VALUES
                    (1, 0, 0, 0),
                    (2, 30, 1, 10),
                    (3, 90, 3, 15),
                    (4, 300, 12, 20)
            ");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM MembershipTypes");
        }
    }
}
