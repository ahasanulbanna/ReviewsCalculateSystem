namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upadate_Model_Relationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReviewProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ReviewProducts", "ReviewId", "dbo.Reviews");
            DropIndex("dbo.ReviewProducts", new[] { "ProductId" });
            DropIndex("dbo.ReviewProducts", new[] { "ReviewId" });
            AddColumn("dbo.Reviews", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "ProductId");
            AddForeignKey("dbo.Reviews", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            DropTable("dbo.ReviewProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReviewProducts",
                c => new
                    {
                        ReviewProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewProductId);
            
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropColumn("dbo.Reviews", "ProductId");
            CreateIndex("dbo.ReviewProducts", "ReviewId");
            CreateIndex("dbo.ReviewProducts", "ProductId");
            AddForeignKey("dbo.ReviewProducts", "ReviewId", "dbo.Reviews", "ReviewId", cascadeDelete: true);
            AddForeignKey("dbo.ReviewProducts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
