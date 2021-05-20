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
        //string med lokasjon til databasen
        String dbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security=True";


        //knapp som kjører diagnose på systemet
        private void Diagnostics_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kjører diagnostisk test...");
        }

        //knapp som intialiserer testen
        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Starter test...");
        }

        //funksjon som vil autofylle tekst om regnr til kjøretøyet finnes i databasen
        //hvis regnr ikke finnes i databasen, vil det åpnes et pop-up vindu som tilsvarer "regnr.xaml"
        private void regNr_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                //kobler til databasen
                SqlConnection conn = new SqlConnection(dbConnectionString);
                conn.Open();

                //query til databasen der man vdlger alle regnr i databasene
                string query = "SELECT * FROM Vehicles WHERE regNr=" + int.Parse(regNrText.Text);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    //vis kjøretøy info som tilsvarer det regnr som bruker har skrevet inn
                    outputText.Text = "Navn: " + sdr["vehicleName"].ToString() + "\n Type: " + sdr["type"].ToString() + "\n Årstall: " + sdr["year"].ToString();
                }
                else
                {
                    //om kjøretøyet ikke finnes i databasen
                    outputText.Text = "Kjøretøy info: Ikke funnet";
                    PopUp.Height = 900;
                    //PopUp.Width = 500;

                    //åpne siden kalt regnr
                    PopUp.Content = new regnr();

                }
                conn.Close();
            }
            //fanger opp andre feil
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

