using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public abstract class IEncoderFactory<T>
    {
        public abstract IEncoder<T> GetEncoderLSB();

        public abstract IEncoder<T> GetEncoderSimple();
    }
}
