namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_REVIEWER_MODEL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviewers", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviewers", "Phone");
        }
    }
}
