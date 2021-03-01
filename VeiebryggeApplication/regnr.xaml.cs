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
    /// Interaction logic for regnr.xaml
    /// </summary>
    public partial class regnr : Page
    {
        public regnr()
        {
            InitializeComponent();
            FillDataGrid();
        }

        string type; //variable that keeps track of the radiobutton
        const string dbConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security = True"; //string that connects to the database

        
        private void FillDataGrid() //function that loads the grid where we see the lists of rows and columns
        {
            string CmdString = string.Empty;
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {

                CmdString = "SELECT regNr, vehicleName, type, year FROM Vehicles";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Vehicles");
                sda.Fill(dt);
                grdVehicles.ItemsSource = dt.DefaultView;
            }
        }


        private bool IsValid() //check to see if the user has left any fields empty, if so, print error messages
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


        private void Insert_Button_Click(object sender, RoutedEventArgs e) //method/function for inserting vehicle information into database
        {
            if (IsValid()) //check to see if the user has not left any empty fields
            {
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil legge til dette kjøretøyet ?", "Legg til kjøretøy", MessageBoxButton.YesNo); //Displays a selection box to confirm deletion

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


        private void Delete_Button_Click(object sender, RoutedEventArgs e) //function for deleting
        {
            if (IsValid_1())
            {
                
                    MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil slette dette kjøretøyet ?" , "Slett kjøretøy", MessageBoxButton.YesNo); //Displays a selection box to confirm deletion
                
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
                        //MessageBox.Show("Deleted");
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
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil gjennomføre denne endringen ?" , "Endre kjøretøy opplysninger", MessageBoxButton.YesNo); //Displays a selection box to confirm deletion

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

