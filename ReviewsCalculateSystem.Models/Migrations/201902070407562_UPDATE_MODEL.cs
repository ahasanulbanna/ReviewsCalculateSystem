namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_MODEL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Advance", c => c.Double(nullable: false));
            AlterColumn("dbo.Payments", "DueAmount", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "DueAmount", c => c.Double(nullable: false));
            DropColumn("dbo.Payments", "Advance");
        }
    }
}
