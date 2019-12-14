using System;

namespace DSS_Busol.MathClasses
{
    public static class Constants
    {
        public const double EPS = 0.0001;
        public static int ExpSign(int k)
        {
            return (k % 2 == 0) ? 1 : -1;
        }
        public static double Log(double z)
        {
            return 1 / (1 + Math.Exp(-z));
        }
    }
}
