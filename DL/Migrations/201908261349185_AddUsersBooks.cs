namespace DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersBooks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "UsersBooks_Id", c => c.Int());
            CreateIndex("dbo.Books", "UsersBooks_Id");
            AddForeignKey("dbo.Books", "UsersBooks_Id", "dbo.UsersBooks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "UsersBooks_Id", "dbo.UsersBooks");
            DropIndex("dbo.Books", new[] { "UsersBooks_Id" });
            DropColumn("dbo.Books", "UsersBooks_Id");
            DropTable("dbo.UsersBooks");
        }
    }
}
