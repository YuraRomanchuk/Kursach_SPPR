using DSS_Busol.AuxiliaryClasses;
using DSS_Busol.MathClasses;
using System;
using System.Windows.Forms;

namespace DSS_Busol.Forms
{
    public partial class ForecastForm : Form
    {
        private double[,] ForecastAr;
        private ARMA ARMA;

        public ForecastForm(ARMA ARMA)
        {
            InitializeComponent();
            this.ARMA = ARMA;
            StatProgWindow();
        }

        private void ForecastForm_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
        }

        private void StatProgWindow()
        {
            numericUpDown1.Maximum = ARMA.DataAr.Length;
            if (numericUpDown1.Value > numericUpDown1.Maximum) numericUpDown1.Value = numericUpDown1.Maximum;
            numericUpDown1.Minimum = ARMA.P;
            numericUpDown2.Maximum = ARMA.DataAr.Length;
            if (numericUpDown2.Value > numericUpDown2.Maximum) numericUpDown2.Value = numericUpDown2.Maximum;
            numericUpDown2.Minimum = ARMA.P;
        }

        private void DunProgWindow()
        {
            numericUpDown1.Maximum = ARMA.DataAr.Length + 300;
            numericUpDown1.Minimum = ARMA.P;
            numericUpDown2.Maximum = ARMA.DataAr.Length + 300;
            numericUpDown2.Minimum = ARMA.P;
        }

        private void radioButton1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            StatProgWindow();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            DunProgWindow();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OutDataAr();
            if (radioButton1.Checked) { StatForecast(); }
            else { DunForecast(); }
            OutForecast();
        }

        private void OutDataAr()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.RowCount = ARMA.DataAr.Length;
            for (int i = 0; i < ARMA.DataAr.Length; i++)
            {
                dataGridView1[0, i].Value = i;
                dataGridView1[1, i].Value = ARMA.DataAr[i];
            }
        }

        private void OutForecast()
        {
            double SKP = 0, SPP = 0, ASPP = 0;
            Vector yprog = new Vector(ForecastAr.GetLength(0));
            Vector yreal = new Vector(yprog.n);
            int shift = 0;
            for (int i = 0; i < ForecastAr.GetLength(0); i++)
            {
                int index = (int)ForecastAr[i, 0];
                if (index >= dataGridView1.RowCount)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, index].Value = index;
                    dataGridView1[1, index].Value = double.NaN;
                    shift++;
                }
                dataGridView1[2, index].Value = ForecastAr[i, 1];
                yprog[i] = ForecastAr[i, 1];
                yreal[i] = Convert.ToDouble(dataGridView1[1, index].Value);
                yreal.OutVector("Forecast.txt");
            }
            int s = yreal.n;
            for (int i = 0; i < yreal.n - shift; i++)
            {
                SKP += yreal[i] - yprog[i];
                if (Math.Abs(yreal[i]) > Constants.EPS)
                {
                    SPP += (yreal[i] - yprog[i]) / yreal[i];
                    ASPP += (yreal[i] - yprog[i]) / yreal[i];
                }
                else
                {
                    s--;
                }


            }
            if (s != 0)
            {
                SKP = Math.Sqrt(SKP * SKP / s);
                SPP = 100 * (SPP / s);
                ASPP = 100 * Math.Abs(ASPP) / s;
            }
            textBox1.Text = "";
            textBox1.Text += "СКП: " + SKP.ToString() + "\r\n";
            textBox1.Text += "СПП: " + SPP.ToString() + "%\r\n";
            textBox1.Text += "АСПП: " + ASPP.ToString() + "%\r\n";
        }

        private void StatForecast()
        {
            int s = (int)numericUpDown1.Value;
            int f = (int)numericUpDown2.Value;
            int size = f >= s ? (f - s + 1) : 0;
            this.ForecastAr = new double[size, 2];
            int count = 0;
            for (int i = s; i <= f; i++)
            {
                this.ForecastAr[count, 0] = i;
                this.ForecastAr[count, 1] = SPFunc(i);
                count++;
            }
        }

        private double SPFunc(int index)
        {
            double res = 0;
            int inc = 0;
            if (ARMA.IsAVG) { res = ARMA.B[0, 0]; inc = 1; }
            int inb = ARMA.P;
            string[] strAR = ARMA.ParamsAR.Split(';');
            for (int i = 0; i < strAR.Length; i++)
            {
                int p = int.Parse(strAR[i]);
                res = res + ARMA.DataAr[index - p] * ARMA.B[i + inc, 0];
            }
            return res;
        }

        private void DunForecast()
        {
            int s = (int)numericUpDown1.Value;
            int f = (int)numericUpDown2.Value;
            int size = f >= s ? (f - s + 1) : 0;
            this.ForecastAr = new double[size, 2];
            int count = 0;
            for (int i = s; i <= f; i++)
            {
                this.ForecastAr[count, 0] = i;
                this.ForecastAr[count, 1] = DPFunc(i);
                count++;
            }
        }

        private double DPFunc(int index)
        {
            double res = 0;
            int inc = 0;
            int s = (int)numericUpDown1.Value;
            if (ARMA.IsAVG) { res = ARMA.B[0, 0]; inc = 1; }
            int inb = ARMA.P;
            string[] strAR = ARMA.ParamsAR.Split(';');
            for (int i = 0; i < strAR.Length; i++)
            {
                int p = int.Parse(strAR[i]);
                if ((index - p) < s)
                { res = res + ARMA.DataAr[index - p] * ARMA.B[i + inc, 0]; }
                else { res = res + this.ForecastAr[index - p - s, 1] * ARMA.B[i + inc, 0]; }
            }
            return res;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[] dataP = new double[ForecastAr.GetLength(0)];
            double[] indexP = new double[ForecastAr.GetLength(0)];
            for (int i = 0; i < ForecastAr.GetLength(0); i++)
            {
                indexP[i] = ForecastAr[i, 0];
                dataP[i] = ForecastAr[i, 1];
            }
            GraphForm GraphRowWindow = new GraphForm();
            GraphRowWindow.Add(ARMA.RowName, ARMA.DataAr);
            GraphRowWindow.Add("Прогноз", dataP, indexP);
            GraphRowWindow.SetRanColor();
            GraphRowWindow.Draw();
            GraphRowWindow.Show();
        }
    }
}
