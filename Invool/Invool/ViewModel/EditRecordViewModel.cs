using Invool.Data;
using Invool.Data.Entities;
using Invool.Services;
using Invool.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Invool.ViewModel
{
    public class EditRecordViewModel : ViewModelBase
    {
        public EditRecordViewModel(ApplicationDbContext ctx, RecordSchool recordSchool)
        {
            _categorieService = new(ctx);
            _responsibleService = new(ctx);
            _thingService = new(ctx);
            _recordSchoolService = new(ctx);
            _locationService = new(ctx);
      
            RecordSchools = recordSchool;
 
              
 

                Things.Article = RecordSchools.Things.Article;
                Things.Title = RecordSchools.Things.Title;
                SelectedThingCategories = RecordSchools.Things.ThingCategories;
                SelectedLocation = RecordSchools.Locations;
                //SelectedResponsibles = RecordSchools.Responsibles;
                RecordSchools.PostingDate = RecordSchools.PostingDate;
                RecordSchools.WriteOffDate = RecordSchools.WriteOffDate;


            UpdateLists();
        }
        private void UpdateLists()
        {
            ThingCategories = new List<ThingCategorie>(GetThingCategorie());
            Responsibles = new List<Responsible>(GetResponsible());
            Locations = new List<Location>(GetLocation());
        }
        private EditWindow _view = null!;
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
        private DateTime? _offDate;
        public DateTime? OffDate { get => _offDate; set => Set(ref _offDate, value, nameof(OffDate)); }
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
        

       private bool PropertiesIsNull() => (string.IsNullOrEmpty(Things.Article) || string.IsNullOrEmpty(Things.Title) || SelectedThingCategories == null! || SelectedLocation == null!);
        private void EditEmployee()
        {
             if (ArticleIsExist())
                MessageBox.Show("Используйте другой артикул", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (PropertiesIsNull())
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Things.Article = Things.Article;
                Things.Title = Things.Title;
                Things.ThingCategories = SelectedThingCategories;
                _thingService.Update(Things);
                //RecordSchools.Responsibles = SelectedResponsibles;
                RecordSchools.Locations = SelectedLocation;
                RecordSchools.PostingDate = RecordSchools.PostingDate.Date;
                RecordSchools.WriteOffDate = RecordSchools.WriteOffDate;
                RecordSchools.Things = Things;
                _recordSchoolService.Update(RecordSchools);
                MessageBox.Show($"Запись была обновлена!", "", MessageBoxButton.OK, MessageBoxImage.None);
            }


        }
        public ICommand RecordSchoolobutton => new Command(Addregistration => EditEmployee());
    }
}
