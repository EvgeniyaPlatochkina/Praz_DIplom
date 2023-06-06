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
    public class RecordSchoolService
    {
        private ApplicationDbContext _ctx = null!;
        public RecordSchoolService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<RecordSchool> GetUsers()
            => _ctx.RecordSchools
            .Include(l => l.Locations)
            .Include(t => t.Things)
            .ThenInclude(tc => tc.ThingCategories)
         
            .ToList();
        public RecordSchool? GetUsers(int id)
            => _ctx.RecordSchools.SingleOrDefault(c => c.Id == id);
        public void Insert(RecordSchool recordSchool)
        {
            _ctx.RecordSchools.Add(recordSchool);
            _ctx.SaveChanges();
        }
        public void Update(RecordSchool recordSchool)
        {
            _ctx.RecordSchools.Update(recordSchool);
            _ctx.SaveChanges();
        }
        public void Delete(RecordSchool recordSchool)
        {
            _ctx.RecordSchools.Remove(recordSchool);
            _ctx.SaveChanges();
        }
    }
}
