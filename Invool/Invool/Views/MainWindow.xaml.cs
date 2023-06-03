using Invool.Data;
using Invool.Data.Entities;
using Invool.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Invool.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;
        public MainWindow(ApplicationDbContext db, User user)
        {
            //new ApplicationDbContext();
            InitializeComponent();
            DataContext = _mainWindowViewModel = new MainWindowViewModel(db);
        }

        private void ClickButton(object sender, RoutedEventArgs e)
        {
            Close();
            //_mainWindowViewModel.ExitApplicantWindow();
        }
    }
}
