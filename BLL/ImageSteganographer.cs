using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace BLL
{
    public class ImageSteganographer
    {
        private IEncoder<Bitmap> encoder;
        public Bitmap Bitmap { get; private set; }

        public ImageSteganographer()
        {

        }

        public ImageSteganographer(string imagePath)
        {
            this.SetNewImage(imagePath);
        }

        public void SetNewImage(string imagePath)
        {
            this.Bitmap = new Bitmap(imagePath);
        }

        public void SetNewImage(Bitmap image)
        {
            var d = image.GetPixel(0, 0);
            this.Bitmap = image;
            d = this.Bitmap.GetPixel(0, 0);
            var t = 5;
        }

        public void SetLSBAlgorithm()
        {
            IEncoderFactory<Bitmap> imageEncoderFactory = new ImageEncoderFactory();
            this.encoder = imageEncoderFactory.GetEncoderLSB();
        }

        public void SetSimpleAlgorithm()
        {
            IEncoderFactory<Bitmap> imageEncoderFactory = new ImageEncoderFactory();
            this.encoder = imageEncoderFactory.GetEncoderSimple();
        }

        public Bitmap EncryptText(string text)
        {
            return encoder.EmbedText(this.Bitmap, text);
        }

        public string DecryptText(string? key = null)
        {
            var d = this.Bitmap.GetPixel(0, 0);
            return encoder.ExtractText(this.Bitmap, key);
        }

        public void SaveImage(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            this.Bitmap.Save(path);
        }

        public int GetFreeSpace()
        {


            int res = 0;
            if (this.Bitmap != null && encoder.GetType() == typeof(ImageEncoderSimple))
            {
                res = this.Bitmap.Width * this.Bitmap.Height;
            }
            else if (this.Bitmap != null && encoder.GetType() == typeof(ImageEncoderLSB))
            {
                res = this.Bitmap.Width * this.Bitmap.Height * 3 / 8;
            }
            return res;
        }
    }
}
