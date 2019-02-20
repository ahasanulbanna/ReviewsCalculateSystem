namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_REGISTRATION_MODEL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        RegistrationId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Key = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RegistrationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Registrations");
        }
    }
}
