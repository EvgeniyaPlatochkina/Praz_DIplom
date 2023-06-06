using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Data.Entities
{
    public class Location
    {
        public Location()
        {
            RecordSchools = new HashSet<RecordSchool>();
        }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ResponsibleId { get; set; } // 1-1
        public Responsible Responsibles { get; set; } = null!;

        public ICollection<RecordSchool> RecordSchools { get; set; } = null!;
    }
}
