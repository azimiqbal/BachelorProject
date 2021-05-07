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
using System.Drawing;
using System.Drawing.Imaging;
using Point = System.Drawing.Point;

namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for imageProcessing.xaml
    /// </summary>
    public partial class imageProcessing : System.Windows.Controls.Page
    {
        
        enum PropertyID
        {
            PropertyTagDateTime = 0x0132,
            PropertyTagExifDTOrig = 0x9003,
            PropertyTagEquipMake = 0x010F,
            PropertyTagEquipModel = 0x0110,
        }
        
        public imageProcessing()
        {
            InitializeComponent();
        }


        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            txtImageInfo.Clear();
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Title = "Select a picture";
            ofd.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (ofd.ShowDialog() == true)
            {
                pbImage.Source = new BitmapImage(new Uri(ofd.FileName));
                System.Drawing.Image img = System.Drawing.Image.FromFile(ofd.FileName);
                string imageType = "";

                
                // Create a Bitmap object from an image file.
                Bitmap myBitmap = new Bitmap(img);
                // Get the color of a pixel within myBitmap.
                System.Drawing.Color pixelColor = myBitmap.GetPixel(50, 50);



                if (ImageFormat.Jpeg.Equals(img.RawFormat))
                {
                    imageType = "JPEG Image";
                    GetPropItems(img, "Date and Time Taken: ", (int)PropertyID.PropertyTagDateTime);
                    GetPropItems(img, "\r\nCamera Maker: ", (int)PropertyID.PropertyTagEquipMake);
                    GetPropItems(img, "\r\nCamera Model: ", (int)PropertyID.PropertyTagEquipModel);
                    
                }
                else if (ImageFormat.Png.Equals(img.RawFormat))
                {
                    imageType = "PNG Image";
                    GetPropItems(img, "Date and Time Taken: ", (int)PropertyID.PropertyTagDateTime);
                    GetPropItems(img, "\r\nCamera Maker: ", (int)PropertyID.PropertyTagEquipMake);
                    GetPropItems(img, "\r\nCamera Model: ", (int)PropertyID.PropertyTagEquipModel);
                }

                string imageWidth = img.Width.ToString();
                string imageHeight = img.Height.ToString();
                string imageResolution = img.HorizontalResolution.ToString();
                string imagePixelDepth = System.Drawing.Image.GetPixelFormatSize(img.PixelFormat).ToString();
                
                txtImageInfo.Text += imageType + "\r\n";
                txtImageInfo.Text += "Width = " + imageWidth + "\r\n";
                txtImageInfo.Text += "Height = " + imageHeight + "\r\n";
                txtImageInfo.Text += "Resolution = " + imageResolution + "\r\n";
                txtImageInfo.Text += "Pixel depth = " + imagePixelDepth + "\r\n";
                txtImageInfo.Text += "Pixel color = " + pixelColor +"\r\n";
            }

        }

        private void GetPropItems(System.Drawing.Image img, string message, int ID)
        {
            try
            {
                PropertyItem propItem = img.GetPropertyItem(ID);
                if(propItem != null)
                {
                    ASCIIEncoding encod = new ASCIIEncoding();
                    string asciiInfo = encod.GetString(propItem.Value, 0, propItem.Len);
                    txtImageInfo.Text += message + asciiInfo;
                }
            }
            catch (Exception)
            {
                txtImageInfo.Text += message + "Not Available";
            }
        }

    }
}
