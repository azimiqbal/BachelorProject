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
            PopUp.Height = 0;

        }
        const string dbConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security = True";



        private void Diagnostics_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DEEZ");
        }

        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("NUTS");
        }


        private void regNr_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(dbConnectionString);
                conn.Open();

                string query = "SELECT * FROM Vehicles WHERE regNr=" + int.Parse(regNrText.Text);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    outputText.Text = sdr["vehicleName"].ToString() + sdr["boolWheels"].ToString() + sdr["year"].ToString();
                }
                else
                {
                    PopUp.Height = 400;
                    PopUp.Content = new regnr();

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

