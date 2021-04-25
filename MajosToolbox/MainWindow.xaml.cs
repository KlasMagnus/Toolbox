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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MajosDashboard.Viewmodels;

namespace MajosDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Appointment_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AppointmentViewModel();
        }

        private void MainMenu_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = null;
        }

        private void Cryptography_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CryptographyViewModel();
        }

        private void TimeReport_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ReportViewModel();
        }
    }
}
