using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
namespace WPFilmweb.Model
{
    public class Tools
    {
        public static ImageSource ByteArrToImageSrc(byte[] arr)
        {
            if(arr != null)
            {
                BitmapImage temp = new BitmapImage();
                MemoryStream ms = new MemoryStream(arr);
                ImageSource result = null;
                if (ms.Length > 0)
                {
                    temp.BeginInit();
                    temp.StreamSource = ms;
                    temp.EndInit();
                    result = temp as ImageSource;
                }
                return result;
            }
            return null;
        }
    }
}
