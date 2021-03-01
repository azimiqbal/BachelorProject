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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

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
            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Draw the text
                graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));
                //Save the document
                document.Save("fileName.pdf");
            }
        }
    }
}



/*
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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;

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
            using (PdfSharp.Pdf.PdfDocument document = new PdfSharp.Pdf.PdfDocument())
            {
                //Add a page to the document
                PdfSharp.Pdf.PdfPage page = document.Pages.Add();

                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Draw the text
                //graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

                //Save the document
                document.Save("Output.pdf");
            }
        }
    }
}
 
*/