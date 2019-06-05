namespace SklepWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageModification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.Users");
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
            
            AddColumn("dbo.Messages", "Seen", c => c.Boolean(nullable: false));
            DropColumn("dbo.Messages", "ReceiverFullName");
            DropColumn("dbo.Messages", "ReceiverId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "ReceiverId", c => c.Int());
            AddColumn("dbo.Messages", "ReceiverFullName", c => c.String());
            DropForeignKey("dbo.MessageUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MessageUsers", "Message_Id", "dbo.Messages");
            DropIndex("dbo.MessageUsers", new[] { "User_Id" });
            DropIndex("dbo.MessageUsers", new[] { "Message_Id" });
            DropColumn("dbo.Messages", "Seen");
            DropTable("dbo.MessageUsers");
            CreateIndex("dbo.Messages", "ReceiverId");
            AddForeignKey("dbo.Messages", "ReceiverId", "dbo.Users", "Id");
        }
    }
}
