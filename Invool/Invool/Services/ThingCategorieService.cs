using Invool.Data;
using Invool.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Services
{
    public class ThingCategorieService
    {
        private ApplicationDbContext _ctx = null!;
        public ThingCategorieService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<ThingCategorie> GetUsers()
            => _ctx.ThingCategories
            .ToList();
        public ThingCategorie? GetUsers(int id)
            => _ctx.ThingCategories.SingleOrDefault(c => c.Id == id);
        public void Insert(ThingCategorie thingCategorie)
        {
            _ctx.ThingCategories.Add(thingCategorie);
            _ctx.SaveChanges();
        }
        public void Update(ThingCategorie thingCategorie)
        {
            _ctx.ThingCategories.Update(thingCategorie);
            _ctx.SaveChanges();
        }
        public void Delete(ThingCategorie thingCategorie)
        {
            _ctx.ThingCategories.Remove(thingCategorie);
            _ctx.SaveChanges();
        }
    }
}
