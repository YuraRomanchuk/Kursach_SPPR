using System;
using System.Drawing;
using System.Windows.Forms;

namespace DSS_Busol.Forms
{
    public partial class GraphForm : Form
    {
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem graphToolStripMenuItem;
        public GraphForm()
        {
            InitializeComponent();
            zedGraphControl1 = new ZedGraph.ZedGraphControl();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();

            zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            zedGraphControl1.Location = new System.Drawing.Point(1, 27);
            zedGraphControl1.Name = "zedGraphControl1";
            zedGraphControl1.ScrollGrace = 0;
            zedGraphControl1.ScrollMaxX = 0;
            zedGraphControl1.ScrollMaxY = 0;
            zedGraphControl1.ScrollMaxY2 = 0;
            zedGraphControl1.ScrollMinX = 0;
            zedGraphControl1.ScrollMinY = 0;
            zedGraphControl1.ScrollMinY2 = 0;
            zedGraphControl1.Size = new System.Drawing.Size(476, 348);
            zedGraphControl1.TabIndex = 0;
  
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            editToolStripMenuItem});
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(477, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";

            editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            graphToolStripMenuItem});
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            editToolStripMenuItem.Text = "Налаштування";

            graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            graphToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            graphToolStripMenuItem.Text = "Криві";
            graphToolStripMenuItem.Click += new System.EventHandler(graphToolStripMenuItem_Click);

            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(477, 376);
            Controls.Add(zedGraphControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "GraphRowForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Графік";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
            ZedGraph.GraphPane pane = zedGraphControl1.GraphPane;
            pane.YAxis.Title.Text = "Значення ряду";
            pane.XAxis.Title.Text = "Виміри";
            pane.Title.IsVisible = false;
        }

        public void Add(string name, double[] dataAr)
        {
            double[] indexAr = new double[dataAr.Length];
            for (int i = 0; i < indexAr.Length; i++) { indexAr[i] = i; }
            Add(name, dataAr, indexAr);
        }

        public void Add(string name, double[] dataAr, double[] indexAr)
        {
            ZedGraph.GraphPane pane = zedGraphControl1.GraphPane;
            pane.AddCurve(name, indexAr, dataAr, Color.Black, ZedGraph.SymbolType.None);
        }

        public void Draw()
        {
            SetRanColor();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void ChangGraph()
        {
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        public void SetRanColor()
        {
            ZedGraph.GraphPane pane = zedGraphControl1.GraphPane;
            int Red = 0;
            int Green = 0;
            int Blue = 0;
            for (int i = 0; i < pane.CurveList.Count; i++)
            {
                pane.CurveList[i].Color = Color.FromArgb(Red, Green, Blue);
                ChangeColor(ref Red, ref Green, ref Blue);
            }
        }

        private void ChangeColor(ref int Red, ref int Green, ref int Blue)
        {
            Red += 57;
            if (Red > 255) Red = Red - 255;
            Green += 172;
            if (Green > 255) Green = Green - 255;
            Blue += 93;
            if (Blue > 255) Blue = Blue - 255;
            if (Red == 255 & Green == 255 & Blue == 255)
            {
                Red -= 57;
                Green -= 33;
                Blue -= 25;
            }
        }

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditGraphForm EditGraphWinwod = new EditGraphForm(zedGraphControl1.GraphPane.CurveList);
            EditGraphWinwod.EventChangedCurve += new ChangedCurve(ChangGraph);
            EditGraphWinwod.ShowDialog();
        }
    }
}
