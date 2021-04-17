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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Drawing;


namespace VeiebryggeApplication
{
    public partial class regnr : Page
    {
        public regnr()
        {
            InitializeComponent();
            FillDataGrid();
        }

        //Variabel som holder på radio knapp verdien
        string type;

        //String med lokasjonen til databasen
        const string dbConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security = True"; //string that connects to the database

        //Lager en kobling til databasen og fyller datatabellen med alt innholdet
        //Dersom det sendes med en string fylles tabellen bare med radene som inneholder denne stringen
        private void FillDataGrid(string search = "") 
        {
            String filterText = comboFilter.Text;

            switch (filterText)
            {
                case @"Alle felter":
                    filterText = "";
                    break;
                case @"RegNr":
                    filterText = "regNr";
                    break;
                case @"vehicleName":
                    filterText = "vehicleName";
                    break;
                case @"Type":
                    filterText = "type";
                    break;
                case @"Year":
                    filterText = "year";
                    break;
                default:
                    Console.WriteLine("Nothing");
                    break;
            }


            string query;
            if (!(search.TrimStart() == string.Empty))
            {
                if (filterText == "")
                {
                    //get values from the Vechicles table
                    query = "SELECT * FROM Vehicles WHERE regNr LIKE '%" + search + "%' or vehicleName LIKE '%" + search + "%' or type LIKE '%" + search + "%' or year LIKE '%" + search + "%'";
                    //query += " INNER JOIN USERS on tests.userID = USERS.userId";
                }
                else
                {
                    query = "SELECT * FROM Vehicles WHERE " + filterText + " LIKE '%" + search + "%'";
                }
            }
            else
            {
                query = "SELECT * FROM Vehicles";
            }


            string CmdString = string.Empty;
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                //CmdString = "SELECT regNr, vehicleName, type, year FROM Vehicles";

                //Henter data fra test databasen
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                //Fyller datagrid med data
                DataTable dt = new DataTable("Vehicles");
                sda.Fill(dt);
                grdVehicles.ItemsSource = dt.DefaultView;
            }
        }


        //Verifiserer at alle tekstfeltene er fylt ut riktig
        private bool IsValid() 
        {
            if (regText.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Error! Please enter a valid registration number");
                return false;
            }
            else if (nameText.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Error! Please enter a valid vehicle name");
                return false;
            }
            else if (hjulName.IsChecked == false && belteName.IsChecked == false)
            {
                MessageBox.Show("Error! Please select a vehicle type");
                return false;
            }
            else if (yearText.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Error! Please enter a valid year");
                return false;
            }
            return true;
        }



        //Ved trykk på denne knappen, vil dataen i tekstboksene legges til i databasen
        private void Insert_Button_Click(object sender, RoutedEventArgs e)
        {
            //Sjekker at alle feltene er fylt ut riktig
            if (IsValid())
            {
                //Åpner en meldingsboks for å bekrefte innsetting av data
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil legge til dette kjøretøyet ?", "Legg til kjøretøy", MessageBoxButton.YesNo); 
                switch (result)
                {
                    //om bruker "sier" ja for å sette inn data
                    case MessageBoxResult.Yes:
                        //kobler til databasen
                        SqlConnection conn = new SqlConnection(dbConnectionString);
                    
                        //Sette inn data fra tekstboksene i databasen
                        try
                            {
                            //Legger til kjøretøy i databasen
                            conn.Open();
                            string query = "INSERT INTO Vehicles (regNr, vehicleName, type, year) values('" + this.regText.Text + "','" + this.nameText.Text + "','" + type + "','" + this.yearText.Text + "')";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            FillDataGrid();
                            conn.Close();
                            }
                        //Dersom kjøretøyet ikke kunne settes inn, vises en meldingsboks med feilen
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                        //Hvis brukeren sier nei til å sette inn data skjer det ikke noe
                        case MessageBoxResult.No:
                            break;
                }
            }
        }


