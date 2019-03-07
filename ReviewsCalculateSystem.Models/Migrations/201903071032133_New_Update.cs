namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviewers", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Reviewers", new[] { "Product_ProductId" });
            DropColumn("dbo.Reviewers", "Product_ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviewers", "Product_ProductId", c => c.Int());
            CreateIndex("dbo.Reviewers", "Product_ProductId");
            AddForeignKey("dbo.Reviewers", "Product_ProductId", "dbo.Products", "ProductId");
        }
    }
}
