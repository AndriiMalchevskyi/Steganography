using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.AudioEncoders
{
    public class AudioEncoderSimple : IEncoder<byte[]>
    {
        public byte[] Embed(byte[] input, byte[] bytes, string key = null)
        {
            byte[] result = new byte[input.Length];
            int charIndex = 0;

            int charValue = bytes[charIndex++];

            if (input.Length / 2 < bytes.Length)
            {
                throw new Exception("little memory for wav lsb");
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (i % 2 == 1)
                {
                    result[i] = (byte)charValue;

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
                else
                {
                    result[i] = input[i];
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
            int charIndex = 0;

            int charValue = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (i % 2 == 1)
                {
                    charValue = input[i];

                    if ((char)charValue == '\0')
                    {
                        var p = ASCIIEncoding.ASCII.GetString(result.ToArray());
                        return result.ToArray();
                    }

                    char c = (char)charValue;
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
    }
}
