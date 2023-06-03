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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow(ApplicationDbContext ctx,RecordSchool recordSchool)
        {
            InitializeComponent();
            DataContext = new EditRecordViewModel(ctx, recordSchool);
        }
    }
}
