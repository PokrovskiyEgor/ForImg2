using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media; 

namespace ForImg
{
    public class ImageContext
    {
        public byte[] PixelByteArray;
        public int PixelByteArraySize;
        public int Height;
        public int Width;
        public int NStride;
        public PixelFormat ImageFormat;

        public ImageContext(int h, int w, PixelFormat f)
        {
            Height = h;
            Width = w;
            ImageFormat = f;
            NStride = (Width * ImageFormat.BitsPerPixel + 7) / 8;
            PixelByteArraySize = Height * NStride;
            PixelByteArray = new byte[PixelByteArraySize];
        }

    }
}
