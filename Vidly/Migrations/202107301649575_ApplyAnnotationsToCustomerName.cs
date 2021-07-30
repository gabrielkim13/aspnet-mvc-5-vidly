namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyAnnotationsToCustomerName : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers_backup",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 2147483647),
                    IsSubscribedToNewsletter = c.Boolean(nullable: false),
                    MembershipTypeId = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO Customers_backup SELECT * FROM Customers");
            DropTable("Customers");

            CreateTable(
                "dbo.Customers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    IsSubscribedToNewsletter = c.Boolean(nullable: false),
                    MembershipTypeId = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO Customers SELECT * FROM Customers_backup");
            DropTable("Customers_backup");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Customers_backup",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    IsSubscribedToNewsletter = c.Boolean(nullable: false),
                    MembershipTypeId = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO Customers_backup SELECT * FROM Customers");
            DropTable("Customers");

            CreateTable(
                "dbo.Customers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 2147483647),
                    IsSubscribedToNewsletter = c.Boolean(nullable: false),
                    MembershipTypeId = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO Customers SELECT * FROM Customers_backup");
            DropTable("Customers_backup");
        }
    }
}
