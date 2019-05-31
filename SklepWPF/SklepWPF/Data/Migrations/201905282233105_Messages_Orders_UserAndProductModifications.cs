namespace SklepWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Messages_Orders_UserAndProductModifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        AuthorId = c.Int(),
                        ReceiverId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Users", t => t.ReceiverId)
                .Index(t => t.AuthorId)
                .Index(t => t.ReceiverId);
            
            AddColumn("dbo.Users", "PhoneNumber", c => c.String());
            AddColumn("dbo.Users", "PaymentMethod", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropIndex("dbo.Messages", new[] { "ReceiverId" });
            DropIndex("dbo.Messages", new[] { "AuthorId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropColumn("dbo.Users", "PaymentMethod");
            DropColumn("dbo.Users", "PhoneNumber");
            DropTable("dbo.Messages");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderProducts");
        }
    }
}
