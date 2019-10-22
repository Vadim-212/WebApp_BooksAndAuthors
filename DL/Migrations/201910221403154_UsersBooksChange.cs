namespace DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersBooksChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UsersBooks", "IssueDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.UsersBooks", "Time", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.UsersBooks", "ReturnDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UsersBooks", "ReturnDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UsersBooks", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UsersBooks", "IssueDate", c => c.DateTime(nullable: false));
        }
    }
}
