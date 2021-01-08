using System;
using System.Collections.Generic;

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
            yield return new ImageCodecInfo{MimeType= "image/bmp", FormatID = new Guid("1c50f0b7-ba45-43f3-92e6-b231c762f670")};
            yield return new ImageCodecInfo{MimeType= "image/png", FormatID = new Guid("1c50f0b7-ba45-43f3-92e6-b231c762f671")};
            yield return new ImageCodecInfo{MimeType= "image/jpeg", FormatID = new Guid("1c50f0b7-ba45-43f3-92e6-b231c762f672")};
        }

        public string MimeType { get; set; }
    }
}
