using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BLL
{
    public class ImageEncoderFactory: IEncoderFactory<Bitmap>
    {
        private IEncoder<Bitmap> encoder;
        public IEncoder<Bitmap> GetEncoderLSB()
        {
            return new ImageEncoderLSB();
        }

        public IEncoder<Bitmap> GetEncoderSimple()
        {
            return new ImageEncoderLSB2();
        }

        public IEncoder<Bitmap> GetEncoderKochZhao()
        {
            return new ImageEncoderKochZhao();
        }
    }
}
