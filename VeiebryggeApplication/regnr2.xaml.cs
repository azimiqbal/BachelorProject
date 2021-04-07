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
    /// <summary>
    /// Interaction logic for regnr2.xaml
    /// </summary>
    public partial class regnr2 : Page
    {
        public regnr2()
        {
            InitializeComponent();
            FillDataGrid();
        }

        //Variabel som holder på radio knapp verdien
        string type;

        const string dbConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security = True"; //string that connects to the database

        //Lager en kobling til databasen og fyller datatabellen med alt innholdet
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
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
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

        private bool IsValid_1()
        {
            if (regText.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Error! Please enter a valid registration number");
                return false;
            }
            return true;
        }

        //Knapp som setter inn dataen fra tekstboksene i databasen
        private void Insert_Button_Click(object sender, RoutedEventArgs e)
        {
            //Sjekker at alle feltene er fylt ut riktig
            if (IsValid())
            {
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil legge til dette kjøretøyet ?", "Legg til kjøretøy", MessageBoxButton.YesNo); //Displays a selection box to confirm deletion

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SqlConnection conn = new SqlConnection(dbConnectionString);

                        //Prøver å sette inn data fra tekstboksene i databasen
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
        }


        //Funskjoner som sørger for at tekstboksene har riktig type input
        private void regText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); //makes sure that input can only be integers
            e.Handled = regex.IsMatch(e.Text);
        }

        private void yearText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void nameText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-æ,ø,å]+"); //makes sure that input can only be letters from a-å
            e.Handled = regex.IsMatch(e.Text);
        }

        private void wheelsText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Funskjon som søker etter om en verdi er i databasen 
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
            if (IsValid_1())
            {
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil slette dette kjøretøyet ?", "Slett kjøretøy", MessageBoxButton.YesNo); //Displays a selection box to confirm deletion

                switch (result)
                {
                    case MessageBoxResult.Yes:
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
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        //Knapp som endrer verdiene i databasen på et valgfritt kjøretøy
        private void Edit_Button_Click(object sender, RoutedEventArgs e) //function for editing
        {
            if (IsValid_1())
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

            if (IsValid())
            {
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil gjennomføre denne endringen ?", "Endre kjøretøy opplysninger", MessageBoxButton.YesNo); //Displays a selection box to confirm deletion

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
        }

        //Funksjon som kjører hver gang teksten i registreringsnummer feltet er endret 
        private void regText_TextChanged(object sender, TextChangedEventArgs e) //autocomplete
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




        private void Close_Button_Click(object sender, RoutedEventArgs e) //function to exit out of regnr form
        {
            this.Content = 0;
            this.Height = 0;
        }


        private void Belte_RadioButton_Checked(object sender, RoutedEventArgs e) //when user select belte in radiobutton
        {
            type = "Belte";
        }

        private void Hjul_RadioButton_Checked(object sender, RoutedEventArgs e) //when user select hjul in radiobutton
        {
            type = "Hjul";
        }
    }
}
