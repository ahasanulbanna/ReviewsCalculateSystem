namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_REVIEWER1_MODEL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "isPay", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "isPay");
        }
    }
}
