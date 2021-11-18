using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.AudioEncoders
{
    public class AudioEncoderLSB : IEncoder<byte[]>
    {
        public byte[] Embed(byte[] input, byte[] bytes, string key = null)
        {
            byte[] result = new byte[input.Length];
            int charIndex = 0;

            int charValue = bytes[charIndex++];

            if (input.Length < bytes.Length * 8)
            {
                throw new Exception("little memory for wav lsb");
            }

            for (int i = 0; i < input.Length; i++)
            {
                var currentByte = input[i] - input[i] % 2;

                currentByte += charValue % 2;
                charValue /= 2;

                result[i] = (byte)currentByte;

                if (i % 8 == 0 && i > 0)
                {
                    if (charIndex < bytes.Length)
                    {
                        charValue = bytes[charIndex++];
                    }
                    else if (charIndex == bytes.Length)
                    {
                        charValue = '\0';
                        charIndex++;
                    }
                    else
                    {
                        Array.Copy(input, i, result, i, result.Length - i);
                        break;
                    }
                }
            }

            return result;
        }

        public byte[] EmbedText(byte[] input, string text, string key = null)
        {
            return this.Embed(input, Encoding.ASCII.GetBytes(text));
        }

        public byte[] Extract(byte[] input, string key = null)
        {
            List<byte> result = new List<byte>();

            int charValue = 0;

            for (int i = 0; i < input.Length; i++)
            {
                charValue = charValue * 2 + input[i] % 2;

                if (i % 8 == 0 && i > 0)
                {
                    charValue = ReverseBits(charValue);

                    if ((char)charValue == '\0')
                    {
                        return result.ToArray();
                    }

                    result.Add((byte)charValue);
                    charValue = 0;
                }
            }

            return result.ToArray();
        }

        public string ExtractText(byte[] input, string key = null)
        {
            return ASCIIEncoding.ASCII.GetString(this.Extract(input));
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
