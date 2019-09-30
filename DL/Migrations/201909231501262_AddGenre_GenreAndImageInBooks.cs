namespace DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenre_GenreAndImageInBooks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "GenreId", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Image");
            DropColumn("dbo.Books", "GenreId");
            DropTable("dbo.Genres");
        }
    }
}
