namespace SklepWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyUserCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProducts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserProducts", "Product_Id", "dbo.Products");
            DropIndex("dbo.UserProducts", new[] { "User_Id" });
            DropIndex("dbo.UserProducts", new[] { "Product_Id" });
            CreateTable(
                "dbo.UserCarts",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.UserId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
            DropTable("dbo.UserProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Product_Id });
            
            DropForeignKey("dbo.UserCarts", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserCarts", "ProductId", "dbo.Products");
            DropIndex("dbo.UserCarts", new[] { "UserId" });
            DropIndex("dbo.UserCarts", new[] { "ProductId" });
            DropTable("dbo.UserCarts");
            CreateIndex("dbo.UserProducts", "Product_Id");
            CreateIndex("dbo.UserProducts", "User_Id");
            AddForeignKey("dbo.UserProducts", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserProducts", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
