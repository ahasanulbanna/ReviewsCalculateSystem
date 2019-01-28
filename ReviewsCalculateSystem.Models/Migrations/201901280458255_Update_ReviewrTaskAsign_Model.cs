namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ReviewrTaskAsign_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReviewerTaskAsigns", "ReviewerId", c => c.Int(nullable: false));
            AddColumn("dbo.ReviewerTaskAsigns", "AdminId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReviewerTaskAsigns", "ReviewerId");
            CreateIndex("dbo.ReviewerTaskAsigns", "AdminId");
            AddForeignKey("dbo.ReviewerTaskAsigns", "AdminId", "dbo.Admins", "AdminId", cascadeDelete: true);
            AddForeignKey("dbo.ReviewerTaskAsigns", "ReviewerId", "dbo.Reviewers", "ReviewerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewerTaskAsigns", "ReviewerId", "dbo.Reviewers");
            DropForeignKey("dbo.ReviewerTaskAsigns", "AdminId", "dbo.Admins");
            DropIndex("dbo.ReviewerTaskAsigns", new[] { "AdminId" });
            DropIndex("dbo.ReviewerTaskAsigns", new[] { "ReviewerId" });
            DropColumn("dbo.ReviewerTaskAsigns", "AdminId");
            DropColumn("dbo.ReviewerTaskAsigns", "ReviewerId");
        }
    }
}
