namespace SklepWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergedMigrations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "AuthorId" });
            DropIndex("dbo.Messages", new[] { "ReceiverId" });
            CreateTable(
                "dbo.MessageUsers",
                c => new
                    {
                        Message_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Message_Id, t.User_Id })
                .ForeignKey("dbo.Messages", t => t.Message_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Message_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.MessageUser1",
                c => new
                    {
                        Message_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Message_Id, t.User_Id })
                .ForeignKey("dbo.Messages", t => t.Message_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Message_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Users", "IsAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Name", c => c.String());
            AddColumn("dbo.Orders", "Surname", c => c.String());
            AddColumn("dbo.Orders", "StreetName", c => c.String());
            AddColumn("dbo.Orders", "PostalCode", c => c.String());
            AddColumn("dbo.Orders", "City", c => c.String());
            AddColumn("dbo.Orders", "PhoneNumber", c => c.String());
            AddColumn("dbo.Orders", "PaymentMethod", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "DeliveryMethod", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "LastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "ClientSeen", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "AdminSeen", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Messages", "AuthorId", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "ReceiverFullName");
            DropColumn("dbo.Messages", "ReceiverId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "ReceiverId", c => c.Int());
            AddColumn("dbo.Messages", "ReceiverFullName", c => c.String());
            DropForeignKey("dbo.MessageUser1", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MessageUser1", "Message_Id", "dbo.Messages");
            DropForeignKey("dbo.MessageUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MessageUsers", "Message_Id", "dbo.Messages");
            DropIndex("dbo.MessageUser1", new[] { "User_Id" });
            DropIndex("dbo.MessageUser1", new[] { "Message_Id" });
            DropIndex("dbo.MessageUsers", new[] { "User_Id" });
            DropIndex("dbo.MessageUsers", new[] { "Message_Id" });
            AlterColumn("dbo.Messages", "AuthorId", c => c.Int());
            AlterColumn("dbo.Products", "Price", c => c.String(nullable: false));
            DropColumn("dbo.Messages", "AdminSeen");
            DropColumn("dbo.Messages", "ClientSeen");
            DropColumn("dbo.Messages", "LastModified");
            DropColumn("dbo.Orders", "DeliveryMethod");
            DropColumn("dbo.Orders", "PaymentMethod");
            DropColumn("dbo.Orders", "PhoneNumber");
            DropColumn("dbo.Orders", "City");
            DropColumn("dbo.Orders", "PostalCode");
            DropColumn("dbo.Orders", "StreetName");
            DropColumn("dbo.Orders", "Surname");
            DropColumn("dbo.Orders", "Name");
            DropColumn("dbo.Users", "IsAdmin");
            DropTable("dbo.MessageUser1");
            DropTable("dbo.MessageUsers");
            CreateIndex("dbo.Messages", "ReceiverId");
            CreateIndex("dbo.Messages", "AuthorId");
            AddForeignKey("dbo.Messages", "ReceiverId", "dbo.Users", "Id");
            AddForeignKey("dbo.Messages", "AuthorId", "dbo.Users", "Id");
        }
    }
}
