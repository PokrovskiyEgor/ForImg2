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
using System.IO;
using System.Drawing;

namespace ForImg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public byte[] pixelByteArrayIn;
        public byte[] pixelByteArrayOut;
        public int pixelByteArraySize;
        public int height;
        public int width;
        public int nStride;
        public PixelFormat imageFormat;

        public MainWindow()
        {
            InitializeComponent();
            //

        }
        public void Button1_Click(object sender, RoutedEventArgs e)
        {
            
            MakeBitmap();
            MakeGray();
            MakeImage();
        }

        public void InitSettings()
        {
        }


        public void MakeBitmap()
        {
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@text1.Text);
            myBitmapImage.EndInit();
            myImage.Source = myBitmapImage;
            //
            height = myBitmapImage.PixelHeight;
            width = myBitmapImage.PixelWidth;
            imageFormat = myBitmapImage.Format;
            nStride = (myBitmapImage.PixelWidth * imageFormat.BitsPerPixel + 7) / 8;

            pixelByteArraySize = myBitmapImage.PixelHeight * nStride;
            pixelByteArrayIn = new byte[pixelByteArraySize];
            myBitmapImage.CopyPixels(pixelByteArrayIn, nStride, 0);
        }

        public void MakeGray()
        {
            byte middle;
            pixelByteArrayOut= new byte[pixelByteArraySize];
            for (int i = 0; i < pixelByteArraySize; i += 4)
            {
                middle = (byte)((pixelByteArrayIn[i + 1] + pixelByteArrayIn[i + 2] + pixelByteArrayIn[i + 3])/3);
                pixelByteArrayOut[i] = pixelByteArrayOut[i + 1] = pixelByteArrayOut[i + 2] = middle;
            }
        }

        public void MakeImage()
        {
            var bitmap = BitmapSource.Create(width, height, 96d, 96d, imageFormat, null, pixelByteArrayOut, nStride);
            myImage.Source = bitmap;
        }
    }
}

