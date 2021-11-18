using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IEncoderFactory<T>
    {
        public IEncoder<T> GetEncoderLSB();

        public IEncoder<T> GetEncoderSimple();
    }
}
