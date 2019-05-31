namespace SklepWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart_Annotations_Messages_Orders : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Product_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.Users", "PhoneNumber", c => c.String());
            AddColumn("dbo.Users", "PaymentMethod", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Price", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Brand", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.UserProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.UserProducts", "User_Id", "dbo.Users");
            DropIndex("dbo.UserProducts", new[] { "Product_Id" });
            DropIndex("dbo.UserProducts", new[] { "User_Id" });
            DropIndex("dbo.Messages", new[] { "ReceiverId" });
            DropIndex("dbo.Messages", new[] { "AuthorId" });
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            AlterColumn("dbo.Products", "Brand", c => c.String());
            AlterColumn("dbo.Products", "Price", c => c.String());
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            DropColumn("dbo.Users", "PaymentMethod");
            DropColumn("dbo.Users", "PhoneNumber");
            DropTable("dbo.UserProducts");
            DropTable("dbo.Messages");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Orders");
        }
    }
}
