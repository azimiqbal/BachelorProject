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

        const string dbConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security = True";

        
        private void FillDataGrid()
        {
            string CmdString = string.Empty;
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {

                CmdString = "SELECT regNr, vehicleName, boolWheels, year FROM Vehicles";
                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Vehicles");
                sda.Fill(dt);
                grdVehicles.ItemsSource = dt.DefaultView;
            }
        }


        private void Insert_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                SqlConnection conn = new SqlConnection(dbConnectionString);
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Vehicles (regNr, vehicleName, boolWheels, year) values('" + this.regText.Text + "','" + this.nameText.Text + "','" + this.wheelsText.Text + "','" + this.yearText.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data is saved successfully");
                    FillDataGrid();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
            }
        }


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
            else if (wheelsText.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Error! Please enter the number of wheels on the vehicle");
                return false;
            }
            else if (yearText.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Error! Please enter a valid year");
                return false;
            }
            return true;
        }

        private void regText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void yearText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void nameText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-æ,ø,å]+");
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
                string query = "SELECT * FROM Vehicles WHERE CONCAT(`regNr`, `vehicleName`, `boolWheels`, `year`) like '%" + valuetoSearch + "%'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Vehicles");
                sda.Fill(dt);
                grdVehicles.ItemsSource = dt.DefaultView;
            }
        }


        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("deez");
        }



        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(dbConnectionString);
            try
            {
                conn.Open();
                string query = "DELETE FROM Vehicles WHERE regNr='" + this.regText.Text+"'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted");
                FillDataGrid();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /*
        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(dbConnectionString);
            conn.Open();

            string query = "SELECT * FROM Vehicles WHERE regNr=" + int.Parse(regText.Text);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                nameText.Text = sdr["vehicleName"].ToString();
                wheelsText.Text = sdr["boolWheels"].ToString();
                yearText.Text = sdr["year"].ToString();
            }
            else
            {
                regText.Text = "";
                nameText.Text = "";
                wheelsText.Text = "";
                yearText.Text = "";
                MessageBox.Show("No Data For This Id");
            }
            conn.Close();
        }
        */
        private void regText_TextChanged(object sender, TextChangedEventArgs e)
        {
         /*   try {
                SqlConnection conn = new SqlConnection(dbConnectionString);
                conn.Open();
                string query = "SELECT * FROM Vehicles WHERE regNr=" + int.Parse(regText.Text);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    nameText.Text = sdr["vehicleName"].ToString();
                    wheelsText.Text = sdr["boolWheels"].ToString();
                    yearText.Text = sdr["year"].ToString();
                }
                else
                {
                    regText.Text = "";
                    nameText.Text = "";
                    wheelsText.Text = "";
                    yearText.Text = "";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void regText_Input(object sender, TextCompositionEventArgs e)
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
                    wheelsText.Text = sdr["boolWheels"].ToString();
                    yearText.Text = sdr["year"].ToString();
                }
                else
                {
                    regText.Text = "";
                    nameText.Text = "";
                    wheelsText.Text = "";
                    yearText.Text = "";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    

        /*
        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(dbConnectionString);
            try
            {
                conn.Open();
                string query = "SELECT regNr, vehicleName, boolWheels, year FROM Vehicles ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Vehicles");
                sda.Fill(dt);
                grdVehicles.ItemsSource = dt.DefaultView;
                sda.Update(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        */
    }
}

