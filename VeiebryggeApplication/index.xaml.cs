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
using System.Diagnostics;
using System.Windows.Shapes;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using System.Data;
using System.Windows.Media;


namespace VeiebryggeApplication
{
    public partial class index : Page
    {
        public index()
        {
            InitializeComponent();
            Main.Content = new RunTest();
        }

        private void BtnRuntest(object sender, RoutedEventArgs e)
        {
            Main.Content = new RunTest();
        }

        private void btnTests(object sender, RoutedEventArgs e)
        {
            Main.Content = new Tests();
        }

        private void BtnVehicles(object sender, RoutedEventArgs e)
        {
            Main.Content = new regnr();
        }

        private void BtnHelp(object sender, RoutedEventArgs e)
        {
            Main.Content = new helpPage();
        }
    }
}