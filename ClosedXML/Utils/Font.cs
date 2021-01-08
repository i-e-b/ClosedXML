using System;
using ClosedXML.Utils;

namespace ClosedXML.Utils
{
    /// <summary>
    /// Wrapper for System.Drawing.Font to keep naming clear.
    /// </summary>
    [Janitor.SkipWeaving]
    public class GdiFont : System.Drawing.Font, IDisposable
    {
        private readonly string _name;
        private readonly FontStyle _style;

        public GdiFont(string name, float size, FontStyle style)
        {
            _name = name;
            Size = size;
            _style = style;
        }

        public float Size { get; set; }

        public void Dispose() { }
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
        public static Graphics FromImage(Bitmap image)
        {
            return new Graphics();
        }

        public SizeF MeasureString(string s, GdiFont font, int maxValue, StringFormat defaultStringFormat)
        {
            var guess = s.Length * font.Size * 0.582454321976516f;
            return new SizeF(guess, font.Size * 1.2f);
        }

        public float DpiX => 96;
        public float DpiY => 96;
    }
}
