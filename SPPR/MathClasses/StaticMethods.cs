using DSS_Busol.MathClasses;
using System;

namespace DSS_Busol.AuxiliaryClasses
{
    public enum Methods { LSM = 0, RLSM = 1 };

    public static class StaticMethods
    {

        public static Matrix MatrixForNonstatTest(double[] dataAr, bool IsAVG, bool IsTrend, string ParamsAR)
        {
            int CountClumns = 1;
            if (IsAVG) CountClumns++;
            if (IsTrend) CountClumns++;

            int CountRows;
            int startDataAr;
            string[] prmAR;
            double[] diffDataAr;
            if (!String.IsNullOrEmpty(ParamsAR))
            {
                prmAR = ParamsAR.Split(';');
                CountClumns = CountClumns + prmAR.Length;
                CountRows = dataAr.Length - 1 - int.Parse(prmAR[prmAR.Length - 1]);
                startDataAr = 1 + int.Parse(prmAR[prmAR.Length - 1]);
                double[] tmp = new double[dataAr.Length];
                dataAr.CopyTo(tmp, 0);
                DataConvertationClass.DifferRow(ref tmp, 1);
                diffDataAr = new double[tmp.Length - 1];
                Array.Copy(tmp, 1, diffDataAr, 0, diffDataAr.Length);
            }
            else
            {
                prmAR = new string[0];
                diffDataAr = new double[0];
                CountRows = dataAr.Length - 1;
                startDataAr = 1;
            }

            Matrix H = new Matrix(CountRows, CountClumns);
            int nowClmn = 0;
            if (IsAVG)
            {
                for (int i = 0; i < H.CountRows; i++) { H[i, 0] = 1; }
                nowClmn++;
            }
            if (IsTrend)
            {
                for (int i = 0; i < H.CountRows; i++)
                {
                    H[i, nowClmn] = startDataAr + i + 1;
                }
                nowClmn++;
            }
            for (int i = 0; i < H.CountRows; i++)
            {
                H[i, nowClmn] = dataAr[startDataAr + i - 1];
            }
            nowClmn++;
            if (!String.IsNullOrEmpty(ParamsAR))
            {
                for (int k = 0; k < prmAR.Length; k++)
                {
                    int p = int.Parse(prmAR[k]);
                    for (int i = 0; i < H.CountRows; i++)
                    {
                        H[i, nowClmn + k] = diffDataAr[i + p];
                    }
                }
            }

            return H;
        }

        public static double NormVec(Matrix Mtrx)
        {
            double res = 0;
            for (int i = 0; i < Mtrx.CountRows; i++)
            {
                res += Mtrx[i, 0] * Mtrx[i, 0];
            }
            return Math.Sqrt(res);
        }

        static public Matrix LSM(Matrix H, Matrix Y)
        {
            Matrix temp = Matrix.Transpose(H);
            temp = Matrix.Increase(temp, H);
            temp = Matrix.Inverse(temp);
            temp = Matrix.Increase(temp, Matrix.Transpose(H));
            Matrix B = Matrix.Increase(temp, Y);
            return B;
        }

        static public Matrix RLSM(Matrix H, Matrix Y, double beta)
        {
            Matrix B = new Matrix(H.CountColumns, 1);
            Matrix P = Matrix.DiagonalMatrix(H.CountColumns, beta);
            Matrix Row;
            for (int i = 0; i < H.CountRows; i++)
            {
                Row = new Matrix(H.RowArray(i));
                Row = Matrix.Transpose(Row);
                P = PFunc(P, Row);
                B = BFunc(P, Row, B, Y[i, 0]);
            }
            return B;
        }

        static private Matrix PFunc(Matrix P, Matrix RowH)
        {
            Matrix chs = Matrix.Transpose(RowH);
            chs = Matrix.Increase(P, chs);
            chs = Matrix.Increase(chs, RowH);
            chs = Matrix.Increase(chs, P);

            Matrix znm = Matrix.Transpose(RowH);
            znm = Matrix.Increase(P, znm);
            znm = Matrix.Increase(RowH, znm);
            znm[0, 0] = znm[0, 0] + 1;

            Matrix Res = Matrix.IncrConst(1 / znm[0, 0], chs);
            Res = Matrix.Diff(P, Res);
            return Res;
        }

        static private Matrix BFunc(Matrix P, Matrix RowH, Matrix B, double dimY)
        {
            Matrix temp = Matrix.Increase(RowH, B);
            temp[0, 0] = dimY - temp[0, 0];
            temp = Matrix.Increase(temp, RowH);
            temp = Matrix.Transpose(temp);
            temp = Matrix.Increase(P, temp);
            B = Matrix.Add(B, temp);
            return B;
        }
    }
}
