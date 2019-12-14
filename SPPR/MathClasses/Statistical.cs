using System;

namespace DSS_Busol.MathClasses
{
    public static class Statistical
    {
        static public double Middle(double[] dataAr)
        {
            if (DataConvertationClass.IsUndefinedElement(dataAr)) return double.NaN;
            double temp = 0;
            foreach (double el in dataAr)
            {
                temp = temp + el;
            }
            temp = temp / dataAr.Length;
            return temp;
        }
        static public double Dispersion(double[] dataAr)
        {
            if (DataConvertationClass.IsUndefinedElement(dataAr)) return double.NaN;
            double middl = Middle(dataAr);
            double disp = 0;
            foreach (double el in dataAr)
            {
                disp = disp + Math.Pow((el - middl), 2);
            }
            disp = disp / (dataAr.Length - 1);
            return disp;
        }
        static public double Skewness(double[] dataAr)
        {
            if (DataConvertationClass.IsUndefinedElement(dataAr)) return double.NaN;
            double dis = Math.Sqrt(Dispersion(dataAr));
            double middl = Middle(dataAr);
            double skew = 0;
            foreach (double el in dataAr)
            {
                skew = skew + Math.Pow((el - middl) / dis, 3);
            }
            skew = skew / dataAr.Length;
            return skew;
        }
        static public double Kurtosis(double[] dataAr)
        {
            if (DataConvertationClass.IsUndefinedElement(dataAr)) return double.NaN;
            double dis = Math.Sqrt(Dispersion(dataAr));
            double middl = Middle(dataAr);
            double kurt = 0;
            foreach (double el in dataAr)
            {
                kurt = kurt + Math.Pow((el - middl) / dis, 4);
            }
            kurt = kurt / dataAr.Length;
            return kurt;
        }
        static public double[] ACF(double[] dataAr, int degree)
        {
            double[] ACArray;
            if (degree < 0 || DataConvertationClass.IsUndefinedElement(dataAr))
            {
                ACArray = new double[] { double.NaN };
                return ACArray;
            }
            ACArray = new double[degree + 1];
            ACArray[0] = 1;
            double midd = Middle(dataAr);
            double c0 = 0;
            foreach (double el in dataAr) { c0 = c0 + Math.Pow((el - midd), 2); }
            double ck;
            for (int k = 1; k <= degree; k++)
            {
                ck = 0;
                for (int i = 0; i < dataAr.Length - k; i++)
                {
                    ck += (dataAr[i] - midd) * (dataAr[i + k] - midd);
                }
                ACArray[k] = ck / c0;
            }
            return ACArray;
        }
        static private double PACFij(double[] pacfAr, int i, int j)
        {
            double res = 0;
            if (i == j) res = pacfAr[i];
            else
            {
                res = PACFij(pacfAr, i - 1, j) - PACFij(pacfAr, i, i) * PACFij(pacfAr, i - 1, i - j);
            }
            return res;
        }
        static public double[] PACF(double[] dataAr, int degree)
        {
            double[] PACFArray;
            if (degree < 1 || DataConvertationClass.IsUndefinedElement(dataAr))
            {
                PACFArray = new double[] { double.NaN };
                return PACFArray;
            }
            PACFArray = new double[degree + 1];
            double[] ACFArray = ACF(dataAr, degree);
            PACFArray[1] = ACFArray[1];
            for (int k = 2; k <= degree; k++)
            {
                double ch = 0;
                double zn = 0;
                for (int j = 1; j < k; j++)
                {
                    ch += PACFij(PACFArray, k - 1, j) * ACFArray[k - j];
                    zn += PACFij(PACFArray, k - 1, j) * ACFArray[j];
                }
                ch = ACFArray[k] - ch;
                zn = 1 - zn;
                PACFArray[k] = ch / zn;
            }
            return PACFArray;
        }
        static private double Cov(double[] x, double[] y)
        {
            if (x.Length != y.Length || DataConvertationClass.IsUndefinedElement(x) || DataConvertationClass.IsUndefinedElement(y))
            {
                return double.NaN;
            }
            double middX = Middle(x);
            double middY = Middle(y);
            double[] diffXY = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                diffXY[i] = (x[i] - middX) * (y[i] - middY);
            }
            return Middle(diffXY);
        }
        static public double[,] Covariation(params double[][] dataAr)
        {
            double[,] resCov = new double[1, 1];
            resCov[0, 0] = double.NaN;
            foreach (double[] el in dataAr)
            {
                if (el.Length != dataAr[0].Length || DataConvertationClass.IsUndefinedElement(el)) return resCov;
            }
            resCov = new double[dataAr.Length, dataAr.Length];
            for (int i = 0; i < dataAr.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    resCov[i, j] = Cov(dataAr[i], dataAr[j]);
                    resCov[j, i] = resCov[i, j];
                }
                resCov[i, i] = Cov(dataAr[i], dataAr[i]);
            }
            return resCov;
        }
        static public double[,] Correlation(params double[][] dataAr)
        {
            double[,] resCor = new double[1, 1];
            resCor[0, 0] = double.NaN;
            if (dataAr.Length < 2) return resCor;
            foreach (double[] el in dataAr)
            {
                if (el.Length != dataAr[0].Length || DataConvertationClass.IsUndefinedElement(el)) return resCor;
            }
            resCor = Covariation(dataAr);
            for (int i = 0; i < resCor.GetLength(0); i++)
            {
                double dsp1 = Dispersion(dataAr[i]);
                for (int j = 0; j < resCor.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        double dsp2 = Dispersion(dataAr[j]);
                        resCor[i, j] = resCor[i, j] / (Math.Sqrt(dsp1 * dsp2));
                    }
                    else { resCor[i, j] = 1; }
                }
            }
            return resCor;
        }
        static public double R2(double[] Y, double[] Ypr)
        {
            double res = Dispersion(Ypr) / Dispersion(Y);
            return res;
        }
        static public double SSE(double[] Y, double[] Ypr)
        {
            int inc = Y.Length - Ypr.Length;
            if (inc < 0) return double.NaN;
            double res = 0;
            for (int i = 0; i < Ypr.Length; i++)
            {
                res += Math.Pow(Y[i + inc] - Ypr[i], 2);
            }
            return res;
        }
        static public double SEofRegression(double[] Y, double[] Yrp)
        {
            double res = SSE(Y, Yrp);
            res = res / Yrp.Length;
            return Math.Sqrt(res);
        }
        static public double MAE(double[] Y, double[] Ypr)
        {
            int size = Y.Length >= Ypr.Length ? Ypr.Length : Y.Length;
            double res = 0;
            for (int i = 0; i < size; i++)
            {
                res += 100 * Math.Abs(Y[i] - Ypr[i]) / Y[i];
            }
            res = res / size;
            return res;
        }
        static public double DW(double[] Resid)
        {
            double chs = 0;
            for (int i = 1; i < Resid.Length; i++)
            {
                chs += Math.Pow(Resid[i] - Resid[i - 1], 2);
            }

            double znm = 0;
            foreach (double el in Resid)
            {
                znm += Math.Pow(el, 2);
            }

            return chs / znm;
        }

        static public double NormedDW(double[] Y, double[] Ypr)
        {
            var d = Y.Length - Ypr.Length;
            double[] resid = new double[Ypr.Length];
            for (int i_ = 0; i_ < resid.Length; i_++)
            {
                resid[i_] = Y[i_ + d] - Ypr[i_];
            }
            return Math.Exp(Math.Abs(2 - DW(resid)))-1; 
        }
        static public double NormedR2(double[] Y, double[] Ypr)
        {
            return Math.Exp(Math.Abs(1 - R2(Y, Ypr)))-1;
        }
        static public double NormedSSE(double[] Y, double[] Ypr)
        {
            var Y2 = 0.0;
            for (var i = 0; i < Y.Length; i++)
                Y2 = Y[i] * Y[i];
            Y2 = (Y2 == 0) ? 1 : Y2;
            return SSE(Y, Ypr)/Y2;
        }

        static public double IntCriteria(double[] Y, double[] Ypr)
        {
            return NormedR2(Y, Ypr) + NormedSSE(Y, Ypr) + NormedDW(Y, Ypr);
        }

        static public double F(double[] Y, double[] Ypr, int CntParams)
        {
            int N = Y.Length;
            double r2 = R2(Y, Ypr);
            double res = (r2 / (1 - r2)) * ((N - CntParams - 1) / CntParams);
            return res;
        }
        static public double JB(double[] Y, int n)
        {
            double res = double.NaN;
            if (n < 0) { return res; }
            double k = Kurtosis(Y);
            double s = Skewness(Y);
            res = (Math.Pow(s, 2) + 0.25 * Math.Pow(k - 3, 2));
            res = res * (Y.Length - n) / 6;
            return res;
        }
        static public double U(double[] Y, double[] Ypr)
        {
            double res = double.NaN;
            double ch = Math.Sqrt(SSE(Y, Ypr));
            double[] Zero = new double[Y.Length];
            double zn = Math.Sqrt(SSE(Y, Zero));
            Zero = new double[Ypr.Length];
            zn += Math.Sqrt(SSE(Ypr, Zero));

            res = ch / zn;
            return res;
        }
    }
}
