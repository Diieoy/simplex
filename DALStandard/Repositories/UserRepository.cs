using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace DALStandard.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.User.Add(user);
                db.SaveChanges();
            }
        }

        public void AddUserRole(User user, string roleName)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var roleId = db.Role.Single(x => x.RoleName == roleName).Id;
                db.UserRole.Add(new UserRole { UserId = user.Id, RoleId = roleId });
                db.SaveChanges();
            }
        }

        public void Delete(string userId)
        {
            using (MyDbContext db = new MyDbContext())
            {
                DeleteUserFromRole(userId);                
                var user = db.User.FirstOrDefault(x => x.Id == userId);
                db.User.Remove(user);
                db.SaveChangesAsync();
            }
        }

        public void DeleteUserFromRole(string userId)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var userRole = db.UserRole.SingleOrDefault(x => x.UserId == userId);

                if(userRole != null)
                {
                    db.UserRole.Remove(userRole);
                    db.SaveChanges();
                }                
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (MyDbContext db = new MyDbContext())
            {
                return (from u in db.User select u).ToList();
            }
        }

        public IEnumerable<string> GetAllRoles()
        {
            using(MyDbContext db = new MyDbContext())
            {
                return (from r in db.Role select r.RoleName).ToList();
            }
        }

        public User GetUserById(string id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.User.FirstOrDefault(x => x.Id == id);
            }
        }

        public User GetUserByName(string userName)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.User.FirstOrDefault(x => x.UserName == userName);
            }
        }

        public IEnumerable<string> GetUserRoles(string userName)
        {   
            using (MyDbContext db = new MyDbContext())
            {
                var result = from u in db.User
                             join ur in db.UserRole on u.Id equals ur.UserId
                             where u.UserName == userName
                             join r in db.Role on ur.RoleId equals r.Id
                             select r.RoleName;

                return result.ToList();
            }
        }

        public void UpdateUser(User user)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var u = db.User.FirstOrDefault(x => x.Id == user.Id);
                u.UserName = user.UserName;
                u.FirstName = user.FirstName;
                u.Surname = user.Surname;
                u.Email = user.Email;
                u.PasswordHash = user.PasswordHash;
                u.Account = user.Account;
                u.TimeZone = user.TimeZone;
                u.Language = user.Language;
                db.SaveChanges();
            }
        }
    }
}
