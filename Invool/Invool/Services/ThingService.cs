using Invool.Data;
using Invool.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Services
{
    public class ThingService
    {
        private ApplicationDbContext _ctx = null!;
        public ThingService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<Thing> GetUsers()
            => _ctx.Things
            .ToList();
        public Thing? GetUsers(int id)
            => _ctx.Things.SingleOrDefault(c => c.Id == id);
        public void Insert(Thing thing)
        {
            _ctx.Things.Add(thing);
            _ctx.SaveChanges();
        }
        public void Update(Thing thing)
        {
            _ctx.Things.Update(thing);
            _ctx.SaveChanges();
        }
        public void Delete(Thing thing)
        {
            _ctx.Things.Remove(thing);
            _ctx.SaveChanges();
        }
    }
}
