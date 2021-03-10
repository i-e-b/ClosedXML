using System;
using System.Collections.Generic;
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

            try
            {
                // read enough header data from a few formats to get this
                var size = ImageHeaders.GetDimensions(_imageData);
                Width = size.Width;
                Height = size.Height;
            }
            catch
            {
                Width = 64;
                Height = 64;
            }
        }

        public Bitmap(int width, int height)
        {
            Width = width;
            Height = height;
        }


        public void Dispose()
        {
        }

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

    /// <summary>
    /// Reads image headers to extract basic information without decoding the entire file
    /// </summary>
    /// <remarks>Based on https://stackoverflow.com/a/112711</remarks>
    [Janitor.SkipWeaving]
    public static class ImageHeaders
    {
        private const string ErrorMessage = "Did not recognize image format";

        private static readonly Dictionary<byte[], Func<BinaryReader, Size2D>> _imageFormatDecoders = new Dictionary<byte[], Func<BinaryReader, Size2D>>()
        {
            {new byte[] {0x42, 0x4D}, DecodeBitmap},
            {new byte[] {0x47, 0x49, 0x46, 0x38, 0x37, 0x61}, DecodeGif},
            {new byte[] {0x47, 0x49, 0x46, 0x38, 0x39, 0x61}, DecodeGif},
            {new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A}, DecodePng},
            {new byte[] {0xff, 0xd8}, DecodeJfif},
        };

        /// <summary>
        /// Gets the dimensions of an image.
        /// </summary>
        /// <param name="data">Raw image image data</param>
        /// <returns>The dimensions of the specified image.</returns>
        /// <exception cref="ArgumentException">The image was of an unrecognized format.</exception>
        public static Size2D GetDimensions(byte[] data)
        {
            if (data == null) throw new ArgumentException(ErrorMessage, nameof(data));
            using var ms = new MemoryStream(data);
            ms.Seek(0, SeekOrigin.Begin);
            using var binaryReader = new BinaryReader(ms);
            try
            {
                return GetDimensions(binaryReader);
            }
            catch (ArgumentException e)
            {
                if (e.Message?.StartsWith(ErrorMessage) == true) throw new ArgumentException(ErrorMessage, nameof(data), e);
                throw;
            }
        }

        /// <summary>
        /// Gets the dimensions of an image.
        /// </summary>
        /// <param name="binaryReader">The raw image data</param>
        /// <returns>The dimensions of the specified image.</returns>
        /// <exception cref="ArgumentException">The image was of an unrecognized format.</exception>
        public static Size2D GetDimensions(BinaryReader binaryReader)
        {
            if (binaryReader == null) throw new ArgumentException(ErrorMessage, nameof(binaryReader));
            var maxMagicBytesLength = _imageFormatDecoders?.Keys.OrderByDescending(x => x!.Length).FirstOrDefault()?.Length;
            if (maxMagicBytesLength == null) throw new ArgumentException(ErrorMessage, nameof(binaryReader));

            var len = maxMagicBytesLength.Value;
            var magicBytes = new byte[len];

            for (var i = 0; i < len; i += 1)
            {
                magicBytes[i] = binaryReader.ReadByte();

                foreach (var kvPair in _imageFormatDecoders.Where(kvPair => magicBytes.StartsWith(kvPair.Key)))
                {
                    return kvPair.Value!(binaryReader);
                }
            }

            throw new ArgumentException(ErrorMessage, nameof(binaryReader));
        }

        private static bool StartsWith(this IReadOnlyList<byte> thisBytes, IReadOnlyList<byte> thatBytes)
        {
            if (thisBytes == null) return false;
            if (thatBytes == null) return false;
            for (var i = 0; i < thatBytes.Count; i += 1)
            {
                if (thisBytes[i] != thatBytes[i]) return false;
            }

            return true;
        }

        private static short ReadLittleEndianInt16(this BinaryReader binaryReader)
        {
            var bytes = new byte[sizeof(short)];
            for (var i = 0; i < sizeof(short); i += 1)
            {
                bytes[sizeof(short) - 1 - i] = binaryReader!.ReadByte();
            }

            return BitConverter.ToInt16(bytes, 0);
        }

        private static int ReadLittleEndianInt32(this BinaryReader binaryReader)
        {
            var bytes = new byte[sizeof(int)];
            for (var i = 0; i < sizeof(int); i += 1)
            {
                bytes[sizeof(int) - 1 - i] = binaryReader!.ReadByte();
            }

            return BitConverter.ToInt32(bytes, 0);
        }

        private static Size2D DecodeBitmap(BinaryReader binaryReader)
        {
            binaryReader!.ReadBytes(16);
            var width = binaryReader.ReadInt32();
            var height = binaryReader.ReadInt32();
            return new Size2D(width, height);
        }

        private static Size2D DecodeGif(BinaryReader binaryReader)
        {
            int width = binaryReader!.ReadInt16();
            int height = binaryReader.ReadInt16();
            return new Size2D(width, height);
        }

        private static Size2D DecodePng(BinaryReader binaryReader)
        {
            binaryReader!.ReadBytes(8);
            var width = binaryReader.ReadLittleEndianInt32();
            var height = binaryReader.ReadLittleEndianInt32();
            return new Size2D(width, height);
        }

        private static Size2D DecodeJfif(BinaryReader binaryReader)
        {
            while (binaryReader!.ReadByte() == 0xff)
            {
                var marker = binaryReader.ReadByte();
                var chunkLength = binaryReader.ReadLittleEndianInt16();

                if (marker == 0xc0)
                {
                    binaryReader.ReadByte();

                    int height = binaryReader.ReadLittleEndianInt16();
                    int width = binaryReader.ReadLittleEndianInt16();
                    return new Size2D(width, height);
                }

                binaryReader.ReadBytes(chunkLength - 2);
            }

            throw new ArgumentException(ErrorMessage);
        }
    }

    public readonly struct Size2D
    {
        public int Width { get; }
        public int Height { get; }

        public Size2D(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
