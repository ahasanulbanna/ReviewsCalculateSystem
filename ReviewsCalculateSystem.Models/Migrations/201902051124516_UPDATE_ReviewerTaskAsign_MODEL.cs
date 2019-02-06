namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_ReviewerTaskAsign_MODEL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReviewerTaskAsigns", "isComplete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReviewerTaskAsigns", "isComplete");
        }
    }
}
