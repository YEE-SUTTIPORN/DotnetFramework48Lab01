namespace DotnetFramework48Lab01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleModels",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.UserRoleModels",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleID)
                .ForeignKey("dbo.RoleModels", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.UserModels", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 100),
                        PasswordHash = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoleModels", "UserID", "dbo.UserModels");
            DropForeignKey("dbo.UserRoleModels", "RoleID", "dbo.RoleModels");
            DropIndex("dbo.UserRoleModels", new[] { "RoleID" });
            DropIndex("dbo.UserRoleModels", new[] { "UserID" });
            DropTable("dbo.UserModels");
            DropTable("dbo.UserRoleModels");
            DropTable("dbo.RoleModels");
        }
    }
}
