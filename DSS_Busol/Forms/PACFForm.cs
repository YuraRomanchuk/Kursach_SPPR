using DSS_Busol.MathClasses;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DSS_Busol.Forms
{
    public partial class PACFForm : Form
    {
        private MenuStrip MenuStrip1;
        private Panel panel1;
        private Panel panel2;
        private DataGridView dataGridView1;
        private SplitContainer splitContainer1;
        private ZedGraph.ZedGraphControl ACzedGraphControl;
        private ZedGraph.ZedGraphControl PACzedGraphControl;
        private ToolStripMenuItem SetDegreeToolStripMenuItem;
        private ToolStripMenuItem levelToolStripMenuItem;
        private double[] _dataAr;
        private int _degree;
        private double level;
        public PACFForm(string rowName, double[] dataAr) : this(rowName, dataAr, 0)
        {
        }
        public PACFForm(string rowName, double[] dataAr, int degree)
        {
            InitializeComponent();
            MenuStrip1 = new System.Windows.Forms.MenuStrip();
            panel1 = new System.Windows.Forms.Panel();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            ACzedGraphControl = new ZedGraph.ZedGraphControl();
            PACzedGraphControl = new ZedGraph.ZedGraphControl();
            panel2 = new System.Windows.Forms.Panel();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            SetDegreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MenuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).BeginInit();
            SuspendLayout();

            MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            SetDegreeToolStripMenuItem,
            levelToolStripMenuItem});
            MenuStrip1.Location = new System.Drawing.Point(0, 0);
            MenuStrip1.Name = "MenuStrip1";
            MenuStrip1.Size = new System.Drawing.Size(554, 24);
            MenuStrip1.TabIndex = 0;
            MenuStrip1.Text = "menuStrip1";

            panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            panel1.Controls.Add(splitContainer1);
            panel1.Location = new System.Drawing.Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(554, 283);
            panel1.TabIndex = 1;

            splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            splitContainer1.Location = new System.Drawing.Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;

            splitContainer1.Panel1.Controls.Add(ACzedGraphControl);

            splitContainer1.Panel2.Controls.Add(PACzedGraphControl);
            splitContainer1.Size = new System.Drawing.Size(548, 277);
            splitContainer1.SplitterDistance = 118;
            splitContainer1.TabIndex = 0;
 
            ACzedGraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            ACzedGraphControl.Location = new System.Drawing.Point(3, 0);
            ACzedGraphControl.Name = "ACzedGraphControl";
            ACzedGraphControl.ScrollGrace = 0;
            ACzedGraphControl.ScrollMaxX = 0;
            ACzedGraphControl.ScrollMaxY = 0;
            ACzedGraphControl.ScrollMaxY2 = 0;
            ACzedGraphControl.ScrollMinX = 0;
            ACzedGraphControl.ScrollMinY = 0;
            ACzedGraphControl.ScrollMinY2 = 0;
            ACzedGraphControl.Size = new System.Drawing.Size(542, 115);
            ACzedGraphControl.TabIndex = 0;

            PACzedGraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            PACzedGraphControl.Location = new System.Drawing.Point(3, 3);
            PACzedGraphControl.Name = "PACzedGraphControl";
            PACzedGraphControl.ScrollGrace = 0;
            PACzedGraphControl.ScrollMaxX = 0;
            PACzedGraphControl.ScrollMaxY = 0;
            PACzedGraphControl.ScrollMaxY2 = 0;
            PACzedGraphControl.ScrollMinX = 0;
            PACzedGraphControl.ScrollMinY = 0;
            PACzedGraphControl.ScrollMinY2 = 0;
            PACzedGraphControl.Size = new System.Drawing.Size(542, 149);
            PACzedGraphControl.TabIndex = 0;

            panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            panel2.Controls.Add(dataGridView1);
            panel2.Location = new System.Drawing.Point(0, 313);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(554, 89);
            panel2.TabIndex = 2;
 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            dataGridView1.Size = new System.Drawing.Size(548, 80);
            dataGridView1.TabIndex = 0;

            SetDegreeToolStripMenuItem.Name = "SetDegreeToolStripMenuItem";
            SetDegreeToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            SetDegreeToolStripMenuItem.Text = "Порядок АКФ";
            SetDegreeToolStripMenuItem.Click += new System.EventHandler(SetDegreeToolStripMenuItem_Click);
 
            levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            levelToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            levelToolStripMenuItem.Text = "Поріг";
            levelToolStripMenuItem.Click += new System.EventHandler(levelToolStripMenuItem_Click);

            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(554, 405);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(MenuStrip1);
            MainMenuStrip = MenuStrip1;
            Name = "PACFForm";
            Text = "ЧАКФ та АКФ";
            MenuStrip1.ResumeLayout(false);
            MenuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            ResumeLayout(false);
            PerformLayout();
            Text += ". Ряд \"" + rowName + "\"";
            _dataAr = dataAr;
            if (degree < 0) _degree = 0;
            else _degree = degree;
            level = 0.2;
            Draw();
        }
        private void Draw()
        {
            if (DataConvertationClass.IsUndefinedElement(_dataAr))
            {
                MessageBox.Show(Text + ". Існують не визначені елементи!");
                return;
            }
            dataGridView1.ColumnCount = _degree + 2;
            dataGridView1.RowCount = 2;
            dataGridView1[0, 0].Value = "Автокореляції";
            dataGridView1[0, 1].Value = "ЧАКФ";
            double[] ACArray = Statistical.ACF(_dataAr, _degree);
            double[] PACArray = Statistical.PACF(_dataAr, _degree);
            double[] XList = new double[_degree + 1];
            for (int i = 1; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Name = (i - 1).ToString();
                dataGridView1[i, 0].Value = ACArray[i - 1].ToString("F6");
                if (Math.Abs(ACArray[i - 1]) >= level) { dataGridView1[i, 0].Style.ForeColor = Color.Red; }
                else dataGridView1[i, 0].Style.ForeColor = Color.Black;
                dataGridView1[i, 1].Value = PACArray[i - 1].ToString("F6");
                if (Math.Abs(PACArray[i - 1]) >= level) { dataGridView1[i, 1].Style.ForeColor = Color.Red; }
                else dataGridView1[i, 1].Style.ForeColor = Color.Black;
                XList[i - 1] = i - 1;
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            ZedGraph.LineObj levelLine1 = new ZedGraph.LineObj(Color.Red, -1, level, _degree + 1, level);
            levelLine1.Line.Width = 2;
            ZedGraph.LineObj levelLine2 = new ZedGraph.LineObj(Color.Red, -1, -level, _degree + 1, -level);
            levelLine2.Line.Width = 2;

            ZedGraph.GraphPane ACpane = ACzedGraphControl.GraphPane;
            ACpane.CurveList.Clear();
            ACpane.GraphObjList.Clear();
            ACpane.Title.Text = "Автокореляція";
            ACpane.XAxis.Title.IsVisible = false;
            ACpane.XAxis.MajorGrid.IsVisible = true;
            ACpane.XAxis.MajorGrid.DashOn = 1;
            ACpane.XAxis.MajorGrid.DashOff = 1;
            ACpane.XAxis.Scale.Min = 0;
            ACpane.XAxis.Scale.Max = ACArray.Length;
            ACpane.YAxis.Title.IsVisible = false;
            ACpane.YAxis.MajorGrid.IsVisible = true;
            ACpane.YAxis.MajorGrid.DashOn = 1;
            ACpane.YAxis.MajorGrid.DashOff = 1;
            ACpane.YAxis.Scale.Min = -2;
            ACpane.YAxis.Scale.Max = 2;
            ZedGraph.PointPairList ACList = new ZedGraph.PointPairList(XList, ACArray);
            ACpane.AddBar("", ACList, Color.BlanchedAlmond);
            ACpane.GraphObjList.Add(levelLine1);
            ACpane.GraphObjList.Add(levelLine2);
            ACzedGraphControl.Refresh();

            ZedGraph.GraphPane PACpane = PACzedGraphControl.GraphPane;
            PACpane.CurveList.Clear();
            PACpane.GraphObjList.Clear();
            PACpane.Title.Text = "ЧАКФ";
            PACpane.XAxis.Title.IsVisible = false;
            PACpane.XAxis.MajorGrid.IsVisible = true;
            PACpane.XAxis.MajorGrid.DashOn = 1;
            PACpane.XAxis.MajorGrid.DashOff = 1;
            PACpane.XAxis.Scale.Min = 0;
            PACpane.XAxis.Scale.Max = ACArray.Length;
            PACpane.YAxis.Title.IsVisible = false;
            PACpane.YAxis.MajorGrid.IsVisible = true;
            PACpane.YAxis.MajorGrid.DashOn = 1;
            PACpane.YAxis.MajorGrid.DashOff = 1;
            PACpane.YAxis.Scale.Min = -2;
            PACpane.YAxis.Scale.Max = 2;
            ZedGraph.PointPairList PACList = new ZedGraph.PointPairList(XList, PACArray);
            PACpane.AddBar("", PACList, Color.BlanchedAlmond);
            PACpane.GraphObjList.Add(levelLine1);
            PACpane.GraphObjList.Add(levelLine2);
            PACzedGraphControl.Refresh();
        }

        private void SetDegreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InpDegreeForm InpDegreeWindow = new InpDegreeForm(_dataAr.Length - 1);
            DialogResult res = InpDegreeWindow.ShowDialog();
            if (res != DialogResult.OK) return;
            _degree = InpDegreeWindow.Degree;
            Draw();
        }

        private void levelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InpLevelForm InpLevelWindow = new InpLevelForm(level);
            DialogResult res = InpLevelWindow.ShowDialog();
            if (res != DialogResult.OK) return;
            level = InpLevelWindow.Level;
            Draw();
        }

    }
}
