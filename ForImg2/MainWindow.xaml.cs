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
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Button1_Click(object sender, RoutedEventArgs e)
        {
            ImageContext ImageIn;
            BitmapSource ImageOut;
            ImageIn = MakeBitmap();
            ImageOut = MakeGray(ImageIn);
            SaveImage(ImageOut);
        }

        public ImageContext MakeBitmap()
        {
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@text1.Text);
            myBitmapImage.EndInit();
            myImage.Source = myBitmapImage;
            ImageContext inSource = new ImageContext(myBitmapImage.PixelHeight, myBitmapImage.PixelWidth, myBitmapImage.Format);
            myBitmapImage.CopyPixels(inSource.PixelByteArray, inSource.NStride, 0);
            return inSource;
        }

        public BitmapSource MakeGray(ImageContext inSource)
        {
            ImageContext outSource = inSource;
            byte middle;
            for (int i = 0; i < inSource.PixelByteArraySize; i += 4)
            {
                middle = (byte)((inSource.PixelByteArray[i + 1] + inSource.PixelByteArray[i + 2] + inSource.PixelByteArray[i + 3]) / 3);
                outSource.PixelByteArray[i] = outSource.PixelByteArray[i + 1] = outSource.PixelByteArray[i + 2] = middle;
                inSource.PixelByteArray[i + 3] = 0;
            }
            var bitmap = BitmapSource.Create(outSource.Width, outSource.Height, 96d, 96d, PixelFormats.Cmyk32, null, outSource.PixelByteArray, outSource.NStride);
            myImage.Source = bitmap;

            return bitmap;
        }


        public void SaveImage(BitmapSource bitmap)
        {
            try
            {
                var enc = new JpegBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmap));
                using (FileStream file = File.OpenWrite("newgray.jpg"))
                    enc.Save(file);
            }
            catch
            {
                text1.Text = "Не верно введен путь";
            }
        }

    }
}

