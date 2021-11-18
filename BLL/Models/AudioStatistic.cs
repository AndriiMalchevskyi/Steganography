using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class AudioStatistic
    {
        public void getStatistic(byte[] original, byte[] decrypted)
        {
            double SNR = getSignalNoiseRatio(original, decrypted);
            double NAAD = getNormalizedAverageAbsoluteDifference(original, decrypted);
            double IF = getImageFidelity(original, decrypted);
            double MSE = getMeanSquareError(original, decrypted);
            double AD = getAbsoluteDifference(original, decrypted);
        }

        private double getSignalNoiseRatio(byte[] original, byte[] decrypted)
        {
            double result = 0;
            double A = 0;
            double B = 0;
            for (int i = 0; i < original.Length; i++)
            {
                    var X1 = original[i];
                    var X2 = decrypted[i];
                    A += X1 * X1;
                    B += (X1 - X2) * (X1 - X2);
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

        private double getNormalizedAverageAbsoluteDifference(byte[] original, byte[] decrypted)
        {
            double result = 0;
            double A = 0;
            double B = 0;
            for (int i = 0; i < original.Length; i++)
            {

                    var X1 = original[i];
                    var X2 = decrypted[i];
                    A += Math.Abs(X1 - X2);
                    B += Math.Abs(X1);
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

        private double getImageFidelity(byte[] original, byte[] decrypted)
        {
            double result = 0;
            double A = 0;
            double B = 0;
            for (int i = 0; i < original.Length; i++)
            {
                    var X1 = original[i];
                    var X2 = decrypted[i];
                    A += (X1 - X2) * (X1 - X2);
                    B += X1 * X1;
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

        private double getMeanSquareError(byte[] original, byte[] decrypted)
        {
            double result = 0;
            double A = 0;
            for (int i = 0; i < original.Length; i++)
            {
                    var X1 = original[i];
                    var X2 = decrypted[i];
                    A += (X1 - X2) * (X1 - X2);
            }
            result = (1.0 / (original.Length /** original.Height*/)) * A;

            return result;
        }

        private double getAbsoluteDifference(byte[] original, byte[] decrypted)
        {
            double result = 0;
            double A = 0;
            for (int i = 0; i < original.Length; i++)
            {

                    var X1 = original[i];
                    var X2 = decrypted[i];
                    A += Math.Abs(X1 - X2);
            }
            result = (1.0 / (original.Length /** original.Height*/)) * A;

            return result;
        }
    }
}
