using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Data.Entities
{
    public class RecordSchool
    {
        public int Id { get; set; }
        public DateTime PostingDate { get; set; }
        public string? WriteOffDate { get; set; }
        public int LocationId { get; set; }
       
        public int ResponsibleId { get; set; }
        public int ThingId { get; set; } // 1-1
        public Thing Things { get; set; } = null!;
        public Location Locations { get; set; } = null!;

        public Responsible Responsibles { get; set; } = null!;
    }
}
