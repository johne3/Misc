using ECommerce.Domain.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ECommerce.Data
{
    public class EFDbContext : IdentityDbContext<ApplicationUser>
    {
        public EFDbContext()
            : base("EFDbContext")
        {
            //Database.SetInitializer<EFDbContext>(null);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<FeatureRequest> FeatureRequests { get; set; }

        //public DbSet<UserProfile> Users { get; set; }
    }
}
