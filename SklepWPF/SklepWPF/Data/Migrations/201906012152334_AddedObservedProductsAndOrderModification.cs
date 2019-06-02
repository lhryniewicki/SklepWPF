namespace SklepWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedObservedProductsAndOrderModification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserObservedProducts",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProductId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Orders", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "OrderStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserObservedProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.UserObservedProducts", "UserId", "dbo.Users");
            DropIndex("dbo.UserObservedProducts", new[] { "ProductId" });
            DropIndex("dbo.UserObservedProducts", new[] { "UserId" });
            DropColumn("dbo.Orders", "OrderStatus");
            DropColumn("dbo.Orders", "Created");
            DropTable("dbo.UserObservedProducts");
        }
    }
}
