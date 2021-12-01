using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace BLL
{
    public enum Colours { Red, Green, Blue }

    public class StegoBitmap
    {
        readonly private Bitmap sourceBitmap;

        public string FileSize { get; }
        public byte[] RedColour { get; }
        public byte[] GreenColour { get; }
        public byte[] BlueColour { get; }

        public StegoBitmap(Bitmap bitmap)
        {
            sourceBitmap = bitmap;
            FileSize = bitmap.Height.ToString() + " x " + bitmap.Width.ToString();
            RedColour = ReadColour(bitmap, Colours.Red);
            GreenColour = ReadColour(bitmap, Colours.Green);
            BlueColour = ReadColour(bitmap, Colours.Blue);
        }

        public StegoBitmap(StegoBitmap stgBitmap, byte[] changedColour, Colours c)
        {
            RedColour = stgBitmap.RedColour;
            GreenColour = stgBitmap.GreenColour;
            BlueColour = stgBitmap.BlueColour;
            if (c == Colours.Red)
                RedColour = changedColour;
            if (c == Colours.Green)
                GreenColour = changedColour;
            if (c == Colours.Blue)
                BlueColour = changedColour;

            sourceBitmap = new Bitmap(stgBitmap.sourceBitmap.Width, stgBitmap.sourceBitmap.Height);

            int ind = 0;
            for (int i = 0; i < sourceBitmap.Width; i++)
            {
                for (int j = 0; j < sourceBitmap.Height; j++)
                {
                    sourceBitmap.SetPixel(i, j, Color.FromArgb(RedColour[ind], GreenColour[ind], BlueColour[ind]));
                    ind++;
                }
            }
        }

        public StegoBitmap(Bitmap bmap, double[,] newArr, Colours c)
        {
            sourceBitmap = new Bitmap(bmap.Width, bmap.Height);
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    if (c == Colours.Red)
                        sourceBitmap.SetPixel(i, j, Color.FromArgb((byte)Math.Round(newArr[i, j]), bmap.GetPixel(i, j).G, bmap.GetPixel(i, j).B));
                    else if (c == Colours.Green)
                        sourceBitmap.SetPixel(i, j, Color.FromArgb(bmap.GetPixel(i, j).R, (byte)Math.Round(newArr[i, j]), bmap.GetPixel(i, j).B));
                    else
                        sourceBitmap.SetPixel(i, j, Color.FromArgb(bmap.GetPixel(i, j).R, bmap.GetPixel(i, j).G, (byte)Math.Round(newArr[i, j])));
                }
            }
            RedColour = ReadColour(sourceBitmap, Colours.Red);
            GreenColour = ReadColour(sourceBitmap, Colours.Green);
            BlueColour = ReadColour(sourceBitmap, Colours.Blue);
        }

        public StegoBitmap(Bitmap bmap, byte[,] newArr, Colours c)
        {
            sourceBitmap = new Bitmap(bmap.Width, bmap.Height);
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    if (c == Colours.Red)
                        sourceBitmap.SetPixel(i, j, Color.FromArgb(newArr[i, j], bmap.GetPixel(i, j).G, bmap.GetPixel(i, j).B));
                    else if (c == Colours.Green)
                        sourceBitmap.SetPixel(i, j, Color.FromArgb(bmap.GetPixel(i, j).R, newArr[i, j], bmap.GetPixel(i, j).B));
                    else
                        sourceBitmap.SetPixel(i, j, Color.FromArgb(bmap.GetPixel(i, j).R, bmap.GetPixel(i, j).G, newArr[i, j]));
                }
            }
            RedColour = ReadColour(sourceBitmap, Colours.Red);
            GreenColour = ReadColour(sourceBitmap, Colours.Green);
            BlueColour = ReadColour(sourceBitmap, Colours.Blue);
        }

        public Bitmap GetImage()
        {
            return sourceBitmap;
        }

        public int GetMaxCapacityMethod1()
        {
            return (sourceBitmap.Width * sourceBitmap.Height / 8) - 2 - (sourceBitmap.Width * sourceBitmap.Height / 8).ToString().Length;
        }


        public int GetMaxCapacityMethod2()
        {
            int wid = sourceBitmap.Width;
            int height = sourceBitmap.Height;
            if ((wid % 8) != 0 || (wid % 8) != 0) 
            {
                wid -= wid % 8;
                height -= height % 8;
            }
            int numSeg = (wid * height) / (8 * 8);
            return (numSeg / 8) - 2 - (numSeg / 8).ToString().Length; 
        }

        private byte[] ReadColour(Bitmap bitmap, Colours c)
        {
            byte[] colArr = new byte[bitmap.Height * bitmap.Width]; 
            int ind = 0;

            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                    if (c == Colours.Red)
                        colArr[ind++] = bitmap.GetPixel(i, j).R;
                    else if (c == Colours.Green)
                        colArr[ind++] = bitmap.GetPixel(i, j).G;
                    else if (c == Colours.Blue)
                        colArr[ind++] = bitmap.GetPixel(i, j).B;
            return colArr;
        }


        public byte[] GetColour(Colours channel)
        {
            if (channel == Colours.Red)
                return RedColour;
            else if (channel == Colours.Green)
                return GreenColour;
            else if (channel == Colours.Blue)
                return BlueColour;
            else
                return null;
        }


        public void SaveBitmap(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            sourceBitmap.Save(fileName);
        }

    }
}
