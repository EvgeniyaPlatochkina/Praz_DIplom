using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Data.Entities
{
    public class ThingCategorie
    {
        public ThingCategorie()
        {
            Things = new HashSet<Thing>();
        }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public ICollection<Thing> Things { get; set; } = null!;
    }
}
