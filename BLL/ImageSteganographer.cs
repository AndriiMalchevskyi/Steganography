using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace BLL
{
    public class ImageSteganographer: ISteganographer
    {
        private IEncoder<Bitmap> encoder;
        public Dictionary<string, double> Statistic { get; private set; }
        public Bitmap Bitmap { get; private set; }

        public ImageSteganographer()
        {

        }

        public ImageSteganographer(string imagePath)
        {
            this.SetNewSource(imagePath);
        }

        public void SetNewSource(string imagePath)
        {
            this.Bitmap = new Bitmap(imagePath);
        }

        public void SetNewSource(Bitmap image)
        {
            var d = image.GetPixel(0, 0);
            this.Bitmap = image;
            d = this.Bitmap.GetPixel(0, 0);
            var t = 5;
        }

        public void SetNewSource(byte[] bytes)
        {
            ImageConverter converter = new ImageConverter();
            this.Bitmap = (Bitmap)(converter.ConvertFrom(bytes));
        }

        public bool SetLSBAlgorithm()
        {
            IEncoderFactory<Bitmap> imageEncoderFactory = new ImageEncoderFactory();
            if (this.encoder == null || this.encoder.GetType() != imageEncoderFactory.GetEncoderLSB().GetType())
            {
                this.encoder = imageEncoderFactory.GetEncoderLSB();
                return true;
            }

            return false;
        }

        public bool SetKochZhaoAlgorithm()
        {
            var imageEncoderFactory = new ImageEncoderFactory();
            if (this.encoder == null || this.encoder.GetType() != imageEncoderFactory.GetEncoderKochZhao().GetType())
            {
                this.encoder = imageEncoderFactory.GetEncoderKochZhao();
                return true;
            }

            return false;
        }

        public bool SetSimpleAlgorithm()
        {
            IEncoderFactory<Bitmap> imageEncoderFactory = new ImageEncoderFactory();
            if (this.encoder == null || this.encoder.GetType() != imageEncoderFactory.GetEncoderSimple().GetType())
            {
                this.encoder = imageEncoderFactory.GetEncoderSimple();
                return true;
            }

            return false;
        }

        public byte[] Encrypt(string text)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(encoder.EmbedText(this.Bitmap, text), typeof(byte[]));
        }

        public byte[] Decrypt(string? key = null)
        {
            var d = this.Bitmap.GetPixel(0, 0);
            return Encoding.ASCII.GetBytes(encoder.ExtractText(this.Bitmap, key));
        }

        public void SaveInFile(string path)
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
            if (this.Bitmap != null && encoder.GetType() == typeof(ImageEncoderLSB2))
            {
                res = this.Bitmap.Width * this.Bitmap.Height;
            }
            else if (this.Bitmap != null && encoder.GetType() == typeof(ImageEncoderLSB))
            {
                res = this.Bitmap.Width * this.Bitmap.Height * 3 / 8;
            }
            else if (this.Bitmap != null && encoder.GetType() == typeof(ImageEncoderKochZhao))
            {
                res = this.Bitmap.Width * this.Bitmap.Height / 512;
            }
            return res;
        }

        public byte[] Encrypt(byte[] bytes)
        {
            ImageConverter converter = new ImageConverter();
            var bitmap = new Bitmap(this.Bitmap);
            var decrypted = encoder.Embed(this.Bitmap, bytes);
            ImageStatistic stc = new ImageStatistic();
            this.Statistic = stc.getStatistic(bitmap, decrypted);
            this.Bitmap = decrypted;
            return (byte[])converter.ConvertTo(this.Bitmap, typeof(byte[]));

        }

        public string ConvertBytesToString(byte[] bytes)
        {
            return ASCIIEncoding.ASCII.GetString(bytes);
        }
    }
}
