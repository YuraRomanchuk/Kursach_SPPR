using System;

namespace DSS_Busol.MathClasses
{
    public static class DataConvertationClass
    {
        public static bool NormalizationRow(ref double[] dataAr, int typeNorm)
        {
            if (IsUndefinedElement(dataAr)) return false;
            if (typeNorm != -1 & typeNorm != 0) return false;
            double[] MaxEl = MaxElementAr(dataAr);
            double[] MinEl = MinElementAr(dataAr);
            double znam = MinEl[0] - MaxEl[0];
            if (znam == 0) return false;
            for (int i = 0; i < dataAr.Length; i++)
            {
                if (typeNorm == 0) dataAr[i] = (MinEl[0] - dataAr[i]) / znam;
                else dataAr[i] = 2 * (MinEl[0] - dataAr[i]) / znam - 1;
            }
            return true;
        }
        public static double[] MaxElementAr(double[] mass)
        {
            double[] outMass = { mass[0], 0 };
            if (IsUndefinedElement(mass))
            {
                outMass[0] = double.NaN;
                outMass[1] = -1;
                return outMass;
            }
            for (int i = 1; i < mass.Length; i++)
            {
                if (outMass[0] < mass[i])
                {
                    outMass[0] = mass[i];
                    outMass[1] = i;
                }
            }
            return outMass;
        }
        public static double[] MinElementAr(double[] mass)
        {
            double[] outMass = { mass[0], 0 };
            if (IsUndefinedElement(mass))
            {
                outMass[0] = double.NaN;
                outMass[1] = -1;
                return outMass;
            }
            for (int i = 1; i < mass.Length; i++)
            {
                if (outMass[0] > mass[i])
                {
                    outMass[0] = mass[i];
                    outMass[1] = i;
                }
            }
            return outMass;
        }

        public static bool IsUndefinedElement(double[] dataAr)
        {
            foreach (double el in dataAr)
            {
                if (Double.IsNaN(el)) return true;
            }
            return false;
        }

        public static bool IsNegativeNumber(double[] dataAr)
        {
            foreach (double el in dataAr)
            {
                if (el <= 0) return true;
            }
            return false;
        }

        public static bool LogRow(ref double[] dataAr, double logBase)
        {
            if (IsNegativeNumber(dataAr) || IsUndefinedElement(dataAr)) return false;
            for (int i = 0; i < dataAr.Length; i++)
            {
                dataAr[i] = Math.Log(dataAr[i], logBase);
            }
            return true;
        }

        public static bool LnRow(ref double[] dataAr)
        {
            if (IsNegativeNumber(dataAr) || IsUndefinedElement(dataAr)) return false;
            for (int i = 0; i < dataAr.Length; i++)
            {
                dataAr[i] = Math.Log(dataAr[i]);
            }
            return true;
        }

        public static bool DifferRow(ref double[] dataAr, int diffRank)
        {
            if (diffRank < 1 || diffRank > dataAr.Length - 1) return false;
            for (int i = dataAr.Length - 1; i > -1; i--)
            {
                if (i < diffRank) dataAr[i] = double.NaN;
                else dataAr[i] = dataAr[i] - dataAr[i - diffRank];
            }
            return true;
        }
    }
}
