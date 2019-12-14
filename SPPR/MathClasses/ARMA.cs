using DSS_Busol.AuxiliaryClasses;
using System;

namespace DSS_Busol.MathClasses
{
    public class ARMA
    {
        private string _paramsAR;
        private string _paramsMA;
        private bool _isAVG;
        private double[] dataAr;
        private Matrix MatrxH;
        private Matrix MatrxResid;
        private Matrix MatrxB;
        private string rowName;

        public ARMA()
        {
            Clear();
        }

        public void Clear()
        {
            _paramsAR = "";
            _paramsMA = "";
            _isAVG = false;
            dataAr = null;
            MatrxResid = null;
            MatrxB = null;
            MatrxH = null;
            rowName = null;
        }

        public string ParamsAR
        {
            get
            {
                return _paramsAR;
            }
            set
            {
                _paramsAR = value;
            }
        }

        public string ParamsMA
        {
            get
            {
                return _paramsMA;
            }
            set
            {
                _paramsMA = value;
            }
        }

        public bool IsAVG
        {
            get
            {
                return _isAVG;
            }
            set
            {
                _isAVG = value;
            }
        }

        public int P
        {
            get
            {
                int p = 0;
                if (!String.IsNullOrEmpty(_paramsAR))
                {
                    string[] tmp = _paramsAR.Split(';');
                    p = int.Parse(tmp[tmp.Length - 1]);
                }
                return p;
            }
        }

        public int Q
        {
            get
            {
                int q = 0;
                if (!String.IsNullOrEmpty(_paramsMA))
                {
                    string[] tmp = _paramsMA.Split(';');
                    q = int.Parse(tmp[tmp.Length - 1]);
                }
                return q;
            }
        }

        public string RowName
        {
            set
            {
                rowName = value;
            }
            get
            {
                return rowName;
            }
        }

        public double[] DataAr
        {
            set
            {
                dataAr = new double[value.Length];
                value.CopyTo(dataAr, 0);
            }
            get
            {
                return dataAr;
            }
        }

        public Matrix H
        {
            set
            {
                MatrxH = new Matrix(value.Array());
            }
            get
            {
                return MatrxH;
            }
        }

        public Matrix Resid
        {
            set
            {
                MatrxResid = new Matrix(value.Array());
            }
            get
            {
                return MatrxResid;
            }
        }

        public Matrix B
        {
            set
            {
                MatrxB = new Matrix(value.Array());
            }
            get
            {
                return MatrxB;
            }
        }

        public bool Exist
        {
            get
            {
                if (dataAr == null || MatrxB == null || MatrxH == null || rowName == null) { return false; }
                else return true;
            }
        }
    }
}
