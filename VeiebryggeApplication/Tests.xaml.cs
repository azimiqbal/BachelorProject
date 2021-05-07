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
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfDocument = PdfSharp.Pdf.PdfDocument;
using PdfPage = PdfSharp.Pdf.PdfPage;
using Image = System.Drawing.Image;

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
        //String som alltid holder styr på hvem test som er valgt i datatabellen
        String selectedTest;
        //String med lokasjonen til databasen
        String dbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\forsvaret.mdf;Integrated Security=True";

        //Lager en kobling til databasen og fyller datatabellen med innhold
        //Dersom det sendes med en string fylles tabellen bare med radene som inneholder denne stringen
        private void FillDataGrid(string search = "") 
        {
            String filterText = comboFilter.Text;

            switch (filterText)
            {
                case @"Alle felter":
                    filterText = "";
                    break;
                case @"Test ID":
                    filterText = "testid";
                    break;
                case @"RegNr":
                    filterText = "tests.regNr";
                    break;
                case @"Bruker":
                    filterText = "UserName";
                    break;
                default:
                    Console.WriteLine("Nothing");
                    break;
            }


            string query;
            if (!(search.TrimStart() == string.Empty))
            {
                if(filterText == "")
                {
                    query = "SELECT * FROM tests INNER JOIN Vehicles on tests.regNr = Vehicles.regNr INNER JOIN USERS on tests.userID = USERS.userId WHERE testid LIKE '%" + search + "%' or tests.userId LIKE '%" + search + "%' or tests.regNr LIKE '%" + search + "%' or rolloverAngle LIKE '%" + search + "%' or Equipment LIKE '%" + search + "%' or weight LIKE '%" + search + "%' or error LIKE '%" + search + "%'";
                    //query += " INNER JOIN USERS on tests.userID = USERS.userId";
                }
                else
                {
                    query = "SELECT * FROM tests INNER JOIN Vehicles on tests.regNr = Vehicles.regNr INNER JOIN USERS on tests.userID = USERS.userId WHERE " + filterText + " LIKE '%" + search + "%'";
                }
            }
            else
            {
                query = "SELECT * FROM Tests INNER JOIN Vehicles on tests.regNr = Vehicles.regNr INNER JOIN USERS on tests.userID = USERS.userId";
            }

            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                //Henter data fra test databasen
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                //Fyller datagridden med dataen
                DataTable dt = new DataTable("Test");
                sda.Fill(dt);
                grdTests.ItemsSource = dt.DefaultView;
            }
        }
        
        //Sletter den testen i datatabellen som er valgt fra databasen
        private void Delete_Button_Click(object sender, RoutedEventArgs e) 
        {
            //Sjekker at en test er valgt
            if (selectedTest != null)
            {
                //Åpner en meldingsboks for å bekrefte sletting av test
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil slette testen med ID " + selectedTest + "?", "Slett test", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SqlConnection conn = new SqlConnection(dbConnectionString);
                        try
                        {
                            //Sletter den testen med ID som er lik den valgte testen fra databasen
                            conn.Open();
                            string query = "DELETE FROM Tests WHERE testID='" + selectedTest + "'";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            FillDataGrid(searchTxt.Text);
                            conn.Close();
                        }
                        //Dersom testen ikke kunne slettes vises en meldingsboks med feilen
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                        //Hvis brukeren sier nei til å slette testen skjer det ikke noe
                    case MessageBoxResult.No:
                        break;
                }
            }
            //Meldingsboks som kommer opp dersom ingen test er valgt
            else
            {
                MessageBox.Show("Ingen test valgt");
            }
        }

        //Et void som blir kjørt hver gang det valgte elementet i datatabellen endres
        private void grdTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Setter den til null som standard
            selectedTest = null;
            //Prøver å finne testID til det nye valget
            try
            {
                DataRowView drv = (DataRowView)grdTests.SelectedItem;

                if (drv != null)
                {
                    selectedTest = drv["testID"].ToString();
                    //Console.WriteLine(selectedTest);
                }
            }
            //Dersom testID til den valgte testen ikke kan hentes forblir den valgte testen null
            catch(Exception er)
            {
                Console.WriteLine(er);
            }
        }

        //Knappen som eksporterer den valgte testen til en excel fil
        private void Export_Button_Click(object sender, RoutedEventArgs e)
        {
            //Sjekker at en test er valgt
            if (selectedTest != null)
            {
                //Gjør klart til å redigere excel filer
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = app.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Worksheets[1];

                //Starter kobling til databasen
                SqlConnection conn = new SqlConnection(dbConnectionString);
                conn.Open();

                //Henter all dataen fra testen i databasen med samme ID som den valgte testen fra datatabellen
                string query = "SELECT * FROM tests WHERE testid=" + int.Parse(selectedTest);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sdr = cmd.ExecuteReader();

                //Lager en liste med alle radnavnene til testene som skal lagres
                List<string> columnNameList = new List<string> { "testID", "userId", "regNr", "rolloverAngle", "Equipment", "weight", "timeRan", "centerofgravityx", "centerofgravityy", "centerofgravityz", "error" };
                //Lager en tom liste til all testdataen 
                List<string> testDataList = new List<string> { };


                //Gjør noe dersom SQL queryen ga noen resultater i databasen
                if (sdr.Read())
                {
                    //Gir arket i excel filen et navn
                    worksheet.Name = "TestData";
                  
                    //Legger til dataen fra listene med radnavn og testdata i de to første kolonnene
                    //Loopen kjører like mange ganger som det er dataelementer i listen
                    for (int column = 0; column < columnNameList.Count; column++)
                    {
                        testDataList.Add(sdr[columnNameList[column]].ToString());
                        worksheet.Cells[1, column + 1].value = columnNameList[column];
                        worksheet.Cells[2, column + 1].value = testDataList[column];
                    }
                }

                //Initialiserer en dialogboks der man kan lagre filer
                //Dialogboksen gir filen et standard navn utifra testID
                //Lagres som excel fil som standard
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Test_ID_" + selectedTest; // Default file name
                dlg.DefaultExt = ".xlsx"; // Default file extension
                dlg.Filter = "Excel regneark (.xlsx)|*.xlsx"; // Filter files by extension
                Nullable<bool> result = dlg.ShowDialog();

                //Sjekker at dialogboksen ble åpnet
                if (result == true)
                {
                    //Lagrer lokasjonen filen skal lagres under til en string
                    string filename = dlg.FileName;

                    conn.Close();
                    //Lagrer dokumentet på stedet som ble valgt av brukeren i dialogboksen
                    workbook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //Viser hvor dokumentet ble lagret og lukker excel
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

        //Knapp som lar brukeren velge en fil som skal importeres til test databasen
        private void Import_Button_Click(object sender, RoutedEventArgs e)
        {
            //Initialiserer en dialogboks der man kan velge en fil
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            //Filtrerer ut alt annet enn excel filer som standard
            ofd.Title = "Import excel sheet";
            ofd.Filter = "EXCEL sheets|*.xlsx";
            ofd.InitialDirectory = @"C:\";

            //Vier dialogboksen
            Nullable<bool> result = ofd.ShowDialog();

            //Lagrer lokasjonen til den valgte filen i en string
            string openFile = ofd.FileName;

            //Initialiserer excel 
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(openFile);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            int colCount = 11;
            //Lager en tom liste der dataen fra excel skal inn
            List<string> testDataList = new List<string> { };

            for (int j = 1; j <= colCount; j++)
            {
                //Tar all dataen fra den andre kolonnen og legger det til listen med data i riktig ekkefølge
                if (xlRange.Cells[2, j] != null && xlRange.Cells[2, j].Value2 != null)
                {
                    testDataList.Add(xlRange.Cells[2, j].Value2.ToString());
                }
            }

            //Avslutter excel
            xlWorkbook.Close();
            xlApp.Quit();
            
            SqlConnection conn = new SqlConnection(dbConnectionString);
            //Prøver å sette inn dataen fra listen i databasen
            try
            {
                conn.Open();
                //Setter inn all dataen fra listen inn i databsen på riktig plass
                string query = "INSERT INTO tests (testID, userId, regNr, rolloverAngle, Equipment, weight, centerofgravityx, centerofgravityy, centerofgravityz, error) values('" + testDataList[0] + "','" + testDataList[1] + "','" + testDataList[2] + "','" + testDataList[3] + "','" + testDataList[4] + "','" + testDataList[5] + "','"  + testDataList[7] + "','" + testDataList[8] + "','" + testDataList[9] + "','" + testDataList[10] + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                FillDataGrid();
                conn.Close();
                MessageBox.Show("Den valgte filen er importert til databasen");
            }
            //Viser en feilmelding dersom dataen ikke kan settes inn i databasen
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Report_Button_Click(object sender, RoutedEventArgs e)
        {    
            //Starter kobling til databasen
            SqlConnection conn = new SqlConnection(dbConnectionString);
            conn.Open();

            //Henter all dataen fra testen i databasen med samme ID som den valgte testen fra datatabellen
            string query = "SELECT * FROM tests INNER JOIN Vehicles on tests.regNr = Vehicles.regNr INNER JOIN USERS on tests.userID = USERS.userId WHERE testid=" + int.Parse(selectedTest);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader sdr = cmd.ExecuteReader();

            //Lager en liste med alle radnavnene til testene som skal lagres
            List<string> columnNameList = new List<string> { "testID", "userId", "regNr", "rolloverAngle", "Equipment", "weight", "timeRan", "centerofgravityx", "centerofgravityy", "centerofgravityz", "error", "UserName", "vehicleName" };
            //Lager en tom liste til all testdataen 
            List<string> testDataList = new List<string> { };


            //Gjør noe dersom SQL queryen ga noen resultater i databasen
            if (sdr.Read())
            {
                //Legger til dataen fra listene med radnavn og testdata i de to første kolonnene
                //Loopen kjører like mange ganger som det er dataelementer i listen
                for (int column = 0; column < columnNameList.Count; column++)
                {
                    testDataList.Add(sdr[columnNameList[column]].ToString());
                }
            }
            conn.Close();


            string testID = testDataList[0];
            string userId = testDataList[1];
            string regNr = testDataList[2];
            string rolloverAngle = testDataList[3];
            string Equipment = testDataList[4];
            string weight = testDataList[5];
            string timeRan = testDataList[6];
            string centerofgravityx = testDataList[7];
            string centerofgravityy = testDataList[8];
            string centerofgravityz = testDataList[9];
            string error = testDataList[10];
            string username = testDataList[11];
            string vehicleName = testDataList[12];


            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "My First PDF";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
            //Øverst til venstre
            graph.DrawString("Bruker: ID - " + userId + ", Navn - " + username, font, XBrushes.Black, new XRect(5, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Kjøretøy: Reg nr - " + regNr + ", Navn - " + vehicleName, font, XBrushes.Black, new XRect(5, 15, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Utstyr: " + Equipment, font, XBrushes.Black, new XRect(5, 30, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Vekt: " + weight, font, XBrushes.Black, new XRect(5, 45, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Massesentrum: X: " + centerofgravityx + " Y: " + centerofgravityy + " Z: " + centerofgravityz, font, XBrushes.Black, new XRect(5, 60, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Error: " + error, font, XBrushes.Black, new XRect(5, 75, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

            /* need to insert a new string, without bjorbor
            // Draw image
            String filepath = @"C:\Users\bjobo\source\repos\BachelorProjectGITHUB\VeiebryggeApplication\Images\forsvaretlogo.png";
            MemoryStream strm = new MemoryStream();
            Image img = Image.FromStream(File.OpenRead(filepath));
            img.Save(strm, System.Drawing.Imaging.ImageFormat.Png);

            XImage xfoto = XImage.FromStream(strm);
            graph.DrawImage(xfoto, 400, 10, 180, 100);
            */

            //Øverst til høyre
            graph.DrawString("testID:" + testID, font, XBrushes.Black, new XRect(-5, 150, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopRight);
            graph.DrawString("Dato:" + timeRan, font, XBrushes.Black, new XRect(-5, 165, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopRight);
            string pdfFilename = "firstpage.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);
        }

        private void searchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillDataGrid(searchTxt.Text);
        }


        private void comboFilter_DropDownClosed(object sender, EventArgs e)
        {
            FillDataGrid(searchTxt.Text);
        }
    }
}
