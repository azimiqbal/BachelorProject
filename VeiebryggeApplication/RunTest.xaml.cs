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
using System.Data.SqlClient;
using System.Data;


namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for RunTest.xaml
    /// </summary>
    public partial class RunTest : Page
    {
        public RunTest()
        {
            InitializeComponent();

        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DEEZ");
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("NUTS");
        }



        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
