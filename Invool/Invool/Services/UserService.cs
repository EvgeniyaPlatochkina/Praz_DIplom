using Invool.Data;
using Invool.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Services
{
    public class UserService
    {
        private ApplicationDbContext _ctx = null!;
        public UserService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<User> GetUsers()
            => _ctx.Users
            .ToList();
        public User? GetUsers(int id)
            => _ctx.Users.SingleOrDefault(c => c.Id == id);
        public User GetUser(string login, string password)
         => GetUsers()
         .Single(x => x.Login == login && x.Password == password);
        public void Insert(User user)
        {
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
        }
        public void Update(User user)
        {
            _ctx.Users.Update(user);
            _ctx.SaveChanges();
        }
        public void Delete(User user)
        {
            _ctx.Users.Remove(user);
            _ctx.SaveChanges();
        }
    }
}
