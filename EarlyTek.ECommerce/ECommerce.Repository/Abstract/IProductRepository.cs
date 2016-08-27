using System.Linq;
using ECommerce.Domain.Entities;

namespace ECommerce.Repository.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Picture> Pictures { get; }
        IQueryable<Video> Videos { get; } 

        void SaveProduct(Product product);
        void Save(Picture picture);
        void Save(Video video);
        Product DeleteProduct(int productId);
    }
}
