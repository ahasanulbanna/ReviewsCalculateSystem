namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_REVIEWER_ADMIN_MODEL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Password", c => c.String());
            AddColumn("dbo.Reviewers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviewers", "Password");
            DropColumn("dbo.Admins", "Password");
        }
    }
}
