namespace SklepWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageModelModification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "AuthorFullName", c => c.String());
            AddColumn("dbo.Messages", "ReceiverFullName", c => c.String());
            AddColumn("dbo.Messages", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Created");
            DropColumn("dbo.Messages", "ReceiverFullName");
            DropColumn("dbo.Messages", "AuthorFullName");
        }
    }
}
