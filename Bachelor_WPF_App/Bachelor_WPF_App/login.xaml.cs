using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bachelor_WPF_App
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
        }

        private void LocalLoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LocalUsernameBox.Text;
            string password = LocalPasswordBox.Password;

            if ((this.LocalUsernameBox.Text == "Deez") && (this.LocalPasswordBox.Password == "Nuts"))
            {
                NavigationService service = NavigationService.GetNavigationService(this);
                service.Navigate(new Uri("testRun.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                MessageBox.Show("you are not granted with access");
            }

        }
    }
}
