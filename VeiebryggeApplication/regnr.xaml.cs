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
            //FillDataGrid();
        }

        string dbConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security = True";



        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(dbConnectionString);
            try
            {
                conn.Open();
                string query = "SELECT regNr, vehicleName, equipment, boolWheels, year FROM Vehicles ";
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


        private void Insert_Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(dbConnectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO Vehicles (regNr, vehicleName, equipment, boolWheels, year) values('" + this.regText.Text + "','" + this.nameText.Text + "','" + this.equipmentText.Text + "','" + this.wheelsText.Text + "','" + this.yearText.Text + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data is saved successfully");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        
    
        
        
        /*
        private void FillDataGrid()

        {
            string CmdString = string.Empty;

            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security=True"))
            {

                CmdString = "SELECT regNr, vehicleName, boolWheels, year, equipment FROM Vehicles";

                SqlCommand cmd = new SqlCommand(CmdString, conn);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("Test");

                sda.Fill(dt);

                grdVehicles.ItemsSource = dt.DefaultView;

            }
        }
        */
        /*
        private void Button_Insert_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security=True"))
            {
                SqlCommand cmd;

                if (regText.Text != "")
                {
                    foreach (DataRow dr in ds.grdVehicles.Columns)
                    {
                        cmd = new SqlCommand("INSERT INTO Vehicles(regNr, vehicleName, boolWheels, year, equipment) values(@regNr,@vehicleName,@boolWheels,@year,@equipment)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@regNr", regText.Text);
                    cmd.Parameters.AddWithValue("@vehicleName", nameText.Text);
                    cmd.Parameters.AddWithValue("@boolWheels", wheelsText.Text);
                    cmd.Parameters.AddWithValue("@year", yearText.Text);
                    cmd.Parameters.AddWithValue("@equipment", equipmentText.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Inserted Successfully");
                    FillDataGrid();
                }
                else
                {
                    MessageBox.Show("Please Provide Details!");
                }
            }
        }
        */
    }
}
