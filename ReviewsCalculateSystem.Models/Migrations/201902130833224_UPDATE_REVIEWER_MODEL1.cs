namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_REVIEWER_MODEL1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviewers", "AdminApprove", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviewers", "AdminApprove");
        }
    }
}
