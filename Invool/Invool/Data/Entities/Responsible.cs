using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Data.Entities
{
    public class Responsible
    {
        public Responsible()
        {
            RecordSchools = new HashSet<RecordSchool>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; }
        public ICollection<RecordSchool> RecordSchools { get; set; } = null!;

        [NotMapped]
        public string FullName { get => $"{LastName} {FirstName} {MiddleName}"; }
    }
}
