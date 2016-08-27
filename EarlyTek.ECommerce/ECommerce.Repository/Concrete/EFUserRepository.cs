//using ECommerce.Data;
//using ECommerce.Domain.Entities;
//using ECommerce.Repository.Abstract;
//using System.Linq;

//namespace ECommerce.Repository.Concrete
//{
//    public class EFUserRepository : IUserRepository
//    {
//        private readonly EFDbContext context = new EFDbContext();

//        public IQueryable<UserProfile> Users
//        {
//            get { return context.Users; }
//        }

//        public void SaveUser(UserProfile user)
//        {
//            if (user.UserId == 0)
//            {
//                context.Users.Add(user);
//            }
//            else
//            {
//                var dbEntry = context.Users.Find(user.UserId);
//                if (dbEntry != null)
//                {
//                    dbEntry.UserName = user.UserName;
//                }
//            }

//            context.SaveChanges();
//        }
//    }
//}
