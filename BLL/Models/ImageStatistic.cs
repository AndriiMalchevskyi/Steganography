using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace BLL.Models
{
    public class ImageStatistic
    {
        public void getStatistic(Bitmap original, Bitmap decrypted)
        {
            double SNR = getSignalNoiseRatio(original, decrypted);
            double NAAD = getNormalizedAverageAbsoluteDifference(original, decrypted);
            double IF = getImageFidelity(original, decrypted);
            double MSE = getMeanSquareError(original, decrypted);
            double AD = getAbsoluteDifference(original, decrypted);
        }

        private double getSignalNoiseRatio(Bitmap original, Bitmap decrypted)
        {
            double result = 0;
            double A = 0;
            double B = 0;
            for (int i = 0; i < original.Height; i++)
            {
                for (int j = 0; j < original.Width; j++)
                {
                    var pixel1 = original.GetPixel(j, i);
                    var pixel2 = decrypted.GetPixel(j, i);

                    var X1 = pixel1.A + pixel1.R + pixel1.G + pixel1.B;
                    var X2 = pixel2.A + pixel2.R + pixel2.G + pixel2.B;
                    A += X1 * X1;
                    B += (X1 - X2) * (X1 - X2);
                }
            }
            if (B!=0) {
                result = A / B;
            }
            else
            {
                result = double.PositiveInfinity;
            }

            return result;
        }

        private double getNormalizedAverageAbsoluteDifference(Bitmap original, Bitmap decrypted)
        {
            double result = 0;
            double A = 0;
            double B = 0;
            for (int i = 0; i < original.Height; i++)
            {
                for (int j = 0; j < original.Width; j++)
                {
                    var pixel1 = original.GetPixel(j, i);
                    var pixel2 = decrypted.GetPixel(j, i);

                    var X1 = pixel1.A + pixel1.R + pixel1.G + pixel1.B;
                    var X2 = pixel2.A + pixel2.R + pixel2.G + pixel2.B;
                    A += Math.Abs(X1 - X2);
                    B += Math.Abs(X1);
                }
            }
            if (B != 0)
            {
                result = A / B;
            }
            else
            {
                result = double.PositiveInfinity;
            }

            return result;
        }

        private double getImageFidelity(Bitmap original, Bitmap decrypted)
        {
            double result = 0;
            double A = 0;
            double B = 0;
            for (int i = 0; i < original.Height; i++)
            {
                for (int j = 0; j < original.Width; j++)
                {
                    var pixel1 = original.GetPixel(j, i);
                    var pixel2 = decrypted.GetPixel(j, i);

                    var X1 = pixel1.A + pixel1.R + pixel1.G + pixel1.B;
                    var X2 = pixel2.A + pixel2.R + pixel2.G + pixel2.B;
                    A += (X1 - X2) * (X1 - X2);
                    B += X1 * X1;
                }
            }
            if (B != 0)
            {
                result = 1.0 - A / B;
            }
            else
            {
                result = double.NegativeInfinity;
            }

            return result;
        }

        private double getMeanSquareError(Bitmap original, Bitmap decrypted)
        {
            double result = 0;
            double A = 0;
            for (int i = 0; i < original.Height; i++)
            {
                for (int j = 0; j < original.Width; j++)
                {
                    var pixel1 = original.GetPixel(j, i);
                    var pixel2 = decrypted.GetPixel(j, i);

                    var X1 = pixel1.A + pixel1.R + pixel1.G + pixel1.B;
                    var X2 = pixel2.A + pixel2.R + pixel2.G + pixel2.B;
                    A += (X1 - X2) * (X1 - X2);
                }
            }
            result = (1.0 / (original.Width*original.Height)) * A;

            return result;
        }

        private double getAbsoluteDifference(Bitmap original, Bitmap decrypted)
        {
            double result = 0;
            double A = 0;
            for (int i = 0; i < original.Height; i++)
            {
                for (int j = 0; j < original.Width; j++)
                {
                    var pixel1 = original.GetPixel(j, i);
                    var pixel2 = decrypted.GetPixel(j, i);

                    var X1 = pixel1.A + pixel1.R + pixel1.G + pixel1.B;
                    var X2 = pixel2.A + pixel2.R + pixel2.G + pixel2.B;
                    A += Math.Abs(X1 - X2);
                }
            }
            result = (1.0 / (original.Width * original.Height)) * A;

            return result;
        }
    }
}
