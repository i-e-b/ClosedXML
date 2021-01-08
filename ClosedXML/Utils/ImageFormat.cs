using System;
// ReSharper disable UnusedMember.Global

namespace ClosedXML.Utils
{
    /// <summary>
    /// Fake holder. This version of ClosedXML does not support imaging
    /// </summary>
    public class ImageFormat
    {
        /*
            yield return new ImageCodecInfo{MimeType= "image/bmp", FormatID = new Guid("1c50f0b7-ba45-43f3-92e6-b231c762f670")};
            yield return new ImageCodecInfo{MimeType= "image/png", FormatID = new Guid("1c50f0b7-ba45-43f3-92e6-b231c762f671")};
            yield return new ImageCodecInfo{MimeType= "image/jpeg", FormatID = new Guid("1c50f0b7-ba45-43f3-92e6-b231c762f672")};
            */
        /*
        Bmp = 0,
        Gif = 1,
        Png = 2,
        Tiff = 3,
        Icon = 4,
        Pcx = 5,
        Jpeg = 6*/

        public ImageFormat Bmp  => new ImageFormat{Guid = new Guid("1c50f0b7-ba45-43f3-92e6-b231c762f670")};
        public ImageFormat Png  => new ImageFormat{Guid = new Guid("1c50f0b7-ba45-43f3-92e6-b231c762f671")};
        public ImageFormat Jpeg => new ImageFormat{Guid = new Guid("1c50f0b7-ba45-43f3-92e6-b231c762f672")};

        public Guid Guid { get; set; }
    }
}
