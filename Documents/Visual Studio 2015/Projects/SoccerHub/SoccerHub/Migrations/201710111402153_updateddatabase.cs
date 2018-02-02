namespace SoccerHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateddatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MatchFormViewModels", "HomeTeamId", "dbo.Clubs");
            DropForeignKey("dbo.MatchFormViewModels", "OutTeamId", "dbo.Clubs");
            DropForeignKey("dbo.MatchFormViewModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.MatchFormViewModels", new[] { "UserId" });
            DropIndex("dbo.MatchFormViewModels", new[] { "HomeTeamId" });
            DropIndex("dbo.MatchFormViewModels", new[] { "OutTeamId" });
            DropTable("dbo.MatchFormViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MatchFormViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Stadium = c.String(nullable: false, maxLength: 255),
                        Date = c.String(nullable: false),
                        Time = c.String(nullable: false),
                        HomeTeamId = c.Byte(nullable: false),
                        OutTeamId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.MatchFormViewModels", "OutTeamId");
            CreateIndex("dbo.MatchFormViewModels", "HomeTeamId");
            CreateIndex("dbo.MatchFormViewModels", "UserId");
            AddForeignKey("dbo.MatchFormViewModels", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MatchFormViewModels", "OutTeamId", "dbo.Clubs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MatchFormViewModels", "HomeTeamId", "dbo.Clubs", "Id", cascadeDelete: true);
        }
    }
}
