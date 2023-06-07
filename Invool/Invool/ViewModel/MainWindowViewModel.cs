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
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ApplicationDbContext ctx,Window window)
        {
            Window = window;
            _ctx = ctx;
            _schoolService = new(ctx);
            _thingCategorieService = new(ctx);
            _responsibleService = new(ctx);
            _locationService = new(ctx);
            _thingService = new(ctx);
            DateOfPositionFilthers = new List<string> { "Не выбрана" };
            SortResponsible = new List<string> { "Не выбрана" };
            //ThingCategorieSorts = new List<string> { "Не выбрана" };
            LocationsSort = new List<string> { "Не выбрана" };
            //FilterthingTitle = new List<string> { "Не выбрана" };
            //ThingCategorieSorts.AddRange(_thingCategorieService.GetUsers().Select(c => c.Title));
            SortResponsible.AddRange(_responsibleService.GetUsers().Select(c => c.FullName));
            LocationsSort.AddRange(_locationService.GetUsers().Select(c => c.Title));
            //FilterthingTitle.AddRange(_thingService.GetUsers().Select(c => c.Title));
            //SelectedThingCategorieSort = ThingCategorieSorts[0];
            SelectedResponsible = SortResponsible[0];
            SelectedLocationsSort = LocationsSort[0];
            //SelectedThingTitle = FilterthingTitle[0];
            //StartDate = DateTime.Now;
            UpdateLists();
        }
        //private DateTime _startDate;

        //public DateTime StartDate
        //{
        //    get => _startDate; set
        //    {
        //        Set(ref _startDate, value, nameof(StartDate));
        //        UpdateLists();
        //    }
        //}
        private Window Window;
        private ApplicationDbContext _ctx;
        private RecordSchoolService _schoolService;
        private RecordSchool _selectedRecordSchool;
        private List<RecordSchool> _recordSchools;
        private ThingCategorieService _thingCategorieService;
        private ResponsibleService _responsibleService;
        private LocationService _locationService;
        private ThingService _thingService;
        private DateTime _startDate;
        private DateTime _endDate;
        public DateTime StartDate
        {
            get => _startDate; set
            {
                Set(ref _startDate, value, nameof(StartDate));
                UpdateLists();
            }
        }
        public DateTime EndDate
        {
            get => _endDate; set
            {
                Set(ref _endDate, value, nameof(EndDate));
                UpdateLists();
            }
        }
        private ICollection<RecordSchool> GetRecordSchool() => SearchTitle(SearchThingCategorie(SearchArticle(SearchSaleOffDate(FiltherLocations(FiltherResponsible(_schoolService.GetUsers().Where(d => d.PostingDate >= StartDate && d.PostingDate <= EndDate).ToList()))))));
        #region Filtr
        //private List<RecordSchool> (List<RecordSchool> recordSchools)
        //{
        //    if (!(SelectedThingCategorieSort == ThingCategorieSorts[0]))
        //        return recordSchools.Where(p => p.Things.ThingCategories.Title == SelectedThingCategorieSort).ToList();
        //    else
        //        return recordSchools;
        //}
        private List<RecordSchool> SearchThingCategorie(List<RecordSchool> recordSchools)
        {
            if (!string.IsNullOrEmpty(SelectedThingCategorieSort))
                return recordSchools.Where(p => p.Things.ThingCategories.Title.ToLower().Contains(SelectedThingCategorieSort.ToLower())).ToList();
            else
                return recordSchools;
        }
        private List<RecordSchool> SearchTitle(List<RecordSchool> recordSchools)
        {
            if (!string.IsNullOrEmpty(SelectedThingTitle))
                return recordSchools.Where(p => p.Things.Title.ToLower().Contains(SelectedThingTitle.ToLower())).ToList();
            else
                return recordSchools;
        }
        private List<RecordSchool> FiltherResponsible(List<RecordSchool> recordSchools)
        {
            if (!(SelectedResponsible == SortResponsible[0]))
                return recordSchools.Where(p => p.Locations.Responsibles.FullName == SelectedResponsible).ToList();
            else
            return recordSchools;
        }
        private List<RecordSchool> FiltherLocations(List<RecordSchool> recordSchools)
        {
            if (!(SelectedLocationsSort == LocationsSort[0]))
                return recordSchools.Where(p => p.Locations.Title == SelectedLocationsSort).ToList();
            else
                return recordSchools;
        }
        //private List<RecordSchool> FiltherTitle(List<RecordSchool> recordSchools)
        //{
        //    if (!(SelectedThingTitle == FilterthingTitle[0]))
        //        return recordSchools.Where(p => p.Things.Title == SelectedThingTitle).ToList();
        //    else
        //        return recordSchools;
        //}
        private List<RecordSchool> SearchSaleOffDate(List<RecordSchool> recordSchools)
        {
            if (!string.IsNullOrEmpty(DateOffDateSearch))
                return recordSchools.Where(p => p.WriteOffDate.ToLower().Contains(DateOffDateSearch.ToLower())).ToList();
            else
                return recordSchools;
        }
        private List<RecordSchool> SearchArticle(List<RecordSchool> recordSchools)
        {
            if (!string.IsNullOrEmpty(SearchArticleValue))
                return recordSchools.Where(p => p.Things.Article.ToLower().Contains(SearchArticleValue.ToLower())).ToList();
            else
                return recordSchools;
        }
        public List<string> DateOfPositionFilthers { get; set; } = null!;
        public List<string> SortResponsible { get; set; } = null!;
        public List<string> ThingCategorieSorts { get; set; } = null!;
        public List<string> LocationsSort { get; set; } = null!;
        public List<string> FilterthingTitle { get; set; } = null!;
        private string _searchDatePositionValue;
        private string _selectedProductFilther;
        private string _selectedProductFiltherTitle;
        private string _selectedLocationsSort;
        private string _selectedThingCategorieSort;
        private string _selectedResponsible;
        private string _searchArticle;
        private string _selectedThingTitle;
        public string SelectedThingTitle
        {
            get => _selectedThingTitle; set
            {
                if (Set(ref _selectedThingTitle, value, nameof(SelectedThingTitle)))
                    UpdateLists();
            }
        }
        public string SearchArticleValue
        {
            get => _searchArticle; set
            {
                if (Set(ref _searchArticle, value, nameof(SearchArticleValue)))
                    UpdateLists();
            }
        }
        public string DateOffDateSearch
        {
            get => _searchDatePositionValue; set
            {
                if (Set(ref _searchDatePositionValue, value, nameof(DateOffDateSearch)))
                    UpdateLists();
            }
        }
        public string SelectedProductFilther
        {
            get => _selectedProductFilther; set
            {
                if (Set(ref _selectedProductFilther, value, nameof(SelectedProductFilther)))
                    UpdateLists();
            }
        }
        public string SelectedProductFiltherTitle
        {
            get => _selectedProductFiltherTitle; set
            {
                if (Set(ref _selectedProductFiltherTitle, value, nameof(SelectedProductFiltherTitle)))
                    UpdateLists();
            }
        }
        public string SelectedLocationsSort
        {
            get => _selectedLocationsSort; set
            {
                if (Set(ref _selectedLocationsSort, value, nameof(SelectedLocationsSort)))
                    UpdateLists();
            }
        }
        public string SelectedThingCategorieSort
        {
            get => _selectedThingCategorieSort; set
            {
                if (Set(ref _selectedThingCategorieSort, value, nameof(SelectedThingCategorieSort)))
                    UpdateLists();
            }
        }
        public string SelectedResponsible
        {
            get => _selectedResponsible; set
            {
                if (Set(ref _selectedResponsible, value, nameof(SelectedResponsible)))
                    UpdateLists();
            }
        }

        private string _searchSaleProductValue;
        public string SearchSaleProductValue
        {
            get => _searchSaleProductValue; set
            {
                if (Set(ref _searchSaleProductValue, value, nameof(SearchSaleProductValue)))
                    UpdateLists();
            }
        }
        #endregion
        public List<RecordSchool> RecordSchools { get => _recordSchools; set => Set(ref _recordSchools, value, nameof(RecordSchools)); }
        public RecordSchool SelectedRecordSchool { get => _selectedRecordSchool; set => Set(ref _selectedRecordSchool, value, nameof(SelectedRecordSchool)); }
        
        private bool SelectedRecordSchoolIsNull() => SelectedRecordSchool == null;
        private void UpdateLists()
        {
            RecordSchools = new List<RecordSchool>(GetRecordSchool());
        }
        public void ExitApplicantWindow()
        {
            var CurrentWindow = Application.Current.MainWindow;
            CurrentWindow.Close();
        }

        private void OpenAddDataWindow()
        {
            var DirectorWindow = new AddDataWindow(_ctx);
            var CurrentWindow = Application.Current.MainWindow;
            DirectorWindow.Show();
            CurrentWindow.Close();
            Window.Close();
        }

        private void OpenAddRecordSchoolWindow()
        {
            var DirectorWindow = new CreateWindow(_ctx);
            var CurrentWindow = Application.Current.MainWindow;
            DirectorWindow.ShowDialog();
            Application.Current.MainWindow = DirectorWindow;
            UpdateLists();
        }
        private void OpenEditRecordSchoolWindow()
        {
            if (!SelectedRecordSchoolIsNull())
            {
                var DirectorWindow = new EditWindow(_ctx,SelectedRecordSchool);
                var CurrentWindow = Application.Current.MainWindow;
                DirectorWindow.ShowDialog();
                Application.Current.MainWindow = DirectorWindow;
                UpdateLists();
            }
            else
                MessageBox.Show("Выберите Запись", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public void DeleteRecordSchool()
        {
            if (!SelectedRecordSchoolIsNull())
            {
                var result = MessageBox.Show("Вы уверены что хотите удалить?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _schoolService.Delete(SelectedRecordSchool);
                    _thingService.Delete(SelectedRecordSchool.Things);
                    UpdateLists();
                }
            }
            else
                MessageBox.Show("Выберите Запись", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #region Commands
        public ICommand AddRecordSchoolButton => new Command(authorization => OpenAddRecordSchoolWindow());
        public ICommand EditRecordSchoolButton => new Command(authorization => OpenEditRecordSchoolWindow());
        public ICommand DeleteRecordSchoolButton => new Command(authorization => DeleteRecordSchool());
        public ICommand OpenAddDataWindowButton => new Command(authorization => OpenAddDataWindow());

        #endregion
    }
}
