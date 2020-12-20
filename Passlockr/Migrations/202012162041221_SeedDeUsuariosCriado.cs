namespace Passlockr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedDeUsuariosCriado : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5292ed9d-6693-4ccd-9a20-cf0bc1a10dc5', N'admin@passlock.com', 0, N'ACstzh01PTw0ALSR/QgwtkBeMJUE7e6TFDUlp8CfwKGF4TcFm4DlW0VYmRgSbDxmMQ==', N'f8b8994c-b4ee-4b6a-a790-fad32bd060cb', NULL, 0, 0, NULL, 1, 0, N'admin@passlock.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c073f2c0-7268-4a65-8482-03d0f583cbb7', N'convidado@passlock.com', 0, N'AAvCZczbrDpjGxu5+7tK0rUPfAastD7/oNJhUHR1T+VlVGbcrB3KZWZvtKN5Dgl8qw==', N'446e6056-1612-4ba0-aac2-26761cde9c57', NULL, 0, 0, NULL, 1, 0, N'convidado@passlock.com')
            
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ecb141e9-bc77-4a38-9de4-fde16a5332b3', N'PodeControlarUsuarios')
                
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5292ed9d-6693-4ccd-9a20-cf0bc1a10dc5', N'ecb141e9-bc77-4a38-9de4-fde16a5332b3')");
        }
        
        public override void Down()
        {
        }
    }
}
