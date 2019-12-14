using DSS_Busol.MathClasses;
using System.Drawing;
using System.Windows.Forms;

namespace DSS_Busol.Forms
{
    public partial class StatisticalAnalysisForm : Form
    {
        private System.Windows.Forms.RichTextBox richTextBox;
        public StatisticalAnalysisForm()
        {
            InitializeComponent();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();

            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox.Location = new System.Drawing.Point(1, 1);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(297, 206);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.WordWrap = false;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 209);
            this.Controls.Add(this.richTextBox);
            this.Name = "StatisticalAnalisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Статистичні характеристики";
            this.ResumeLayout(false);

        }
        public void WriteStatistical(double[] dataAr, string rowName)
        {
            if (DataConvertationClass.IsUndefinedElement(dataAr))
            {
                return;
            }

            double mid = Statistical.Middle(dataAr);
            double disp = Statistical.Dispersion(dataAr);
            double skewn = Statistical.Skewness(dataAr);
            double kurt = Statistical.Kurtosis(dataAr);
            double jb = Statistical.JB(dataAr, 0);

            richTextBox.SelectionFont = new Font("Arial", 12, FontStyle.Bold);
            richTextBox.SelectedText = "\nСтатистичні характеристики.";
            richTextBox.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox.SelectionFont = new Font("Arial", 12, FontStyle.Bold);
            richTextBox.SelectedText = "\nРяд \"" + rowName + "\".";
            richTextBox.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox.SelectedText = "\n";
            richTextBox.SelectionFont = new Font("Arial", 10);
            richTextBox.SelectionAlignment = HorizontalAlignment.Left;
            richTextBox.SelectionIndent = 15;
            richTextBox.SelectedText = "\nСереднє\t\t\t" + mid.ToString("F4") + "\v";
            richTextBox.SelectedText = "\nДисперсія\t\t\t" + disp.ToString("F4") + "\v";
            richTextBox.SelectedText = "\nКоофіцієнт асиметрії\t\t" + skewn.ToString("F4") + "\v";
            richTextBox.SelectedText = "\nЕксцес\t\t\t" + kurt.ToString("F4") + "\v";
            richTextBox.SelectedText = "\nЖака-Бера\t\t\t" + jb.ToString("F4") + "\v";
        }

        public void WritePrmQuality(double[] Y, double[] Ypr, int CntParams)
        {
            int d = Y.Length - Ypr.Length;
            if (d < 0) { throw new StatisticalAnalisFormException(); }
            double[] resid = new double[Ypr.Length];
            double[] tmp = new double[Ypr.Length];
            for (int i = 0; i < resid.Length; i++)
            {
                resid[i] = Y[i + d] - Ypr[i];
                tmp[i] = Y[i + d];
            }
            Y = tmp;

            double dw = Statistical.DW(resid);
            double Rq = Statistical.R2(Y, Ypr);
            double mse = Statistical.SEofRegression(Y, Ypr);
            double f = Statistical.F(Y, Ypr, CntParams);
            double u = Statistical.U(Y, Ypr);

            richTextBox.SelectionFont = new Font("Arial", 12, FontStyle.Bold);
            richTextBox.SelectedText = "\nПараметри якості моделі.";
            richTextBox.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox.SelectedText = "\n";
            richTextBox.SelectionFont = new Font("Arial", 10);
            richTextBox.SelectionAlignment = HorizontalAlignment.Left;
            richTextBox.SelectionIndent = 15;
            richTextBox.SelectedText = "\nКооф. детермінації\t\t" + Rq.ToString("F4") + "\v";
            richTextBox.SelectedText = "\nСКП\t\t\t\t" + mse.ToString("F4") + "\v";
            richTextBox.SelectedText = "\nDW\t\t\t\t" + dw.ToString("F4") + "\v";
            richTextBox.SelectedText = "\nF-статистика\t\t\t" + f.ToString("F4") + "\v";
            richTextBox.SelectedText = "\nКр. Тейла\t\t\t" + u.ToString("F4") + "\v";
        }

    }
}
