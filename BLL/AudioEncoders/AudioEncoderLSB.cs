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

            if (input.Length / 2 < bytes.Length * 8)
            {
                throw new Exception("little memory for wav lsb");
            }
            int currentBit = 0;
            for (int i = 1; i < input.Length; i+=2)
            {
                var currentByte = input[i] - input[i] % 2;

                currentByte += charValue % 2;
                charValue /= 2;

                result[i] = (byte)currentByte;
                currentBit++;
                if (currentBit == 8)
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
                    currentBit = 0;
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
            int currentBit = 0;
            for (int i = 1; i < input.Length; i+=2)
            {
                charValue = charValue * 2 + input[i] % 2;
                currentBit++;
                if (currentBit == 8)
                {
                    charValue = ReverseBits(charValue);

                    if ((char)charValue == '\0')
                    {
                        return result.ToArray();
                    }

                    result.Add((byte)charValue);
                    charValue = 0;
                    currentBit = 0;
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
