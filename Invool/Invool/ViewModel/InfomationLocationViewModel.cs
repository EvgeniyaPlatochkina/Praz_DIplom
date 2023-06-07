using Invool.Data;
using Invool.Data.Entities;
using Invool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.ViewModel
{
    public class InfomationLocationViewModel : ViewModelBase
    {
        public InfomationLocationViewModel(ApplicationDbContext ctx, Location location)
        {
            _locations = location;
            Name = _locations.Title;
            _thingService = new(ctx);
            UpdateLists();
        }
        private Location _locations;
        private string _name;
        public string Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
        private RecordSchoolService _thingService;
        private List<RecordSchool> _location;
        public List<RecordSchool> Locations { get => _location; set => Set(ref _location, value, nameof(Locations)); }

        private void UpdateLists()
        {
            Locations = new List<RecordSchool>(GetQuestionnare());

        }

        private ICollection<RecordSchool> GetQuestionnare() => _thingService.GetUsers().Where(x => x.LocationId == _locations.Id).ToList();
    }
}
