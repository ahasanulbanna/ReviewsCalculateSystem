namespace ReviewsCalculateSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePaymentModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "ProductId", "dbo.Products");
            DropIndex("dbo.Payments", new[] { "ProductId" });
            AddColumn("dbo.Payments", "PaymentDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Payments", "PayAmount");
            DropColumn("dbo.Payments", "DueAmount");
            DropColumn("dbo.Payments", "Advance");
            DropColumn("dbo.Payments", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "Advance", c => c.Double(nullable: false));
            AddColumn("dbo.Payments", "DueAmount", c => c.Double());
            AddColumn("dbo.Payments", "PayAmount", c => c.Double(nullable: false));
            DropColumn("dbo.Payments", "PaymentDate");
            CreateIndex("dbo.Payments", "ProductId");
            AddForeignKey("dbo.Payments", "ProductId", "dbo.Products", "ProductId");
        }
    }
}
