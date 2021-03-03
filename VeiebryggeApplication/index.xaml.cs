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
using System.Diagnostics;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Data;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfDocument = PdfSharp.Pdf.PdfDocument;
using PdfPage = PdfSharp.Pdf.PdfPage;


namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for index.xaml
    /// </summary>
    public partial class index : Page
    {
        public index()
        {
            InitializeComponent();
        }

        private void BtnMenu1(object sender, RoutedEventArgs e)
        {
            Main.Content = new RunTest();
        }
        private void BtnMenu2(object sender, RoutedEventArgs e)
        {
            Main.Content = new Tests();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMenu3(object sender, RoutedEventArgs e)
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "My First PDF";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
            graph.DrawString("Første linje", font, XBrushes.Black, new XRect(5, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Andre linje", font, XBrushes.Black, new XRect(5, 15, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Tredje linje", font, XBrushes.Black, new XRect(5, 30, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Kjøretøytest", font, XBrushes.Black, new XRect(-5, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopRight);
            graph.DrawString("Dato: 03.03.2021", font, XBrushes.Black, new XRect(-5, 15, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopRight);
            string pdfFilename = "firstpage.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);
        }
    }
}