using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BLL
{
    public class ImageEncoderKochZhao : IEncoder<Bitmap>
    {
        public Bitmap Embed(Bitmap input, byte[] bytes, string key = null)
        {
            return this.EmbedText(input, ASCIIEncoding.ASCII.GetString(bytes));
        }

        public Bitmap EmbedText(Bitmap input, string text, string key = null)
        {
            var image = new StegoBitmap(input);
            return KochZhao.Hide(image, text, Colours.Blue, 55).GetImage();
        }

        public byte[] Extract(Bitmap input, string key = null)
        {
            return Encoding.ASCII.GetBytes(this.ExtractText(input));
        }

        public string ExtractText(Bitmap input, string key = null)
        {
            var image = new StegoBitmap(input);
            return KochZhao.GetHiddenText(image, Colours.Blue);
        }
    }
}
