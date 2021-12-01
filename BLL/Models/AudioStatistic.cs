using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class AudioStatistic
    {
        public Dictionary<string, double> getStatistic(byte[] original, byte[] decrypted)
        {
            var map = new Dictionary<string, double>();
            double SNR = getSignalNoiseRatio(original, decrypted);
            double NAAD = getNormalizedAverageAbsoluteDifference(original, decrypted);
            double IF = getImageFidelity(original, decrypted);
            double MSE = getMeanSquareError(original, decrypted);
            double AD = getAbsoluteDifference(original, decrypted);

            map.Add("SNR", SNR);
            map.Add("NAAD", NAAD);
            map.Add("IF", IF);
            map.Add("MSE", MSE);
            map.Add("AD", AD);

            return map;
        }

        private double getSignalNoiseRatio(byte[] original, byte[] decrypted)
        {
            double result = 0;
            double A = 0;
            double B = 0;
            for (int i = 0; i < original.Length; i+=2)
            {
                var X1 = BitConverter.ToInt16(original, i);
                var X2 = BitConverter.ToInt16(decrypted, i);
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
            for (int i = 0; i < original.Length; i+=2)
            {

                var X1 = BitConverter.ToInt16(original, i);
                var X2 = BitConverter.ToInt16(decrypted, i);
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
            for (int i = 0; i < original.Length; i+=2)
            {
                var X1 = BitConverter.ToInt16(original, i);
                var X2 = BitConverter.ToInt16(decrypted, i);
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

            if (result < 0)
            {
                result = 0;
            }

            return result;
        }

        private double getMeanSquareError(byte[] original, byte[] decrypted)
        {
            double result = 0;
            double A = 0;
            for (int i = 0; i < original.Length; i+=2)
            {
                var X1 = BitConverter.ToInt16(original, i);
                var X2 = BitConverter.ToInt16(decrypted, i);
                A += (X1 - X2) * (X1 - X2);
            }
            result = (1.0 / (original.Length /** original.Height*/)) * A;

            return result;
        }

        private double getAbsoluteDifference(byte[] original, byte[] decrypted)
        {
            double result = 0;
            double A = 0;
            for (int i = 0; i < original.Length; i+=2)
            {

                var X1 = BitConverter.ToInt16(original, i);
                var X2 = BitConverter.ToInt16(decrypted, i);
                A += Math.Abs(X1 - X2);
            }
            result = (1.0 / (original.Length /** original.Height*/)) * A;

            return result;
        }
    }
}
