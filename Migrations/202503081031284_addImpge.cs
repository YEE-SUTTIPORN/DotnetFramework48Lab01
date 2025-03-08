namespace DotnetFramework48Lab01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImpge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupperHeroModels", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupperHeroModels", "Image");
        }
    }
}
