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
using System.Data.SqlClient;
using System.Data;

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

        private bool IsValid()
        {
            if (LocalUsernameBox.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Error! Please enter a valid user name");
                return false;
            }
            else if (LocalPasswordBox.Password.TrimStart() == string.Empty)
            {
                MessageBox.Show("Error! Please enter a valid password");
                return false;
            }
            return true;
        }
    

    private void LocalLoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Azim\Documents\GitHub\BachelorProject\Bachelor_WPF_App\Bachelor_WPF_App\forsvaret.mdf;Integrated Security=True"))
                {
                    string query = "SELECT * FROM USERS WHERE UserName = '" + LocalUsernameBox.Text.Trim() +
                        "' AND Password = '" + LocalPasswordBox.Password.Trim() + "'";

                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    DataTable dta = new DataTable();
                    sda.Fill(dta);
                    if (dta.Rows.Count == 1)
                    {
                        NavigationService service = NavigationService.GetNavigationService(this);
                        service.Navigate(new Uri("testRun.xaml", UriKind.RelativeOrAbsolute));
                    } else
                    {
                        MessageBox.Show("Username or password is not correct");
                    }
                }
            }
        }
    }

}

//private void LocalLoginButton_Click(object sender, RoutedEventArgs e)
//{
//    string username = LocalUsernameBox.Text;
//    string password = LocalPasswordBox.Password;

//    if ((this.LocalUsernameBox.Text == "Deez") && (this.LocalPasswordBox.Password == "Nuts"))
//    {
//        NavigationService service = NavigationService.GetNavigationService(this);
//        service.Navigate(new Uri("testRun.xaml", UriKind.RelativeOrAbsolute));
//    }
//    else
//    {
//        MessageBox.Show("you are not granted with access");
//    }

//}