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
    public  class AddDataViewModel : ViewModelBase
    {
        public AddDataViewModel(ApplicationDbContext ctx, Window window)
        {
            _ctx = ctx;
            _locationService = new(ctx);
            _thingCategorieService = new(ctx);
            _responsibleService = new(ctx);
            Window = window;
            UpdateLists();
        }
        private void UpdateLists()
        {


            Locations = new List<Location>(GetLocation()).ToList();
            Categorie = new List<ThingCategorie>(GetThingCategorie()).ToList();
            Responsibles = new List<Responsible>(GetResponsible()).ToList();

        }
        private ApplicationDbContext _ctx;
        private Window Window;
        public void ExitDirectorWindow()
        {
            var DirectorWindow = new MainWindow(_ctx);
            var CurrentWindow = Application.Current.MainWindow;
            DirectorWindow.Show();
            CurrentWindow.Close();
            Window.Close();
        }
        public ICommand ExitButton => new Command(exit => ExitDirectorWindow());

        #region Categorie
        private List<Location> _locations;
        public List<Location> Locations { get => _locations; set => Set(ref _locations, value, nameof(Locations)); }
        private ICollection<Location> GetLocation() => _locationService.GetUsers().ToList();
        private LocationService _locationService;
        private string _locationTitle;
        private Location _selectedCLocationTitle;
        public string LocationTitle { get => _locationTitle; set => Set(ref _locationTitle, value, nameof(LocationTitle)); }
        public Location SelectedLocationTitle
        {
            get => _selectedCLocationTitle;
            set
            {
                Set(ref _selectedCLocationTitle, value, nameof(SelectedLocationTitle));
                if (value != null)
                {
                    LocationTitle = value.Title;

                }
            }
        }
        private bool SelectedCategorieTitleIsNull() => SelectedLocationTitle == null;
        private bool LocationTitleAlredyInUse() => _locationService.GetUsers().Any(c => c.Title == LocationTitle);
        private bool LocationPropertiesIsNull() => string.IsNullOrEmpty(LocationTitle);
        public ICommand AddLocationbutton => new Command(Addregistration => AddLocation());
        public ICommand UpdateLocationButton => new Command(updateaccount => UpdateLocationData());
        public ICommand DeleteLocationButton => new Command(delete => DeleteLocation());

        private void AddLocation()
        {
            if (LocationPropertiesIsNull())
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (LocationTitleAlredyInUse())
                MessageBox.Show("Используйте Введите другой кабинет", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                _locationService.Insert(new Location { Title = LocationTitle });
                MessageBox.Show("Категория Кабинет!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        private void UpdateLocationData()
        {
            if (SelectedCategorieTitleIsNull())
                MessageBox.Show("Выберите Кабинет", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                _selectedCLocationTitle.Title = LocationTitle;
                _locationService.Update(_selectedCLocationTitle);
                MessageBox.Show("Данные Кабинета  успешно обновлены!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        public void DeleteLocation()
        {
            if (!SelectedCategorieTitleIsNull())
            {
                var result = MessageBox.Show("Вы уверены что хотите удалить?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _locationService.Delete(SelectedLocationTitle);
                    UpdateLists();
                }
            }
            else
                MessageBox.Show("Выберите Кабинет", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
        #region Отвесвенное лицо
        private List<Responsible> _responsible;
        public List<Responsible> Responsibles { get => _responsible; set => Set(ref _responsible, value, nameof(Responsibles)); }
        private ICollection<Responsible> GetResponsible() => _responsibleService.GetUsers().ToList();
        private ResponsibleService _responsibleService;

        private Responsible _selectedthingResponsibleTitle;
        private string _fistName;
        private string _lastName;
        private string _middleName;
        private string _phone;
        private string _jobTitle;
        public string FirstName { get => _fistName; set => Set(ref _fistName, value, nameof(FirstName)); }
        public string LastName { get => _lastName; set => Set(ref _lastName, value, nameof(LastName)); }
        public string MiddleName { get => _middleName; set => Set(ref _middleName, value, nameof(MiddleName)); }
        public string Phone { get => _phone; set => Set(ref _phone, value, nameof(Phone)); }
        public string JobTitle { get => _jobTitle; set => Set(ref _jobTitle, value, nameof(JobTitle)); }
        public Responsible SelectedResponsible
        {
            get => _selectedthingResponsibleTitle;
            set
            {
                Set(ref _selectedthingResponsibleTitle, value, nameof(SelectedResponsible));
                if (value != null)
                {
                    FirstName = value.FirstName;
                    LastName = value.LastName;
                    MiddleName = value.MiddleName;
                    Phone = value.Phone;
                    JobTitle = value.JobTitle;

                }
            }
        }
        private bool SelectedResponsibleTitleIsNull() => SelectedResponsible == null;
        private bool ResponsibleAlredyInUse() => _responsibleService.GetUsers().Any(c => c.FirstName == FirstName && c.LastName == LastName && c.MiddleName == MiddleName);
        private bool ResponsibleAlredyInUsePhone() => _responsibleService.GetUsers().Any(c => c.Phone == Phone);
        private bool ResponsiblePropertiesIsNull() => (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(JobTitle));
        public ICommand AddResponsiblebutton => new Command(Addregistration => AddResponsible());
        public ICommand UpdateResponsibleButton => new Command(updateaccount => UpdateResponsibleData());
        public ICommand DeleteResponsibleButton => new Command(delete => DeleteResponsible());

        private void AddResponsible()
        {
            if (ResponsiblePropertiesIsNull())
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (ResponsibleAlredyInUse())
                MessageBox.Show("Используйте Введите другое ФИО", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (ResponsibleAlredyInUsePhone())
                MessageBox.Show("Используйте Введите другое номер", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                _responsibleService.Insert(new Responsible { FirstName = FirstName, LastName = LastName, MiddleName = MiddleName, Phone = Phone, JobTitle = JobTitle });
                MessageBox.Show("Отвественое лицо Добавлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        private void UpdateResponsibleData()
        {
            if (SelectedResponsibleTitleIsNull())
                MessageBox.Show("Выберите Отвественое лицо", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                _selectedthingResponsibleTitle.FirstName = FirstName;
                _selectedthingResponsibleTitle.LastName = LastName;
                _selectedthingResponsibleTitle.MiddleName = MiddleName;
                _selectedthingResponsibleTitle.Phone = Phone;
                _selectedthingResponsibleTitle.JobTitle = JobTitle;
                _responsibleService.Update(_selectedthingResponsibleTitle);
                MessageBox.Show("Данные категории  успешно обновлены!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        public void DeleteResponsible()
        {
            if (!SelectedResponsibleTitleIsNull())
            {
                var result = MessageBox.Show("Вы уверены что хотите удалить?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _responsibleService.Delete(SelectedResponsible);
                    UpdateLists();
                }
            }
            else
                MessageBox.Show("Выберите Отвественое лицо", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion


        #region Categorie
        private List<ThingCategorie> _categorie;
        public List<ThingCategorie> Categorie { get => _categorie; set => Set(ref _categorie, value, nameof(Categorie)); }
        private ICollection<ThingCategorie> GetThingCategorie() => _thingCategorieService.GetUsers().ToList();
        private ThingCategorieService _thingCategorieService;
        private string _thingCategorieTitle;
        private ThingCategorie _selectedthingCategorieTitle;
        public string ThingCategorieTitle { get => _thingCategorieTitle; set => Set(ref _thingCategorieTitle, value, nameof(ThingCategorieTitle)); }
        public ThingCategorie SelectedThingCategorieTitle
        {
            get => _selectedthingCategorieTitle;
            set
            {
                Set(ref _selectedthingCategorieTitle, value, nameof(SelectedThingCategorieTitle));
                if (value != null)
                {
                    ThingCategorieTitle = value.Title;

                }
            }
        }
        private bool SelectedThingCategorieTitleIsNull() => SelectedThingCategorieTitle == null;
        private bool ThingCategorieTitleAlredyInUse() => _thingCategorieService.GetUsers().Any(c => c.Title == ThingCategorieTitle);
        private bool ThingCategoriePropertiesIsNull() => string.IsNullOrEmpty(ThingCategorieTitle);
        public ICommand AddThingCategoriebutton => new Command(Addregistration => AddThingCategorie());
        public ICommand UpdateThingCategorieButton => new Command(updateaccount => UpdateThingCategorieData());
        public ICommand DeleteThingCategorieButton => new Command(delete => DeleteThingCategorie());

        private void AddThingCategorie()
        {
            if (ThingCategoriePropertiesIsNull())
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (ThingCategorieTitleAlredyInUse())
                MessageBox.Show("Используйте Введите другое название", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                _thingCategorieService.Insert(new ThingCategorie { Title = ThingCategorieTitle });
                MessageBox.Show("Категория Добавлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        private void UpdateThingCategorieData()
        {
            if (SelectedThingCategorieTitleIsNull())
                MessageBox.Show("Выберите категорию", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                _selectedthingCategorieTitle.Title = ThingCategorieTitle;
                _thingCategorieService.Update(_selectedthingCategorieTitle);
                MessageBox.Show("Данные категории  успешно обновлены!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateLists();
            }
        }
        public void DeleteThingCategorie()
        {
            if (!ThingCategoriePropertiesIsNull())
            {
                var result = MessageBox.Show("Вы уверены что хотите удалить?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _thingCategorieService.Delete(SelectedThingCategorieTitle);
                    UpdateLists();
                }
            }
            else
                MessageBox.Show("Выберите категорию", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
    }
}
