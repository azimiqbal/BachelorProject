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

namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for helpPage_Tests.xaml
    /// </summary>
    public partial class helpPage_Tests : Page
    {
        public helpPage_Tests()
        {
            InitializeComponent();
        }

        private void Button_Click_runTest2(object sender, RoutedEventArgs e)
        {
            NavigationService service = NavigationService.GetNavigationService(this);
            service.Navigate(new Uri("helpPage_RunTest.xaml", UriKind.RelativeOrAbsolute));
        }


        private void Button_Click_vehicles2(object sender, RoutedEventArgs e)
        {
            NavigationService service = NavigationService.GetNavigationService(this);
            service.Navigate(new Uri("helpPageVehicles.xaml", UriKind.RelativeOrAbsolute));
        }

        private void tilbake_Click2(object sender, RoutedEventArgs e)
        {
            NavigationService service = NavigationService.GetNavigationService(this);
            service.Navigate(new Uri("helpPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
