namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO AspNetRoles
                    (Id, Name)
                VALUES
                    ('da66bbbc-9fea-42b6-bf64-70ca7d8a1866', 'CanManageMovies')
            ");

            Sql(@"
                INSERT INTO AspNetUsers
                    (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName)
                VALUES
                    ('04d39bb9-dc4d-44f8-9be7-ce7b31f47a02', 'guest@vidly.com', 0, 'ALN4AOXNoO9ZJxqBMw4JJjK4dKKKg31xl9gFZLJqs0YYrnqrZSJaVFVbjf6OoF6WKA==', '708d4dfa-fa18-4eda-9b04-a5b7385436ea', NULL, 0, 0, NULL, 1, 0, 'guest@vidly.com'),
                    ('2e64414a-9ee3-4da2-9c13-4c665f0c2941', 'admin@vidly.com', 0, 'AAdhoXdysJwitlFFONjpefPdgYfEH1Y8FQb6540GuB8qV/Uf0nWDF+k0v/IcyBg7Pw==', 'fb02b722-9319-4066-bef0-90a92665a4a6', NULL, 0, 0, NULL, 1, 0, 'admin@vidly.com')
            ");

            Sql(@"
                INSERT INTO AspNetUserRoles
                    (UserId, RoleId)
                VALUES
                    ('2e64414a-9ee3-4da2-9c13-4c665f0c2941', 'da66bbbc-9fea-42b6-bf64-70ca7d8a1866')
            ");
        }
        
        public override void Down()
        {
            Sql(@"
                DELETE FROM 
                    AspNetUserRoles 
                WHERE
                    UserId = '2e64414a-9ee3-4da2-9c13-4c665f0c2941'
                    AND RoleId = 'da66bbbc-9fea-42b6-bf64-70ca7d8a1866'
            ");

            Sql(@"
                DELETE FROM 
                    AspNetUsers 
                WHERE
                    Id IN
                    (
                        '04d39bb9-dc4d-44f8-9be7-ce7b31f47a02',
                        '2e64414a-9ee3-4da2-9c13-4c665f0c2941'
                    )
            ");

            Sql(@"
                DELETE FROM 
                    AspNetRoles 
                WHERE
                    Id = 'da66bbbc-9fea-42b6-bf64-70ca7d8a1866'
            ");
        }
    }
}
