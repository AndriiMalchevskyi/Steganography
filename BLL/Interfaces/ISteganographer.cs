using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ISteganographer
    {
        public void SetNewSource(string filePath);
        public void SetNewSource(byte[] bytes);

        public bool SetLSBAlgorithm();

        public bool SetSimpleAlgorithm();

        public byte[] Encrypt(string text);

        public byte[] Encrypt(byte[] bytes);

        public byte[] Decrypt(string? key = null);

        public string ConvertBytesToString(byte[] bytes);

        public void SaveInFile(string path);

        public int GetFreeSpace();
    }
}
