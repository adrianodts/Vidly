namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"

                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1834b928-8bb2-43d0-8dc1-ba9a150cd17f', N'admin@vidly.com', 0, N'AEaxOtSMk608jCpW64UkOKwo1pV27qREgkYB6kal4ySzVd/HjwDJIl9NJSOeWsrQdA==', N'03750694-bec3-4253-ae03-54de3fd1c538', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'11277096-178d-4028-8fea-e68d46e5ec79', N'CanManageMovies')
                
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1834b928-8bb2-43d0-8dc1-ba9a150cd17f', N'11277096-178d-4028-8fea-e68d46e5ec79')

            ");

        }
        
        public override void Down()
        {
        }
    }
}
