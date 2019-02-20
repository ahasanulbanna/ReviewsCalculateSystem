namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE_REGISTRATION_MODEL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Registrations", "ActivationKey", c => c.String());
            DropColumn("dbo.Registrations", "Key");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Registrations", "Key", c => c.String());
            DropColumn("dbo.Registrations", "ActivationKey");
        }
    }
}
