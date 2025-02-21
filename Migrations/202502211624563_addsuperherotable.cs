namespace DotnetFramework48Lab01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsuperherotable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoleModels", "RoleID", "dbo.RoleModels");
            DropForeignKey("dbo.UserRoleModels", "UserID", "dbo.UserModels");
            DropIndex("dbo.UserRoleModels", new[] { "UserID" });
            DropIndex("dbo.UserRoleModels", new[] { "RoleID" });
            CreateTable(
                "dbo.SupperHeroModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SuperHeroName = c.String(nullable: false),
                        SuperHeroPower = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SupperHeroModels");
            CreateIndex("dbo.UserRoleModels", "RoleID");
            CreateIndex("dbo.UserRoleModels", "UserID");
            AddForeignKey("dbo.UserRoleModels", "UserID", "dbo.UserModels", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.UserRoleModels", "RoleID", "dbo.RoleModels", "RoleID", cascadeDelete: true);
        }
    }
}
