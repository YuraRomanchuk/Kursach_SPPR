using System.Windows.Forms;

namespace DSS_Busol.AuxiliaryClasses
{
    public class Matrix
    {
        private double[,] matrix;
        public Matrix(int countRows, int countColumns)
        {
            matrix = new double[countRows, countColumns];
        }
        public Matrix(double[] matrix)
        {
            this.matrix = new double[matrix.Length, 1];
            for (int i = 0; i < matrix.Length; i++)
            {
                this.matrix[i, 0] = matrix[i];
            }
        }
        public Matrix(double[,] matrix)
        {
            this.matrix = matrix;
        }
        public double this[int i, int j]
        {
            get
            {
                return matrix[i, j];
            }
            set
            {
                matrix[i, j] = value;
            }
        }
        static public Matrix Transpose(Matrix matr)
        {
            double[,] TAr = new double[matr.CountColumns, matr.CountRows];
            for (ushort i = 0; i < TAr.GetLength(0); i++)
            {
                for (ushort j = 0; j < TAr.GetLength(1); j++)
                {
                    TAr[i, j] = matr[j, i];
                }
            }

            Matrix res = new Matrix(TAr);
            return res;
        }
        public double[,] Array()
        {
            return matrix;
        }
        public double[] ColumnArray(int indexColumn)
        {
            double[] columnAr = new double[this.CountRows];
            for (int i = 0; i < this.CountRows; i++)
            {
                columnAr[i] = matrix[i, indexColumn];
            }
            return columnAr;
        }
        public double[] RowArray(int indexRow)
        {
            double[] rowAr = new double[this.CountColumns];
            for (int i = 0; i < this.CountColumns; i++)
            {
                rowAr[i] = matrix[indexRow, i];
            }
            return rowAr;
        }
        public int CountRows
        {
            get
            {
                return matrix.GetLength(0);
            }
        }
        public int CountColumns
        {
            get
            {
                return matrix.GetLength(1);
            }
        }
        public Matrix Clone(int Si, int Fi, int Sj, int Fj)
        {
            Matrix Res = new Matrix(Fi - Si + 1, Fj - Sj + 1);
            for (int i = Si; i <= Fi; i++)
            {
                for (int j = Sj; j <= Fj; j++)
                {
                    Res[i - Si, j - Sj] = this[i, j];
                }
            }
            return Res;
        }
        static public Matrix Inverse(Matrix matr)
        {
            if (matr.CountRows != matr.CountColumns)
            {
                MessageBox.Show("Не можливо знайти обернену, матриця не квадратна !");
                return null;
            }

            Matrix res = new Matrix(Method_Haleckogo(matr.Array()));
            return res;
        }
        static private double[,] Method_Haleckogo(double[,] matrixA)
        {
            int n = matrixA.GetLength(0);
            double[,] matrixC = new double[n, n];
            double[,] matrixB = new double[n, n];

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (i < j)
                    {
                        double temp = 0;
                        for (int l = 0; l < i; l++)
                        {
                            temp += matrixC[i, l] * matrixB[l, j];
                        }

                        matrixB[i, j] = (matrixA[i, j] - temp) / matrixC[i, i];
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    if (i >= j)
                    {
                        double temp = 0;
                        for (int l = 0; l < i; l++)
                        {
                            temp += matrixC[i, l] * matrixB[l, j];
                        }

                        matrixC[i, j] = matrixA[i, j] - temp;
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                matrixB[i, i] = 1;
            }

            double[,] inverseMatrix = new double[n, n];
            int flag = n - 1;

            while (flag != -1)
            {
                for (int l = flag; l > -1; l--)
                {
                    if (flag >= l)
                    {
                        if (l == flag)
                        {
                            double temp = 0;
                            for (int i = l + 1; i < n; i++)
                            {
                                temp += matrixC[i, l] * inverseMatrix[flag, i];
                            }

                            inverseMatrix[flag, l] = (1 - temp) / matrixC[l, l];
                        }
                        else
                        {
                            double temp = 0;
                            for (int i = l + 1; i < n; i++)
                            {
                                temp -= matrixC[i, l] * inverseMatrix[flag, i];
                            }

                            inverseMatrix[flag, l] = temp / matrixC[l, l];
                        }
                    }
                }

                for (int k = flag; k > -1; k--)
                {
                    if (k < flag)
                    {
                        double temp = 0;
                        for (int i = k + 1; i < n; i++)
                        {
                            temp -= matrixB[k, i] * inverseMatrix[i, flag];
                        }

                        inverseMatrix[k, flag] = temp;
                    }
                }

                flag--;
            }

            return inverseMatrix;
        }
        static public Matrix Increase(Matrix A, Matrix B)
        {
            if (A.CountColumns != B.CountRows)
            {
                MessageBox.Show("Неможливо перемножити матриці, не збігаються розмірності !");
                return null;
            }
            Matrix res = new Matrix((ushort)A.CountRows, (ushort)B.CountColumns);
            for (ushort i = 0; i < res.CountRows; i++)
            {
                for (ushort j = 0; j < res.CountColumns; j++)
                {
                    for (ushort k = 0; k < A.CountColumns; k++)
                    {
                        res[i, j] = res[i, j] + A[i, k] * B[k, j];
                    }
                }
            }
            return res;
        }
        static public Matrix Diff(Matrix A, Matrix B)
        {
            if (A.CountColumns != B.CountColumns || A.CountRows != B.CountRows)
            {
                MessageBox.Show("Не можливо здійснити віднімання матриць, розмірності не збігаються !");
                return null;
            }

            Matrix res = new Matrix((ushort)A.CountRows, (ushort)A.CountColumns);
            for (ushort i = 0; i < res.CountRows; i++)
            {
                for (ushort j = 0; j < res.CountColumns; j++)
                {
                    res[i, j] = A[i, j] - B[i, j];
                }
            }
            return res;
        }
        static public Matrix Add(Matrix A, Matrix B)
        {
            if (A.CountColumns != B.CountColumns || A.CountRows != B.CountRows)
            {
                MessageBox.Show("Не можливо здійснити додавання матриць, розмірності не збігаються !");
                return null;
            }

            for (int i = 0; i < A.CountRows; i++)
            {
                for (int j = 0; j < A.CountColumns; j++)
                {
                    A[i, j] = A[i, j] + B[i, j];
                }
            }
            return A;
        }
        static public Matrix DiagonalMatrix(int size, double diagEl)
        {
            Matrix Res = new Matrix(size, size);
            for (int i = 0; i < Res.CountColumns; i++)
            {
                Res[i, i] = diagEl;
            }
            return Res;
        }
        static public Matrix IncrConst(double c, Matrix A)
        {
            for (int i = 0; i < A.CountRows; i++)
            {
                for (int j = 0; j < A.CountColumns; j++)
                {
                    A[i, j] = c * A[i, j];
                }
            }
            return A;
        }
    }
}
