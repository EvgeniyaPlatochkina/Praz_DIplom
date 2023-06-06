using Invool.Data;
using Invool.Data.Entities;
using Invool.Services;
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
    /// Логика взаимодействия для LocationInfomationThingWindow.xaml
    /// </summary>
    public partial class LocationInfomationThingWindow : Window
    {
        public LocationInfomationThingWindow(ApplicationDbContext ctx,Location locationService)
        {
            InitializeComponent();
        }
    }
}
