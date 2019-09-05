namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsers : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Authors",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(maxLength: 100, unicode: false),
            //            LastName = c.String(maxLength: 100, unicode: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Books",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            AuthorId = c.Int(nullable: false),
            //            Title = c.String(nullable: false, maxLength: 150, unicode: false),
            //            Pages = c.Int(),
            //            Price = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Authors", t => t.AuthorId)
            //    .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.Users");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
