using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BLL
{
    public class ImageEncoderFactory: IEncoderFactory<Bitmap>
    {
        private IEncoder<Bitmap> encoder;
        public override IEncoder<Bitmap> GetEncoderLSB()
        {
            return new ImageEncoderLSB();
        }

        public override IEncoder<Bitmap> GetEncoderSimple()
        {
            return new ImageEncoderSimple();
        }
    }
}
