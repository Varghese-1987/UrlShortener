namespace Nintex.UrlShortener.Model.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Entities.UrlShortenerContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Entities.UrlShortenerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
