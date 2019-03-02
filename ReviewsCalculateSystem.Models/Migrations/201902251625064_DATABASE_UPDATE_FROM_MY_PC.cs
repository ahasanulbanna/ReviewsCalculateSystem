namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DATABASE_UPDATE_FROM_MY_PC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
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
                        DueAmount = c.Double(),
                        Advance = c.Double(nullable: false),
                        ReviewerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Reviewers", t => t.ReviewerId)
                .Index(t => t.ReviewerId)
                .Index(t => t.ProductId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductAsin = c.String(),
                        ProductLink = c.String(),
                        CurrentStatus = c.Boolean(nullable: false),
                        NumberOfReviewNeed = c.Int(nullable: false),
                        NumberOfReviewCollect = c.Int(),
                        ReviewStartDate = c.DateTime(),
                        ReviewEndDate = c.DateTime(),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Reviewers",
                c => new
                    {
                        ReviewerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        NumberOfAmazonAccount = c.Int(),
                        Phone = c.String(),
                        AdminApprove = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewerId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
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
                        isPay = c.Boolean(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ReviewerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Reviewers", t => t.ReviewerId)
                .Index(t => t.ProductId)
                .Index(t => t.ReviewerId);
            
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        RegistrationId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        ActivationKey = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RegistrationId);
            
            CreateTable(
                "dbo.ReviewerTaskAsigns",
                c => new
                    {
                        ReviewerTaskAsignId = c.Int(nullable: false, identity: true),
                        NumberOfReviewCollect = c.Int(),
                        ReviewCollectMargin = c.Int(),
                        ProductId = c.Int(nullable: false),
                        ReviewerId = c.Int(nullable: false),
                        PerReviewCost = c.Double(nullable: false),
                        AdminId = c.Int(nullable: false),
                        isComplete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewerTaskAsignId)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Reviewers", t => t.ReviewerId)
                .Index(t => t.ProductId)
                .Index(t => t.ReviewerId)
                .Index(t => t.AdminId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewerTaskAsigns", "ReviewerId", "dbo.Reviewers");
            DropForeignKey("dbo.ReviewerTaskAsigns", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ReviewerTaskAsigns", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Payments", "ReviewerId", "dbo.Reviewers");
            DropForeignKey("dbo.Payments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Reviews", "ReviewerId", "dbo.Reviewers");
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Reviewers", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Payments", "AdminId", "dbo.Admins");
            DropIndex("dbo.ReviewerTaskAsigns", new[] { "AdminId" });
            DropIndex("dbo.ReviewerTaskAsigns", new[] { "ReviewerId" });
            DropIndex("dbo.ReviewerTaskAsigns", new[] { "ProductId" });
            DropIndex("dbo.Reviews", new[] { "ReviewerId" });
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.Reviewers", new[] { "Product_ProductId" });
            DropIndex("dbo.Products", new[] { "AdminId" });
            DropIndex("dbo.Payments", new[] { "AdminId" });
            DropIndex("dbo.Payments", new[] { "ProductId" });
            DropIndex("dbo.Payments", new[] { "ReviewerId" });
            DropTable("dbo.ReviewerTaskAsigns");
            DropTable("dbo.Registrations");
            DropTable("dbo.Reviews");
            DropTable("dbo.Reviewers");
            DropTable("dbo.Products");
            DropTable("dbo.Payments");
            DropTable("dbo.Admins");
        }
    }
}
