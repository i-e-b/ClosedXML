using System;
using System.Collections.Generic;
using System.IO;

namespace ClosedXML.Utils
{

    /// <summary>
    /// Fake holder. This version of ClosedXML does not support imaging
    /// </summary>
    public class ImageCodecInfo
    {
        public Guid FormatID { get; set; }

        public static IEnumerable<ImageCodecInfo> GetImageDecoders()
        {
            yield break;
        }

        public string MimeType { get; set; }
    }

    /// <summary>
    /// Fake holder. This version of ClosedXML does not support imaging
    /// </summary>
    [Janitor.SkipWeaving]
    public class Image
    {
        public ImageFormat RawFormat { get; set; }
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
        }

        public Bitmap(int width, int height)
        {
            throw new NotImplementedException();
        }


        public void Dispose() { }

        public void Save(MemoryStream target, ImageFormat imageFormat)
        {
            throw new NotImplementedException();
        }

        public static Bitmap FromStream(Stream resourceStream)
        {
            throw new NotImplementedException();
        }
    }
}
