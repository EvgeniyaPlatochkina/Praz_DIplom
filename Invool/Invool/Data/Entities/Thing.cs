using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Data.Entities
{
    public class Thing
    {
        public int Id { get; set; }
        public string Article { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int ThingCategorieId { get; set; }
        public ThingCategorie ThingCategories { get; set; } = null!;
        public RecordSchool RecordSchools { get; set; }

    }
}
