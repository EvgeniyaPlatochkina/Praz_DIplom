using Invool.Data;
using Invool.Data.Entities;
using Invool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Invool.ViewModel
{
    public class CreateRecordViewModel : ViewModelBase
    {
        public CreateRecordViewModel(ApplicationDbContext ctx)
        {
            _categorieService = new(ctx);
            _responsibleService = new(ctx);
            _thingService = new(ctx);
            _recordSchoolService = new(ctx);
            _locationService = new(ctx);
            RecordSchools.PostingDate = DateTime.Now;
            UpdateLists();
        }
        private void UpdateLists()
        {
            ThingCategories = new List<ThingCategorie>(GetThingCategorie());
            Responsibles = new List<Responsible>(GetResponsible());
            Locations = new List<Location>(GetLocation());
        }
        private List<ThingCategorie> _thingCategories;
        private List<Responsible> _responsibles;
        private ThingCategorie _selectedThingCategorie;
        private Responsible _selectedResponsibles;
        private ThingCategorieService _categorieService;
        private ResponsibleService _responsibleService;
        private ThingService _thingService;
        private RecordSchoolService _recordSchoolService;
        private LocationService _locationService;
        private List<Location> _locations;
        private Location _selectedLocation;
        public Location SelectedLocation { get => _selectedLocation; set => Set(ref _selectedLocation, value, nameof(SelectedLocation)); }
        public List<Location> Locations { get => _locations; set => Set(ref _locations, value, nameof(Locations)); }
        private Thing _thing = new Thing();
        public Thing Things { get => _thing; set => Set(ref _thing, value, nameof(Things)); }

        private RecordSchool _recordSchool = new RecordSchool();
        public RecordSchool RecordSchools { get => _recordSchool; set => Set(ref _recordSchool, value, nameof(RecordSchools)); }
        private ICollection<ThingCategorie> GetThingCategorie() => _categorieService.GetUsers().ToList();
        private ICollection<Responsible> GetResponsible() => _responsibleService.GetUsers().ToList();
        private ICollection<Location> GetLocation() => _locationService.GetUsers().ToList();
        public List<ThingCategorie> ThingCategories { get => _thingCategories; set => Set(ref _thingCategories, value, nameof(ThingCategories)); }
        public ThingCategorie SelectedThingCategories { get => _selectedThingCategorie; set => Set(ref _selectedThingCategorie, value, nameof(SelectedThingCategories)); }
        public List<Responsible> Responsibles { get => _responsibles; set => Set(ref _responsibles, value, nameof(Responsibles)); }
        public Responsible SelectedResponsibles { get => _selectedResponsibles; set => Set(ref _selectedResponsibles, value, nameof(SelectedResponsibles)); }

        private bool ArticleIsExist() => _thingService.GetUsers().Any(c => c.Article == Things.Article);
        private bool PropertiesIsNull() => (string.IsNullOrEmpty(Things.Article) || string.IsNullOrEmpty(Things.Title) || SelectedThingCategories == null! || SelectedLocation == null! || RecordSchools.PostingDate == null!);
        private void AddEmployee()
        {
            if(PropertiesIsNull())
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (ArticleIsExist())
                MessageBox.Show("Используйте другой артикул", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                        Things.Article = Things.Article;
                        Things.Title = Things.Title;
                        Things.ThingCategories = SelectedThingCategories;
                        _thingService.Insert(Things);
                        //RecordSchools.Responsibles = SelectedResponsibles;
                        RecordSchools.Locations = SelectedLocation;
                        RecordSchools.PostingDate = RecordSchools.PostingDate.Date;
                        RecordSchools.WriteOffDate = null;
                        RecordSchools.Things = Things;
                        _recordSchoolService.Insert(RecordSchools);
                        MessageBox.Show($"Запись была добавлен!", "", MessageBoxButton.OK, MessageBoxImage.None);
            }
                

        }  
        public ICommand AddRecordSchoolobutton => new Command(Addregistration => AddEmployee());
    }
}
