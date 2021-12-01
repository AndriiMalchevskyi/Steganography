using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BLL
{
    class KochZhao // метод, суть якого у відносній заміні величин коефіцієнтів ДКП (кожний блок (сегмент) - призначений для вшивання 1біт данних) 
    {
        static Point p1 = new Point(6, 3); // точки для роботи з сегментами коефіцієнтів ДКП
        static Point p2 = new Point(3, 6); // точки для роботи з сегментами коефіцієнтів ДКП\
        private const int SizeSegment = 8; // розмір блоку(сегмента)

        public static StegoBitmap Hide(StegoBitmap stgbmap, string txt, Colours colour, int CoefDif)
        {
            var bmap = stgbmap.GetImage();
            if ((bmap.Width % SizeSegment) != 0 || (bmap.Height % SizeSegment) != 0) //обрізаємо зображення якщо воно не ділиться рівно на блок 
                CommonFunc.Cut(ref bmap, SizeSegment);

            var arrForHiding = new byte[bmap.Width, bmap.Height]; // массив байтів тексту
            //вибираємо з зображення пікселі вибраного кольору
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    if (colour == Colours.Red)
                        arrForHiding[i, j] = bmap.GetPixel(i, j).R;
                    else if (colour == Colours.Green)
                        arrForHiding[i, j] = bmap.GetPixel(i, j).G;
                    else if (colour == Colours.Blue)
                        arrForHiding[i, j] = bmap.GetPixel(i, j).B;
                    else
                        throw new NullReferenceException();
                }
            }

            var stgByte = Encoding.ASCII.GetBytes(txt);
            byte[] len = CommonFunc.LenInBytes(txt.Length, CommonFunc.Size(txt.Length));        
            byte[] txtByte = new byte[2 + len.Length + stgByte.Length];
            txtByte[0] = Convert.ToByte('Z'); // перший елемент масиву містить мітку що в зображенні є повідомлення 
            txtByte[1] = CommonFunc.Size(txt.Length);
            int ind = 0;
            for (int i = 0; i < len.Length; i++)
                txtByte[i + 2] = len[ind++]; 
            ind = 0;
            for (int i = 0; i < stgByte.Length; i++)
                txtByte[i + 2 + len.Length] = stgByte[ind++]; 
            var segm = new List<byte[,]>();
            Separate(arrForHiding, segm, bmap.Width, bmap.Height, SizeSegment);
            // ДКП
            var dctList = new List<double[,]>();
            foreach (var b in segm)
                dctList.Add(DCT(b));
            SetText(txtByte, ref dctList, CoefDif);
            // зворотнє ДКП
            var idctList = new List<double[,]>();
            foreach (var d in dctList)
                idctList.Add(IDCT(d));
            var newArr = new double[bmap.Width, bmap.Height];
            Join(ref newArr, idctList, bmap.Width, bmap.Height, SizeSegment); //обєднуємо блоки
            Normalize(ref newArr);
            return new StegoBitmap(bmap, newArr, colour);
        }

        private static void Separate(byte[,] arr, List<byte[,]> segmList, int width, int height, int sizeSegm)
        {
            int numSW = width / sizeSegm;
            int numSH = height / sizeSegm;
            for (int i = 0; i < numSW; i++)
            {
                int firstWPoint = i * sizeSegm; 
                int lastWPoint = firstWPoint + sizeSegm - 1;
                for (int j = 0; j < numSH; j++)
                {
                    int firstHPoint = j * sizeSegm; 
                    int lastHPoint = firstHPoint + sizeSegm - 1;
                    segmList.Add(SegmBytes(arr, firstWPoint, lastWPoint, firstHPoint, lastHPoint));
                }
            }
        }

        private static byte[,] SegmBytes(byte[,] arr, int a, int b, int c, int d)
        {
            var sg = new byte[b - a + 1, d - c + 1];
            for (int i = a, x = 0; i <= b; i++, x++)
                for (int j = c, y = 0; j <= d; j++, y++)
                    sg[x, y] = arr[i, j];
            return sg;
        }

        private static double GetCoefficient(int arg)
        {
            if (arg == 0)
                return 1.0 / Math.Sqrt(2);
            return 1;
        }

        // пряме ДКП
        private static double[,] DCT(byte[,] b)
        {
            int len = b.GetLength(0);
            double[,] arrDCT = new double[len, len];
            double temp = 0;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    temp = 0;
                    for (int x = 0; x < len; x++)
                    {
                        for (int y = 0; y < len; y++)
                            temp += b[x, y] * Math.Cos(Math.PI * i * (2 * x + 1) / (2 * len)) * Math.Cos(Math.PI * j * (2 * y + 1) / (2 * len));
                    }
                    arrDCT[i, j] = GetCoefficient(j) * GetCoefficient(i) * temp / Math.Sqrt(2 * len);
                }
            }
            return arrDCT;
        }

        private static void SetText(byte[] txt, ref List<double[,]> DCT, int coefDif)
        {
            List<int> freePos = new List<int>();
            for (int i = 0; i < DCT.Count; i++)
                freePos.Add(i);
            for (int i = 0; i < txt.Length; i++)
            {
                bool[] bitsSymb = CommonFunc.ByteBoolArr(txt[i]);
                for (int j = 0; j < 8; j++)
                {
                    bool currentBit = bitsSymb[j];
                    int pos = freePos[0];
                    freePos.RemoveAt(0);

                    double AbsP1 = Math.Abs(DCT[pos][p1.X, p1.Y]);
                    double AbsP2 = Math.Abs(DCT[pos][p2.X, p2.Y]);
                    int z1 = 1, z2 = 1;
                    if (DCT[pos][p1.X, p1.Y] < 0)
                        z1 = -1;
                    if (DCT[pos][p2.X, p2.Y] < 0)
                        z2 = -1;
                    if (currentBit)
                    {
                        if (AbsP1 - AbsP2 >= -coefDif)
                            AbsP2 = coefDif + AbsP1 + 1;
                    }
                    else
                    {
                        if (AbsP1 - AbsP2 <= coefDif)
                            AbsP1 = coefDif + AbsP2 + 1;
                    }

                    DCT[pos][p1.X, p1.Y] = z1 * AbsP1;
                    DCT[pos][p2.X, p2.Y] = z2 * AbsP2;
                }
            }
        }

        // зворотнє ДКП
        private static double[,] IDCT(double[,] dct)
        {
            int len = dct.GetLength(0);
            double[,] result = new double[len, len];
            double temp = 0;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    temp = 0;
                    for (int x = 0; x < len; x++)
                    {
                        for (int y = 0; y < len; y++)
                            temp += GetCoefficient(x) * GetCoefficient(y) * dct[x, y] * Math.Cos(Math.PI * x * (2 * i + 1) / (2 * len)) * Math.Cos(Math.PI * y * (2 * j + 1) / (2 * len));
                    }
                    result[i, j] = temp / (Math.Sqrt(2 * len));
                }
            }
            return result;
        }


        private static void Join(ref double[,] arr, List<double[,]> Idct, int width, int height, int sizeSegm)
        {
            var temp = Idct.ToArray();
            int numSW = width / sizeSegm; 
            int numSH = height / sizeSegm; 
            int k = 0;
            for (int i = 0; i < numSW; i++)
            {
                int firstWPoint = i * sizeSegm;
                int lastWPoint = firstWPoint + sizeSegm - 1;
                for (int j = 0; j < numSH; j++)
                {
                    int firstHPoint = j * sizeSegm;
                    int lastHPoint = firstHPoint + sizeSegm - 1;
                    Insert(ref arr, temp[k], firstWPoint, lastWPoint, firstHPoint, lastHPoint);
                    k++;
                }
            }
        }

        private static void Insert(ref double[,] arr, double[,] temp, int firstWPoint, int lastWPoint, int firstHPoint, int lastHPoint)
        {
            for (int i = firstWPoint, u = 0; i < lastWPoint + 1; i++, u++)
            {
                for (int j = firstHPoint, v = 0; j < lastHPoint + 1; j++, v++)
                    arr[i, j] = temp[u, v];
            }
        }

        private static void Normalize(ref double[,] Idct)
        {
            double min = double.MaxValue, max = double.MinValue;
            for (int i = 0; i < Idct.GetLength(0); i++)
            {
                for (int j = 0; j < Idct.GetLength(1); j++)
                {
                    if (Idct[i, j] > max)
                        max = Idct[i, j]; 
                    if (Idct[i, j] < min)
                        min = Idct[i, j];
                }
            }
            for (int i = 0; i < Idct.GetLength(0); i++)
            {
                for (int j = 0; j < Idct.GetLength(1); j++)
                    Idct[i, j] = 255 * (Idct[i, j] + Math.Abs(min)) / (max + Math.Abs(min));
            }
        }

        public static bool IsHiddenText(StegoBitmap stgbmap, Colours c)
        {
            var bmap = stgbmap.GetImage();
            int width = bmap.Width;
            int height = bmap.Height;
            var arrWhereHide = new byte[bmap.Width, bmap.Height];
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    if (c == Colours.Red)
                        arrWhereHide[i, j] = bmap.GetPixel(i, j).R;
                    else if (c == Colours.Green)
                        arrWhereHide[i, j] = bmap.GetPixel(i, j).G;
                    else if (c == Colours.Blue)
                        arrWhereHide[i, j] = bmap.GetPixel(i, j).B;
                    else
                        throw new NullReferenceException();
                }
            }
            var segm = new List<byte[,]>();
            Separate(arrWhereHide, segm, bmap.Width, bmap.Height, SizeSegment);

            var dctList = new List<double[,]>();
            foreach (var b in segm)
                dctList.Add(DCT(b));
            var txtByte = new List<byte>();
            List<int> possibPos = new List<int>();
            for (int i = 0; i < dctList.Count; i++)
                possibPos.Add(i);
            var bits = new bool[8];
            for (int j = 0; j < 8; j++)
            {
                int pos = possibPos[0];
                possibPos.RemoveAt(0);
                double AbsPoint1 = Math.Abs(dctList[pos][p1.X, p1.Y]);
                double AbsPoint2 = Math.Abs(dctList[pos][p2.X, p2.Y]);

                if (AbsPoint1 > AbsPoint2)
                    bits[j] = false;
                else if (AbsPoint1 < AbsPoint2)
                    bits[j] = true;
            }
            txtByte.Add(CommonFunc.BoolArrByte(bits));
            if (txtByte.ToArray()[0] != Convert.ToByte('Z')) 
                return false;
            return true;
        }

        public static string GetHiddenText(StegoBitmap stgbmap, Colours c)
        {
            var bmap = stgbmap.GetImage();
            int width = bmap.Width;
            int height = bmap.Height;
            var arrWhereHide = new byte[bmap.Width, bmap.Height];
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    if (c == Colours.Red)
                        arrWhereHide[i, j] = bmap.GetPixel(i, j).R;
                    else if (c == Colours.Green)
                        arrWhereHide[i, j] = bmap.GetPixel(i, j).G;
                    else if (c == Colours.Blue)
                        arrWhereHide[i, j] = bmap.GetPixel(i, j).B;
                    else
                        throw new NullReferenceException();
                }
            }
            var segm = new List<byte[,]>();
            Separate(arrWhereHide, segm, bmap.Width, bmap.Height, SizeSegment);// розбиваємо массив на блоки
            // ДКП
            var dctList = new List<double[,]>();
            foreach (var b in segm)
                dctList.Add(DCT(b)); // список  коефіцієнтів ДКП
            var txtByte = new List<byte>();
            List<int> possibPos = new List<int>(); // можливі позиції в залежності з розміром списку коефіцієнтів ДКП
            for (int i = 0; i < dctList.Count; i++)
                possibPos.Add(i);
            int end = 2; //кінець проходу
            bool LenDone = true;
            for (int i = 0; i < end; i++)
            {
                var bits = new bool[8]; // біти символа
                for (int j = 0; j < 8; j++)
                {
                    int pos = possibPos[0]; 
                    possibPos.RemoveAt(0);
                    double AbsPoint1 = Math.Abs(dctList[pos][p1.X, p1.Y]);
                    double AbsPoint2 = Math.Abs(dctList[pos][p2.X, p2.Y]);

                    if (AbsPoint1 > AbsPoint2)
                        bits[j] = false;
                    else if (AbsPoint1 < AbsPoint2)
                        bits[j] = true;
                }
                txtByte.Add(CommonFunc.BoolArrByte(bits));
                if (i == 0 && txtByte.ToArray()[0] != Convert.ToByte('Z')) 
                    return "";
                else if (i == 1) 
                {
                    end = txtByte.ToArray()[1] + 2;
                    txtByte.Clear();
                }
                else if (LenDone && i + 1 == end) 
                {
                    end += CommonFunc.IntBytes(txtByte);
                    txtByte.Clear();
                    LenDone = false;
                }
            }

            return ASCIIEncoding.ASCII.GetString(txtByte.ToArray());
        }
    }

    public static class CommonFunc
    {
        public static byte Size(int len)
        {
            if (len > 0 && len < 256)
                return 1; 
            else if (len < 65536)
                return 2; 
            else
                return 4; 
        }

        public static byte[] LenInBytes(int len, int sizeLen)
        {
            if (sizeLen == 1)
            {
                var arr = new byte[1] { (byte)len };
                return arr;
            }
            else if (sizeLen == 2)
                return BitConverter.GetBytes((short)len);
            else
                return BitConverter.GetBytes(len);
        }

        public static bool[] ByteBoolArr(byte b)
        {
            bool[] arr = new bool[8];
            for (int i = 0; i < 8; i++)
                arr[i] = (b & (1 << i)) != 0 ? true : false;
            Array.Reverse(arr); 
            return arr;
        }

        public static byte BoolArrByte(bool[] arr)
        {
            byte res = 0;
            int ind = 8 - arr.Length;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i])
                    res |= (byte)(1 << (7 - ind)); 
                ind++;
            }
            return res;
        }

        public static int IntBytes(List<byte> lenBytes)
        {
            int ans = 0;
            for (int i = 0; i < lenBytes.Count; i++)
                ans |= lenBytes[i] << i * 8; 
            return ans;
        }

        public static void Cut(ref Bitmap img, int sizeSegm)
        {
            int x = img.Width % sizeSegm;
            int y = img.Height % sizeSegm;
            var newImg = new Bitmap(img.Width - x, img.Height - y);
            for (int i = 0; i < newImg.Width; i++)
            {
                for (int j = 0; j < newImg.Height; j++)
                    newImg.SetPixel(i, j, img.GetPixel(i, j));
            }
            img = newImg;
        }
    }
}
