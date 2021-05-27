using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public abstract class IEncoder<T>
    {
        public abstract T EmbedText(T input, string text, string? key = null);

        public abstract string ExtractText(T input, string? key = null);
    }
}
