namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_REVIEWER_MODEL : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReviewerTaskAsigns", "NumberOfReviewCollect", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReviewerTaskAsigns", "NumberOfReviewCollect", c => c.Int(nullable: false));
        }
    }
}