        //RegEx-Funskjoner som sørger for at tekstboksene har riktig type input
        private void regText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); //regnr kan kun være int
            e.Handled = regex.IsMatch(e.Text);
        }

        private void yearText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); //årstall kan kun være int
            e.Handled = regex.IsMatch(e.Text);
        }

        private void nameText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-æ,ø,å]+"); //kjøretøy-navn kan kun være string mellom a-å
            e.Handled = regex.IsMatch(e.Text);
        }


        //Funskjon som søker etter om en verdi er i Vehicles tabellen 
        public void searchData(string valuetoSearch)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                string query = "SELECT * FROM Vehicles WHERE CONCAT(`regNr`, `vehicleName`, `type`, `year`) like '%" + valuetoSearch + "%'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Vehicles");
                sda.Fill(dt);
                grdVehicles.ItemsSource = dt.DefaultView;
            }
        }

        //Knapp som sletter objekter fra databasen
        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                //Åpner en meldingsboks for å bekrefte sletting av kjøretøy
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil slette dette kjøretøyet ?" , "Slett kjøretøy", MessageBoxButton.YesNo); 
                switch (result)
                {
                    case MessageBoxResult.Yes:
                    SqlConnection conn = new SqlConnection(dbConnectionString);
                        try
                        {
                        //Sletter den testen med regNr som er lik den valgte testen fra databasen
                        conn.Open();
                        string query = "DELETE FROM Vehicles WHERE regNr='" + this.regText.Text + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        FillDataGrid();
                        conn.Close();
                        }
                        //Dersom kjøretøyet ikke kunne slettes vises en meldingsboks med feilen
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    //Hvis brukeren sier nei til å slette kjøretøyet skjer det ikke noe
                    case MessageBoxResult.No:
                        break;
                }
            }
        }
 
        //Knapp som endrer verdiene i databasen på et valgfritt kjøretøy, delete + insert = edit
        private void Edit_Button_Click(object sender, RoutedEventArgs e) 
        {
            if (IsValid())
            {
                SqlConnection conn = new SqlConnection(dbConnectionString);
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Vehicles WHERE regNr='" + this.regText.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    FillDataGrid();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil gjennomføre denne endringen ?" , "Endre kjøretøy opplysninger", MessageBoxButton.YesNo);
                switch (result)
                {
                   case MessageBoxResult.Yes:
                    SqlConnection conn = new SqlConnection(dbConnectionString);
                    try
                    {
                        conn.Open();
                        string query = "INSERT INTO Vehicles (regNr, vehicleName, type, year) values('" + this.regText.Text + "','" + this.nameText.Text + "','" + type + "','" + this.yearText.Text + "')";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        FillDataGrid();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                    case MessageBoxResult.No:
                        break;
                    }
                 }

        //Funksjon som kjører hver gang teksten i registreringsnummer feltet er endret 
        private void regText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(dbConnectionString);
                conn.Open();

                string query = "SELECT * FROM Vehicles WHERE regNr=" + int.Parse(regText.Text);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    nameText.Text = sdr["vehicleName"].ToString();
                    type = sdr["type"].ToString();
                    yearText.Text = sdr["year"].ToString();
                }
                else
                {
                    nameText.Text = "";
                    type = "";
                    yearText.Text = "";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillDataGrid(searchTxt.Text);
        }


        private void comboFilter_DropDownClosed(object sender, EventArgs e)
        {
            FillDataGrid(searchTxt.Text);
        }


        //for å lukke vindu
        private void Close_Button_Click(object sender, RoutedEventArgs e) 
        {
            this.Content = 0;
            this.Height = 0;
        }

        //når bruker velger belte 
        private void Belte_RadioButton_Checked(object sender, RoutedEventArgs e) 
        {
            type = "Belte";
        }
        //når bruker velger hjul 
        private void Hjul_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            type = "Hjul";
        }
    }
}

