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
        public int Width { get; protected set; }
        public int Height { get; protected set; }
    }

    /// <summary>
    /// Fake holder. This version of ClosedXML does not support imaging
    /// </summary>
    [Janitor.SkipWeaving]
    public class Bitmap : Image, IDisposable
    {
        private readonly byte[] _imageData;

        public Bitmap(MemoryStream imageStream)
        {
            _imageData = imageStream?.ToArray();

            // TODO: read enough header data from a few formats to get this
            Width = 157;
            Height = 101;
        }

        public Bitmap(int width, int height)
        {
            Width = width;
            Height = height;
        }


        public void Dispose() { }

        public void Save(MemoryStream target, ImageFormat imageFormat)
        {
            if (target != null && _imageData != null) target.Write(_imageData, 0, _imageData.Length);
        }

        public static Bitmap FromStream(Stream resourceStream)
        {
            using var ms = new MemoryStream();
            resourceStream?.CopyTo(ms);
            return new Bitmap(ms);
        }
    }
}
