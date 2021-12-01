using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BLL.Models
{
    public class AudioWAV
    {
        public byte[] audioBytesArr { get; private set; }
        public int Size { get; private set; }
        public string Format { get; private set; }
        public short Channels { get; private set; } // 1-mono, 2-stereo ...
        public int SampleRate { get; private set; } // 44100, 16000, 8000 ...
        public int ByteRate { get; private set; } // SampleRate * Channels * (BitsPerSample / 8)
        public short BlockAlign { get; private set; } // Channels * (BitsPerSample / 8)
        public int BitsPerSample { get; private set; } // 8, 16, 24, 32
        public int DataSize { get; private set; }
        public short DataStartFromIndex { get; private set; }

        public int Duration
        {
            get
            {
                return DataSize / ByteRate;
            }
        }

        public int FrameSize
        {
            get
            {
                return BitsPerSample * Channels;
            }
        }

        public int Frames
        {
            get
            {
                return DataSize / FrameSize;
            }
        }

        public int FrameRate
        {
            get
            {
                return Frames / Duration;
            }
        }

        public AudioWAV(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);

            SetupMetadata(bytes);
        }


        public AudioWAV(byte[] bytesFile)
        {
            SetupMetadata(bytesFile);
        }

        public byte[] GetSoundData()
        {
            byte[] copy = new byte[DataSize];
            Array.Copy(audioBytesArr, DataStartFromIndex, copy, 0, DataSize);

            return copy;
        }

        public byte[] SetSoundData(byte[] arr)
        {
            Array.Copy(arr, 0, audioBytesArr, DataStartFromIndex, DataSize);

            return GetSoundData();
        }

        public byte[] GetBytes()
        {
            return audioBytesArr;
        }

        private void SetupMetadata(byte[] arr)
        {
            audioBytesArr = arr;
            var wavMetadata = new Dictionary<string, byte[]>();
            ///////RIFF SECTION////////////////////
            wavMetadata["id"] = new byte[4] { arr[0], arr[1], arr[2], arr[3] }; // returns RIFF
            var id = ASCIIEncoding.ASCII.GetString(wavMetadata["id"]);

            wavMetadata["size"] = new byte[4] { arr[4], arr[5], arr[6], arr[7] };
            Size = BitConverter.ToInt32(wavMetadata["size"]);

            wavMetadata["format"] = new byte[4] { arr[8], arr[9], arr[10], arr[11] };
            Format = ASCIIEncoding.ASCII.GetString(wavMetadata["format"]);

            ////////FORMAT SECTION//////////////////
            wavMetadata["id_format"] = new byte[4] { arr[12], arr[13], arr[14], arr[15] };
            var id_format = ASCIIEncoding.ASCII.GetString(wavMetadata["id_format"]);

            wavMetadata["format_section_size"] = new byte[4] { arr[16], arr[17], arr[18], arr[19] };
            var format_section_size = BitConverter.ToInt32(wavMetadata["format_section_size"]);

            wavMetadata["format_audio"] = new byte[2] { arr[20], arr[21] };
            var format_audio = BitConverter.ToInt16(wavMetadata["format_audio"]);

            wavMetadata["channels"] = new byte[2] { arr[22], arr[23] };
            Channels = BitConverter.ToInt16(wavMetadata["channels"]);

            wavMetadata["sample_rate"] = new byte[4] { arr[24], arr[25], arr[26], arr[27] };
            SampleRate = BitConverter.ToInt32(wavMetadata["sample_rate"]);

            wavMetadata["byte_rate"] = new byte[4] { arr[28], arr[29], arr[30], arr[31] };
            ByteRate = BitConverter.ToInt32(wavMetadata["byte_rate"]);

            wavMetadata["block_align"] = new byte[2] { arr[32], arr[33] };
            BlockAlign = BitConverter.ToInt16(wavMetadata["block_align"]);

            wavMetadata["bits_per_sample"] = new byte[2] { arr[34], arr[35] };
            BitsPerSample = BitConverter.ToInt16(wavMetadata["bits_per_sample"]);
            ////////DATA SECTION////////////////////
            wavMetadata["id_data"] = new byte[4] { arr[36], arr[37], arr[38], arr[39] };
            var id_data = ASCIIEncoding.ASCII.GetString(wavMetadata["id_data"]);

            wavMetadata["data_size"] = new byte[4] { arr[40], arr[41], arr[42], arr[43] };
            DataSize = BitConverter.ToInt32(wavMetadata["data_size"]);

            if (Format == "WAVE")
            {
                DataStartFromIndex = 44;
            }
        }
    }
}
