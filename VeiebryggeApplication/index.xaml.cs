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
    /// Interaction logic for index.xaml
    /// </summary>
    public partial class index : Page
    {
        public index()
        {
            InitializeComponent();
        }

        private void BtnMenu1(object sender, RoutedEventArgs e)
        {
            Main.Content = new RunTest();
        }
        private void BtnMenu2(object sender, RoutedEventArgs e)
        {
            Main.Content = new Tests();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
