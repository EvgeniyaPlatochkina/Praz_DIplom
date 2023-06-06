using Invool.Data;
using Invool.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Services
{
    public class LocationService
    {
        private ApplicationDbContext _ctx = null!;
        public LocationService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<Location> GetUsers()
            => _ctx.Locations
            .Include(r => r.Responsibles)
             .Include(rs => rs.RecordSchools)
                .ThenInclude(th =>th.Things)
            .ToList();
        public Location? GetUsers(int id)
            => _ctx.Locations.SingleOrDefault(c => c.Id == id);
        public void Insert(Location location)
        {
            _ctx.Locations.Add(location);
            _ctx.SaveChanges();
        }
        public void Update(Location location)
        {
            _ctx.Locations.Update(location);
            _ctx.SaveChanges();
        }
        public void Delete(Location location)
        {
            _ctx.Locations.Remove(location);
            _ctx.SaveChanges();
        }
    }
}
