using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BLL
{
    public class ImageEncoderLSB: IEncoder<Bitmap>
    {
        public Bitmap Embed(Bitmap input, byte[] bytes, string key = null)
        {
            State state = State.Hiding;

            int charIndex = 0;

            int charValue = 0;

            long pixelElementIndex = 0;

            int zeros = 0;

            int R = 0, G = 0, B = 0;

            for (int i = 0; i < input.Height; i++)
            {
                for (int j = 0; j < input.Width; j++)
                {
                    Color pixel = input.GetPixel(j, i);

                    R = pixel.R - pixel.R % 2;
                    G = pixel.G - pixel.G % 2;
                    B = pixel.B - pixel.B % 2;

                    for (int n = 0; n < 3; n++)
                    {
                        if (pixelElementIndex % 8 == 0)
                        {
                            if (state == State.Filling_With_Zeros && zeros == 8)
                            {
                                if ((pixelElementIndex - 1) % 3 < 2)
                                {
                                    var pixe = input.GetPixel(j, i);
                                    input.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }

                                return input;
                            }

                            if (charIndex >= bytes.Length)
                            {
                                state = State.Filling_With_Zeros;
                            }
                            else
                            {
                                charValue = bytes[charIndex++];
                            }
                        }

                        switch (pixelElementIndex % 3)
                        {
                            case 0:
                                {
                                    if (state == State.Hiding)
                                    {
                                        R += charValue % 2;
                                        charValue /= 2;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    if (state == State.Hiding)
                                    {
                                        G += charValue % 2;
                                        charValue /= 2;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (state == State.Hiding)
                                    {
                                        B += charValue % 2;
                                        charValue /= 2;
                                    }
                                    var pixe = input.GetPixel(j,i);
                                    input.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }
                                break;
                        }

                        pixelElementIndex++;

                        if (state == State.Filling_With_Zeros)
                        {
                            zeros++;
                        }
                    }
                }
            }

            return input;
        }

        public Bitmap EmbedText(Bitmap bmp, string text, string? key = null)
        {
            return this.Embed(bmp, Encoding.ASCII.GetBytes(text));
        }

        public byte[] Extract(Bitmap input, string key = null)
        {
            int colorUnitIndex = 0;
            int charValue = 0;

            List<byte> result = new List<byte>();

            for (int i = 0; i < input.Height; i++)
            {
                for (int j = 0; j < input.Width; j++)
                {
                    Color pixel = input.GetPixel(j, i);

                    for (int n = 0; n < 3; n++)
                    {
                        switch (colorUnitIndex % 3)
                        {
                            case 0:
                                {
                                    charValue = charValue * 2 + pixel.R % 2;
                                }
                                break;
                            case 1:
                                {
                                    charValue = charValue * 2 + pixel.G % 2;
                                }
                                break;
                            case 2:
                                {
                                    charValue = charValue * 2 + pixel.B % 2;
                                }
                                break;
                        }

                        colorUnitIndex++;

                        if (colorUnitIndex % 8 == 0)
                        {
                            charValue = ReverseBits(charValue);

                            if (charValue == 0)
                            {
                                return result.ToArray();
                            }

                            result.Add((byte)charValue);
                        }
                    }
                }
            }

            return result.ToArray();
        }

        public string ExtractText(Bitmap bmp, string? key = null)
        {
            return ASCIIEncoding.ASCII.GetString(this.Extract(bmp));
        }

        protected int ReverseBits(int n)
        {
            int result = 0;

            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;
                n /= 2;
            }

            return result;
        }
    }
}
