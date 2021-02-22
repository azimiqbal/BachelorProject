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
    /// Interaction logic for testRun.xaml
    /// </summary>
    public partial class testRun : Page
    {
        public testRun()
        {
            InitializeComponent();
        }

   
        private bool IsValid()
        {
            if (TextBox.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Error! Please enter a valid registration number");
                return false;
            }
            return true;
        }

        private void Sbmt_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                using (SqlConnection conn = new SqlConnection(@"C:\Users\Azim\Documents\GitHub\BachelorProject\Bachelor_WPF_App\Bachelor_WPF_App\forsvaret.mdf"))
                {
                    string query = "SELECT * FROM Vehicles WHERE regNr = '"
                        + TextBox.Text.Trim() + "'";

                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    DataTable dta = new DataTable();
                    sda.Fill(dta);
                    if (dta.Rows.Count == 1)
                    {
                        MessageBox.Show("What is this dude");
                    }
                    else
                    {
                        MessageBox.Show("This registration number is not registered");
                    }
                }
            }
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("RANA ASLAM");
        }
    }
}

