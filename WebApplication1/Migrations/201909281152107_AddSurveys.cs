namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSurveys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgeValue = c.Int(nullable: true),
                        ReadingValue = c.Int(nullable: true),
                        FantasticsGenre = c.Int(nullable: true),
                        DetectiveGenre = c.Int(nullable: true),
                        HorrorGenre = c.Int(nullable: true),
                        NovelGenre = c.Int(nullable: true),
                        ClassicGenre = c.Int(nullable: true),
                        ScienceGenre = c.Int(nullable: true),
                        ComputersGenre = c.Int(nullable: true),
                        ArtGenre = c.Int(nullable: true),
                        AdventureGenre = c.Int(nullable: true),
                        BuisnessGenre = c.Int(nullable: true),
                        AuthorsSentenceText = c.String(),
                        SentenceText = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Surveys");
        }
    }
}
