using Invool.Data;
using Invool.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Services
{
    public class ResponsibleService
    {
        private ApplicationDbContext _ctx = null!;
        public ResponsibleService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<Responsible> GetUsers()
            => _ctx.Responsibles
            .ToList();
        public Responsible? GetUsers(int id)
            => _ctx.Responsibles.SingleOrDefault(c => c.Id == id);
        public void Insert(Responsible responsible)
        {
            _ctx.Responsibles.Add(responsible);
            _ctx.SaveChanges();
        }
        public void Update(Responsible responsible)
        {
            _ctx.Responsibles.Update(responsible);
            _ctx.SaveChanges();
        }
        public void Delete(Responsible responsible)
        {
            _ctx.Responsibles.Remove(responsible);
            _ctx.SaveChanges();
        }
    }
}
