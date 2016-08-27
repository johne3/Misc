using ECommerce.Data;
using ECommerce.Domain.Entities;
using ECommerce.Repository.Abstract;
using System.Linq;

namespace ECommerce.Repository.Concrete
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly EFDbContext context = new EFDbContext();

        public IQueryable<Category> Categories
        {
            get { return context.Categories; }
        }

        public void SaveCategory(Category category)
        {
            if (category.CategoryId == 0)
            {
                context.Categories.Add(category);
            }
            else
            {
                var dbEntry = context.Categories.Find(category.CategoryId);
                if (dbEntry != null)
                {
                    dbEntry.Name = category.Name;
                }
            }

            context.SaveChanges();
        }

        public Category DeleteCategory(int categoryId)
        {
            var dbEntry = context.Categories.Find(categoryId);
            if (dbEntry != null)
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
