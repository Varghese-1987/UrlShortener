namespace Nintex.UrlShortener.Model.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "UrlShortener.ShortUrl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniqueId = c.String(nullable: false, maxLength: 22),
                        OriginalUrl = c.String(nullable: false, maxLength: 2083),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("UrlShortener.ShortUrl");
        }
    }
}
