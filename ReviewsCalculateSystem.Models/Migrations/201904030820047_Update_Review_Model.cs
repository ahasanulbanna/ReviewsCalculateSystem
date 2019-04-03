namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Review_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "SwapmeetFbName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "SwapmeetFbName");
        }
    }
}
