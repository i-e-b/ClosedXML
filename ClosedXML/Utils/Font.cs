using System;
using ClosedXML.Utils;

namespace ClosedXML.Utils
{
    /// <summary>
    /// Wrapper for System.Drawing.Font to keep naming clear.
    /// </summary>
    public class GdiFont : System.Drawing.Font
    {
        public GdiFont(string name, float size, FontStyle style)
        {
            throw new NotImplementedException();
        }

        public GdiFont()
        {
            throw new NotImplementedException();
        }
    }

    [Flags]
    public enum FontStyle
    {
        Regular = 0,

        Bold = 1<<0,
        Italic = 1<<1,
        Strikeout = 1<<2,
        Underline = 1<<3
    }
}

namespace System.Drawing
{
    /// <summary>
    /// Fake holder. This version of ClosedXML does not support imaging
    /// </summary>
    public class Font
    {
    }

    /// <summary>
    /// Fake holder. This version of ClosedXML does not support imaging
    /// </summary>
    public class StringFormat
    {
        public static StringFormat GenericTypographic { get; set; }
    }

    /// <summary>
    /// Fake holder. This version of ClosedXML does not support imaging
    /// </summary>
    public class Graphics
    {
        public Graphics FromImage(Bitmap image)
        {
            throw new NotImplementedException();
        }

        public SizeF MeasureString(string s, GdiFont font, int maxValue, StringFormat defaultStringFormat)
        {
            throw new NotImplementedException();
        }

        public float DpiX => 96;
        public float DpiY => 96;
    }
}
