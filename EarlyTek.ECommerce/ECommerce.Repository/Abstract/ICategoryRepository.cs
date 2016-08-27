using ECommerce.Domain.Entities;
using System.Linq;

namespace ECommerce.Repository.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        void SaveCategory(Category category);
        Category DeleteCategory(int categoryId);
    }
}
