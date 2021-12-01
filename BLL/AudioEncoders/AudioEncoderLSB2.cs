using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.AudioEncoders
{
    public class AudioEncoderLSB2 : IEncoder<byte[]>
    {
        public byte[] Embed(byte[] input, byte[] bytes, string key = null)
        {
            byte[] result = new byte[input.Length];
            int charIndex = 0;
            Array.Copy(input, result, input.Length);

            int charValue = bytes[charIndex++];

            if (input.Length / 2 < bytes.Length)
            {
                throw new Exception("little memory for wav lsb");
            }

            for (int i = 3; i < input.Length; i += 4)
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

            for (int i = 3; i < input.Length; i += 4)
            {
                charValue = input[i];

                if ((char)charValue == '\0')
                {
                    var p = ASCIIEncoding.ASCII.GetString(result.ToArray());
                    return result.ToArray();
                }

                result.Add((byte)charValue);
                charValue = 0;
            }



            return result.ToArray();
        }

        public string ExtractText(byte[] input, string key = null)
        {
            return ASCIIEncoding.ASCII.GetString(this.Extract(input));
        }
    }
}
