using BLL.AudioEncoders;
using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BLL
{
    public class AudioSteganographer : ISteganographer
    {
        private IEncoder<byte[]> encoder;
        public AudioWAV Audio { get; private set; }
        public Dictionary<string, double> Statistic { get; private set; }
        public AudioSteganographer() { }
        public AudioSteganographer(string audioPath)
        {
            this.SetNewSource(audioPath);
        }

        public void SetNewSource(string audioPath)
        {
            this.Audio = new AudioWAV(audioPath);
            var d = 0;
        }
        public void SetNewSource(byte[] bytes)
        {
            this.Audio = new AudioWAV(bytes);
        }

        public bool SetLSBAlgorithm()
        {
            IEncoderFactory<byte[]> audioEncoderFactory = new AudioEncoderFactory();
            if (this.encoder == null || this.encoder.GetType() != audioEncoderFactory.GetEncoderLSB().GetType()) {
                this.encoder = audioEncoderFactory.GetEncoderLSB();
                return true;
            }

            return false;
        }

        public bool SetSimpleAlgorithm()
        {
            IEncoderFactory<byte[]> audioEncoderFactory = new AudioEncoderFactory();
            if (this.encoder == null || this.encoder.GetType() != audioEncoderFactory.GetEncoderSimple().GetType())
            {
                this.encoder = audioEncoderFactory.GetEncoderSimple();
                return true;
            }

            return false;
        }

        public byte[] Encrypt(string text)
        {
            return Audio.SetSoundData(encoder.EmbedText(this.Audio.GetSoundData(), text));
        }

        public byte[] Encrypt(byte[] bytes)
        {
            AudioStatistic stc = new AudioStatistic();
            var decrypted = encoder.Embed(this.Audio.GetSoundData(), bytes);
            this.Statistic = stc.getStatistic(this.Audio.GetSoundData(), decrypted);
            return Audio.SetSoundData(decrypted);
        }

        public byte[] Decrypt(string? key = null)
        {
            return encoder.Extract(this.Audio.GetSoundData(), key);
        }

        public void SaveInFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllBytes(path, this.Audio.audioBytesArr);
        }

        public int GetFreeSpace()
        {
            int res = this.Audio.DataSize / 2;
            if (this.Audio != null && encoder.GetType() == typeof(AudioEncoderLSB2))
            {
                res = this.Audio.DataSize / 4;
            }
            else if (this.Audio != null && encoder.GetType() == typeof(AudioEncoderLSB))
            {
                res = this.Audio.DataSize / 8 / 2;
            }
            return res;
        }

        public string ConvertBytesToString(byte[] bytes)
        {
            return ASCIIEncoding.ASCII.GetString(bytes);
        }

        public bool SetKochZhaoAlgorithm()
        {
            throw new NotImplementedException();
        }
    }
}
