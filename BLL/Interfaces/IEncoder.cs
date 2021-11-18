using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IEncoder<T>
    {
        public T EmbedText(T input, string text, string? key = null);

        public string ExtractText(T input, string? key = null);

        public T Embed(T input, byte[] bytes, string? key = null);
        public byte[] Extract(T input, string? key = null);
    }
}
