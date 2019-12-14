using System;
using System.Collections.Generic;
using System.IO;

namespace DSS_Busol.AuxiliaryClasses
{
    public class Vector : ICloneable
    {
        public int n;
        public double[] a;
        public Vector()
        {
            n = 0;
        }
        public Vector(int N)
        {
            n = N;
            a = new double[n];
            for (int i = 0; i < n; i++) a[i] = 0;
        }
        public Vector(int N, double[] A)
        {
            n = N;
            a = new double[n];
            for (int i = 0; i < n; i++) a[i] = A[i];
        }
        public Vector(double[] A)
        {
            n = A.Length;
            a = new double[n];
            for (int i = 0; i < n; i++) a[i] = A[i];
        }
        public Vector(int N, string s)
        {
            n = N;
            a = new double[n];
            int j = 0;
            int i_0 = 0;
            string temp = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    temp = "";
                    for (int k = i_0; k < i; k++) temp += s[k];
                    i_0 = i + 1;
                    a[j] = Convert.ToDouble(temp);
                    j++;
                }
            }
            temp = "";
            for (int k = i_0; k < s.Length; k++) temp += s[k];
            a[j] = Convert.ToDouble(temp);
        }
        public double Delta(int i) => (i == 0) ? 0 : (a[i] - a[i - 1]);
        public Vector(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                var s = "";
                var tempList = new List<double>();
                do
                {
                    tempList.Add(Convert.ToDouble(sr.ReadLine()));
                } while (!sr.EndOfStream);
                n = tempList.Count;
                a = tempList.ToArray();
            }
        }
        public Vector(int N, double rand_radius)
        {
            n = N;
            a = new double[n];
            Random rnd = new Random();
            bool stop = false;
            do
            {
                for (int i = 0; i < n; i++) a[i] = 2 * rnd.NextDouble() - 1;
                stop = Norm() < 1;
            } while (!stop);
            double s = Norm();
            for (int i = 0; i < n; i++) a[i] = a[i] / s;
        }
        public object Clone()
        {
            Vector temp = new Vector(n, a);
            return temp;
        }
        public bool Equals(Vector V)
        {
            bool b = true;
            b = b && (V.n == n);
            for (int i = 0; i < n; i++) b = b && (V.a[i] == a[i]);
            return b;
        }
        public override bool Equals(object obj)
        {
            return (obj is Vector) && Equals((Vector)obj);
        }
        public override int GetHashCode()
        {
            return n.GetHashCode() ^ a.GetHashCode();
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < n; i++)
            {
                s += a[i].ToString();
                if (i != n - 1) s += " ";
            }
            return s;
        }
        public void OutVector(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(n.ToString());
                sw.WriteLine(ToString());
            }
        }
        public double this[int i]
        {
            get
            {
                return a[i];
            }
            set
            {
                a[i] = value;
            }
        }
        public static bool operator ==(Vector v1, Vector v2)
        {
            return Equals(v1, v2);
        }
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !Equals(v1, v2);
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector v = new Vector(v1.n, v1.a);
            for (int i = 0; i < v1.n; i++) v[i] += v2[i];
            return v;
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector v = new Vector(v1.n, v1.a);
            for (int i = 0; i < v1.n; i++) v[i] -= v2[i];
            return v;
        }
        public static Vector operator *(double k, Vector v2)
        {
            Vector v = new Vector(v2.n, v2.a);
            for (int i = 0; i < v2.n; i++) v[i] *= k;
            return v;
        }

        public static double operator *(Vector v1, Vector v2)
        {
            double s = 0;
            for (int i = 0; i < v1.n; i++) s += v1[i] * v2[i];
            return s;
        }
        public double Norm()
        {
            return Math.Sqrt(this * this);
        }
        public void ToZero()
        {
            for (int i = 0; i < n; i++) a[i] = 0;
        }
        public Vector Norming()
        {
            double s = Norm();
            if (s != 0) for (int i = 0; i < n; i++) a[i] = a[i] / s;
            return this;
        }
    }
}
