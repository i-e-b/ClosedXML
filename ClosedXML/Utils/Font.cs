using System;
using System.Linq;
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
            var fudgeFactor = 0.85f;
            var guess = s!.Select(CharSize).Sum() * font!.Size * fudgeFactor;
            return new SizeF(guess, font.Size * 1.2f);
        }

        public float DpiX => 96;
        public float DpiY => 96;

        private static float CharSize(char c)
        {
            return c >= _charWidths!.Length ? 0.8f : _charWidths[c];
        }

        /// <summary> Approximate widths of characters in Calibri Regular </summary>
        private static readonly float[] _charWidths= {
            1.1f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.1f, 1.0f, 1.0f, 1.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, /*   */ 1.089f,
            /* ! */ 0.205f, /* " */ 0.515f, /* # */ 0.925f, /* $ */ 0.816f, /* % */ 1.289f, /* & */ 1.280f,
            /* ' */ 0.170f, /* ( */ 0.357f, /* ) */ 0.356f, /* * */ 0.681f, /* + */ 0.859f, /* , */ 0.321f,
            /* - */ 0.478f, /* . */ 0.210f, /* / */ 0.759f, /* 0 */ 0.874f, /* 1 */ 0.735f, /* 2 */ 0.791f,
            /* 3 */ 0.792f, /* 4 */ 0.909f, /* 5 */ 0.798f, /* 6 */ 0.830f, /* 7 */ 0.824f, /* 8 */ 0.852f,
            /* 9 */ 0.832f, /* : */ 0.205f, /* ; */ 0.317f, /* < */ 0.830f, /* = */ 0.812f, /* > */ 0.831f,
            /* ? */ 0.705f, /* @ */ 1.617f, /* A */ 1.089f, /* B */ 0.833f, /* C */ 0.917f, /* D */ 0.972f,
            /* E */ 0.703f, /* F */ 0.664f, /* G */ 1.028f, /* H */ 0.910f, /* I */ 0.168f, /* J */ 0.466f,
            /* K */ 0.824f, /* L */ 0.655f, /* M */ 1.374f, /* N */ 0.955f, /* O */ 1.130f, /* P */ 0.776f,
            /* Q */ 1.285f, /* R */ 0.843f, /* S */ 0.777f, /* T */ 0.945f, /* U */ 0.952f, /* V */ 1.068f,
            /* W */ 1.659f, /* X */ 0.943f, /* Y */ 0.915f, /* Z */ 0.837f, /* [ */ 0.340f, /* \ */ 0.760f,
            /* ] */ 0.340f, /* ^ */ 0.785f, /* _ */ 1.007f, /* ` */ 0.455f, /* a */ 0.722f, /* b */ 0.809f,
            /* c */ 0.693f, /* d */ 0.809f, /* e */ 0.809f, /* f */ 0.595f, /* g */ 0.832f, /* h */ 0.757f,
            /* i */ 0.199f, /* j */ 0.390f, /* k */ 0.722f, /* l */ 0.161f, /* m */ 1.304f, /* n */ 0.757f,
            /* o */ 0.879f, /* p */ 0.809f, /* q */ 0.809f, /* r */ 0.514f, /* s */ 0.622f, /* t */ 0.574f,
            /* u */ 0.758f, /* v */ 0.837f, /* w */ 1.327f, /* x */ 0.783f, /* y */ 0.838f, /* z */ 0.623f,
            /* { */ 0.482f, /* | */ 0.151f, /* } */ 0.484f, /* ~ */ 0.881f, 1.0f
        };
    }
}
