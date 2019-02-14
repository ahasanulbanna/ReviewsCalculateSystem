namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_REVIEWER_MODEL11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviewers", "Product_ProductId", c => c.Int());
            CreateIndex("dbo.Reviewers", "Product_ProductId");
            AddForeignKey("dbo.Reviewers", "Product_ProductId", "dbo.Products", "ProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviewers", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Reviewers", new[] { "Product_ProductId" });
            DropColumn("dbo.Reviewers", "Product_ProductId");
        }
    }
}
