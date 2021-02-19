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
    /// Interaction logic for Tests.xaml
    /// </summary>
    public partial class Tests : Page
    {
        public Tests()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void FillDataGrid()

        {
            string CmdString = string.Empty;

            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security=True"))
            {

                CmdString = "SELECT testID, regNr, weight, rolloverAngle FROM Tests";

                SqlCommand cmd = new SqlCommand(CmdString, conn);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("Test");

                sda.Fill(dt);

                grdTests.ItemsSource = dt.DefaultView;

            }

        }

    }
}
