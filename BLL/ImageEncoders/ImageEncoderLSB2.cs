using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BLL
{
    public class ImageEncoderLSB2 : IEncoder<Bitmap>
    {
        public Bitmap Embed(Bitmap input, byte[] bytes, string key = null)
        {
            var bmp = new Bitmap(input.Width, input.Height);
            int textIndex = 0;
            for (int i = 0; i < input.Height; i++)
            {
                for (int j = 0; j < input.Width; j++)
                {
                    var color = input.GetPixel(j, i);
                    if (textIndex < bytes.Length)
                    {
                        var later = (byte)bytes[textIndex++];

                        var R1 = (byte)(later >> 6);
                        var G1 = (byte)((byte)(later << 2) >> 6);
                        var B1 = (byte)((byte)(later << 4) >> 6);
                        var A1 = (byte)((byte)(later << 6) >> 6);

                        byte R = (byte)((color.R >> 2 << 2) | R1);
                        byte G = (byte)((color.G >> 2 << 2) | G1);
                        byte B = (byte)((color.B >> 2 << 2) | B1);
                        byte A = (byte)((color.A >> 2 << 2) | A1);

                        var resultColor = Color.FromArgb(A, R, G, B);

                        bmp.SetPixel(j, i, resultColor);
                    }
                    else if (textIndex++ == bytes.Length)
                    {
                        var later = '\0';

                        var R1 = (byte)(later >> 6);
                        var G1 = (byte)((byte)(later << 2) >> 6);
                        var B1 = (byte)((byte)(later << 4) >> 6);
                        var A1 = (byte)((byte)(later << 6) >> 6);

                        byte R = (byte)((color.R >> 2 << 2) | R1);
                        byte G = (byte)((color.G >> 2 << 2) | G1);
                        byte B = (byte)((color.B >> 2 << 2) | B1);
                        byte A = (byte)((color.A >> 2 << 2) | A1);

                        bmp.SetPixel(j, i, Color.FromArgb(A, R, G, B));
                    }
                    else
                    {
                        bmp.SetPixel(j, i, color);
                    }
                }
            }

            return bmp;
        }

        public Bitmap EmbedText(Bitmap input, string text, string? key = null)
        {
            return this.Embed(input, Encoding.ASCII.GetBytes(text));
        }

        public byte[] Extract(Bitmap input, string key = null)
        {
            List<byte> result = new List<byte>();
            for (int i = 0; i < input.Height; i++)
            {
                for (int j = 0; j < input.Width; j++)
                {
                    var color = input.GetPixel(j, i);
                    char later;

                    var R = (byte)(color.R << 6);
                    var G = (byte)(color.G << 6) >> 2;
                    var B = (byte)(color.B << 6) >> 4;
                    var A = (byte)(color.A << 6) >> 6;

                    later = (char)(R | G | B | A);
                    result.Add((byte)later);

                    if (later == '\0')
                    {
                        return result.ToArray();
                    }
                }
            }

            return result.ToArray();
        }

        public string ExtractText(Bitmap input, string? key = null)
        {
            return ASCIIEncoding.ASCII.GetString(this.Extract(input));
        }
    }
}
