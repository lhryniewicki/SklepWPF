namespace SklepWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Name", c => c.String());
            AddColumn("dbo.Orders", "Surname", c => c.String());
            AddColumn("dbo.Orders", "StreetName", c => c.String());
            AddColumn("dbo.Orders", "PostalCode", c => c.String());
            AddColumn("dbo.Orders", "City", c => c.String());
            AddColumn("dbo.Orders", "PhoneNumber", c => c.String());
            AddColumn("dbo.Orders", "PaymentMethod", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "DeliveryMethod", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DeliveryMethod");
            DropColumn("dbo.Orders", "PaymentMethod");
            DropColumn("dbo.Orders", "PhoneNumber");
            DropColumn("dbo.Orders", "City");
            DropColumn("dbo.Orders", "PostalCode");
            DropColumn("dbo.Orders", "StreetName");
            DropColumn("dbo.Orders", "Surname");
            DropColumn("dbo.Orders", "Name");
        }
    }
}
