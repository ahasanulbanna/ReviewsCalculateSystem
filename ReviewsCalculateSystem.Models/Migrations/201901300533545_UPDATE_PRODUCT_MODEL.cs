namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_PRODUCT_MODEL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductAsin", c => c.String());
            AddColumn("dbo.Products", "ProductLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductLink");
            DropColumn("dbo.Products", "ProductAsin");
        }
    }
}
