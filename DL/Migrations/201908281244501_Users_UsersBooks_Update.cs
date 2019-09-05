namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users_UsersBooks_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.UsersBooks", "IssueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UsersBooks", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.UsersBooks", "ReturnDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsersBooks", "ReturnDate");
            DropColumn("dbo.UsersBooks", "Time");
            DropColumn("dbo.UsersBooks", "IssueDate");
            DropColumn("dbo.Users", "Email");
        }
    }
}
