using ECommerce.Data;
using ECommerce.Domain.Entities;
using ECommerce.Repository.Abstract;
using System.Linq;

namespace ECommerce.Repository.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products.Include("Thumbnail"); }
        }

        public IQueryable<Picture> Pictures
        {
            get { return context.Pictures; }
        }

        public IQueryable<Video> Videos
        {
            get { return context.Videos; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                var dbEntry = context.Products.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.CategoryId = product.CategoryId;
                    dbEntry.Category = product.Category;

                    var dbThumbnail = context.Thumbnails.SingleOrDefault(x => x.ThumbnailId == dbEntry.ProductId);
                    if (dbThumbnail != null)
                    {
                        dbEntry.Thumbnail.ImagePath = product.Thumbnail.ImagePath;
                    }
                }
            }
            context.SaveChanges();
        }

        public void Save(Picture picture)
        {
            if (picture.PictureId == 0)
            {
                context.Pictures.Add(picture);
            }
            else
            {
                var dbEntry = context.Pictures.Find(picture.PictureId);
                if (dbEntry != null)
                {
                    dbEntry.ImagePath = picture.ImagePath;
                }
            }

            context.SaveChanges();
        }

        public void Save(Video video)
        {
            if (video.VideoId == 0)
            {
                context.Videos.Add(video);
            }
            else
            {
                var dbEntry = context.Videos.Find(video.VideoId);
                if (dbEntry != null)
                {
                    dbEntry.Source = video.Source;
                }
            }

            context.SaveChanges();
        }

        public void GetProductCategory(Product product)
        {
            context.Entry(product).Reference(x => x.Category);
        }

        public Product DeleteProduct(int productId)
        {
            var dbThumbnail = context.Thumbnails.Find(productId);

            if (dbThumbnail != null)
            {
                context.Thumbnails.Remove(dbThumbnail);
            }

            var dbEntry = context.Products.Find(productId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                
            }

            context.SaveChanges();

            return dbEntry;
        }
    }
}
