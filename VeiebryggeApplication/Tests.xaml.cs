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

        String selectedTest;
        String dbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security=True";

        private void FillDataGrid()
        {
            string CmdString = string.Empty;

            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                CmdString = "SELECT testID, userId, regNr, rolloverAngle, Equipment, weight, timeRan, centerofgravityx, centerofgravityy, centerofgravityz, error FROM Tests";

                SqlCommand cmd = new SqlCommand(CmdString, conn);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("Test");

                sda.Fill(dt);

                grdTests.ItemsSource = dt.DefaultView;
            }
        }
        
        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTest != null)
            {
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil slette testen med ID " + selectedTest + "?", "Slett test", MessageBoxButton.YesNo); //Displays a selection box to confirm deletion
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SqlConnection conn = new SqlConnection(dbConnectionString);
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM Tests WHERE testID='" + selectedTest + "'";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            //MessageBox.Show("Deleted test with ID " + selectedTest);
                            FillDataGrid();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    case MessageBoxResult.No:
                        //MessageBox.Show("Test not deleted");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Ingen test valgt");
            }

        }

        private void grdTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTest = null;
            try
            {
                DataRowView drv = (DataRowView)grdTests.SelectedItem;

                if (drv != null)
                {
                    selectedTest = drv["testID"].ToString();
                    Console.WriteLine(selectedTest);
                }
            }
            catch(Exception er)
            {
                Console.WriteLine(er);
            }
        }

        private void Export_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTest != null)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook workbook = app.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Worksheets[1];

                SqlConnection conn = new SqlConnection(dbConnectionString);
                conn.Open();

                string query = "SELECT * FROM tests WHERE testid=" + int.Parse(selectedTest);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sdr = cmd.ExecuteReader();

                List<string> testDataList = new List<string> { };

                List<string> columnNameList = new List<string> { "testID", "userId", "regNr", "rolloverAngle", "Equipment", "weight", "timeRan", "centerofgravityx", "centerofgravityy", "centerofgravityz", "error" };

                if (sdr.Read())
                {
                    worksheet.Name = "TestData";

                    for (int column = 0; column < columnNameList.Count; column++)
                    {
                        testDataList.Add(sdr[columnNameList[column]].ToString());
                        worksheet.Cells[1, column + 1].value = columnNameList[column];
                        worksheet.Cells[2, column + 1].value = testDataList[column];
                    }
                }

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Test_ID_" + selectedTest; // Default file name
                dlg.DefaultExt = ".xlsx"; // Default file extension
                dlg.Filter = "Excel regneark (.xlsx)|*.xlsx"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    conn.Close();
                    workbook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    MessageBox.Show("Dokument lagret under: " + filename);
                    workbook.Close();
                    app.Quit();
                }

            }
            else
            {
                MessageBox.Show("Ingen test valgt");
            }
        }

        private void Import_Button_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Title = "Import excel sheet";
            ofd.Filter = "EXCEL sheets|*.xlsx";
            ofd.InitialDirectory = @"C:\";

            Nullable<bool> result = ofd.ShowDialog();

            string openFile = ofd.FileName;


            //Create COM Objects. Create a COM object for everything that is referenced
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(openFile);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            int colCount = 11;

            List<string> testDataList = new List<string> { };

            for (int j = 1; j <= colCount; j++)
            {
                //new line
                if (j == 1)
                    Console.Write("\r\n");

                //write the value to the console
                if (xlRange.Cells[2, j] != null && xlRange.Cells[2, j].Value2 != null)
                {
                    testDataList.Add(xlRange.Cells[2, j].Value2.ToString());
                }
            }

            xlWorkbook.Close();
            xlApp.Quit();
            
            SqlConnection conn = new SqlConnection(dbConnectionString);
            try
            {
                conn.Open();
                //"testID", "userId", "regNr", "rolloverAngle", "Equipment", "weight", "timeRan", "centerofgravityx", "centerofgravityy", "centerofgravityz", "error"

                string query = "INSERT INTO tests (testID, userId, regNr, rolloverAngle, Equipment, weight, centerofgravityx, centerofgravityy, centerofgravityz, error) values('" + testDataList[0] + "','" + testDataList[1] + "','" + testDataList[2] + "','" + testDataList[3] + "','" + testDataList[4] + "','" + testDataList[5] + "','"  + testDataList[7] + "','" + testDataList[8] + "','" + testDataList[9] + "','" + testDataList[10] + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                FillDataGrid();
                conn.Close();
                MessageBox.Show("Den valgte filen er importert til databasen");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
