namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        PaymentAmount = c.Double(nullable: false),
                        PayAmount = c.Double(nullable: false),
                        DueAmount = c.Double(nullable: false),
                        ReviewerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Reviewers", t => t.ReviewerId, cascadeDelete: true)
                .Index(t => t.ReviewerId)
                .Index(t => t.ProductId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        CurrentStatus = c.Boolean(nullable: false),
                        NumberOfReviewNeed = c.Int(nullable: false),
                        NumberOfReviewCollect = c.Int(),
                        ReviewStartDate = c.DateTime(),
                        ReviewEndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ReviewProducts",
                c => new
                    {
                        ReviewProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewProductId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Reviews", t => t.ReviewId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ReviewId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        SwapmeetFbProfileLink = c.String(),
                        SwapmeetProductLink = c.String(),
                        SwapmeetReviewLink = c.String(),
                        OwnReviewLink = c.String(),
                        ReviewStatus = c.Boolean(nullable: false),
                        ReviewerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Reviewers", t => t.ReviewerId, cascadeDelete: true)
                .Index(t => t.ReviewerId);
            
            CreateTable(
                "dbo.Reviewers",
                c => new
                    {
                        ReviewerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ReviewerId);
            
            CreateTable(
                "dbo.ReviewerTaskAsigns",
                c => new
                    {
                        ReviewerTaskAsignId = c.Int(nullable: false, identity: true),
                        NumberOfReviewCollect = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        PerReviewCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewerTaskAsignId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewerTaskAsigns", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Payments", "ReviewerId", "dbo.Reviewers");
            DropForeignKey("dbo.Payments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ReviewProducts", "ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.Reviews", "ReviewerId", "dbo.Reviewers");
            DropForeignKey("dbo.ReviewProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Payments", "AdminId", "dbo.Admins");
            DropIndex("dbo.ReviewerTaskAsigns", new[] { "ProductId" });
            DropIndex("dbo.Reviews", new[] { "ReviewerId" });
            DropIndex("dbo.ReviewProducts", new[] { "ReviewId" });
            DropIndex("dbo.ReviewProducts", new[] { "ProductId" });
            DropIndex("dbo.Payments", new[] { "AdminId" });
            DropIndex("dbo.Payments", new[] { "ProductId" });
            DropIndex("dbo.Payments", new[] { "ReviewerId" });
            DropTable("dbo.ReviewerTaskAsigns");
            DropTable("dbo.Reviewers");
            DropTable("dbo.Reviews");
            DropTable("dbo.ReviewProducts");
            DropTable("dbo.Products");
            DropTable("dbo.Payments");
            DropTable("dbo.Admins");
        }
    }
}
