using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.AudioEncoders
{
    public class AudioEncoderFactory : IEncoderFactory<byte[]>
    {
        public IEncoder<byte[]> GetEncoderLSB()
        {
            return new AudioEncoderLSB();
        }

        public IEncoder<byte[]> GetEncoderSimple()
        {
            return new AudioEncoderSimple();
        }
    }
}
