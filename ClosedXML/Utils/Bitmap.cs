using System;
using System.IO;
using System.Linq;

namespace ClosedXML.Utils
{
    /// <summary>
    /// Fake holder. This version of ClosedXML does not support imaging
    /// </summary>
    [Janitor.SkipWeaving]
    public class Image
    {
        public ImageFormat RawFormat => new ImageFormat {Guid = ImageCodecInfo.GetImageDecoders()!.First()!.FormatID};
        public int Width { get; set; }
        public int Height { get; set; }
    }

    /// <summary>
    /// Fake holder. This version of ClosedXML does not support imaging
    /// </summary>
    [Janitor.SkipWeaving]
    public class Bitmap : Image, IDisposable
    {

        public Bitmap(MemoryStream imageStream)
        {
            Console.WriteLine("### Bitmap.ctor");
            Width = 1;
            Height = 1;
            //throw new NotImplementedException("Imaging is disabled in this build of ClosedXML: Bitmap.ctor");
        }

        public Bitmap(int width, int height)
        {
            Width = width;
            Height = height;
        }


        public void Dispose() { }

        public void Save(MemoryStream target, ImageFormat imageFormat)
        {
            Console.WriteLine("### Bitmap.Save");
            //throw new NotImplementedException("Imaging is disabled in this build of ClosedXML: Bitmap.Save");
        }

        public static Bitmap FromStream(Stream resourceStream)
        {
            Console.WriteLine("### Bitmap.FromStream");
            //throw new NotImplementedException("Imaging is disabled in this build of ClosedXML: Bitmap.FromStream");
            return new Bitmap(1,1);
        }
    }
}
