using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DSS_Busol.Forms
{
    public delegate void ChangedCurve();

    public partial class EditGraphForm : Form
    {
        private Button button1;
        private ComboBox comboBoxName;
        private Label label1;
        private GroupBox groupBox1;
        private TextBox textBox1;
        private Label label2;
        private Button bttnOk;
        private ColorDialog CurveColorDlg;
        private Button bttnColor;
        private Label label3;
        private Panel pnlColor;
        private ComboBox ComBoxSymType;
        private Label label4;
        private ComboBox ComBoxLine;
        private Label label5;
        public event ChangedCurve EventChangedCurve;

        private ZedGraph.CurveList _curves;
        public EditGraphForm(ZedGraph.CurveList curves)
        {
            InitializeComponent();
            button1 = new System.Windows.Forms.Button();
            comboBoxName = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            pnlColor = new System.Windows.Forms.Panel();
            bttnColor = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            bttnOk = new System.Windows.Forms.Button();
            CurveColorDlg = new System.Windows.Forms.ColorDialog();
            label4 = new System.Windows.Forms.Label();
            ComBoxSymType = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            ComBoxLine = new System.Windows.Forms.ComboBox();
            groupBox1.SuspendLayout();
            SuspendLayout();

            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            button1.Location = new System.Drawing.Point(112, 218);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Закрити";
            button1.UseVisualStyleBackColor = true;

            comboBoxName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxName.FormattingEnabled = true;
            comboBoxName.Location = new System.Drawing.Point(66, 15);
            comboBoxName.Name = "comboBoxName";
            comboBoxName.Size = new System.Drawing.Size(121, 21);
            comboBoxName.TabIndex = 1;
            comboBoxName.SelectedIndexChanged += new System.EventHandler(comboBoxName_SelectedIndexChanged);

            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label1.Location = new System.Drawing.Point(12, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(48, 17);
            label1.TabIndex = 2;
            label1.Text = "Крива";

            groupBox1.Controls.Add(ComBoxLine);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(ComBoxSymType);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(pnlColor);
            groupBox1.Controls.Add(bttnColor);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new System.Drawing.Point(8, 42);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(184, 170);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Параметри";

            pnlColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlColor.Location = new System.Drawing.Point(80, 58);
            pnlColor.Name = "pnlColor";
            pnlColor.Size = new System.Drawing.Size(56, 22);
            pnlColor.TabIndex = 4;

            bttnColor.Location = new System.Drawing.Point(150, 58);
            bttnColor.Name = "bttnColor";
            bttnColor.Size = new System.Drawing.Size(28, 23);
            bttnColor.TabIndex = 3;
            bttnColor.Text = "...";
            bttnColor.UseVisualStyleBackColor = true;
            bttnColor.Click += new System.EventHandler(bttnColor_Click);

            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 63);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(34, 13);
            label3.TabIndex = 2;
            label3.Text = "Колір";

            textBox1.Location = new System.Drawing.Point(58, 22);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(121, 20);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);

            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 25);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(26, 13);
            label2.TabIndex = 0;
            label2.Text = "Ім\'я";

            bttnOk.Enabled = false;
            bttnOk.Location = new System.Drawing.Point(15, 218);
            bttnOk.Name = "bttnOk";
            bttnOk.Size = new System.Drawing.Size(75, 23);
            bttnOk.TabIndex = 4;
            bttnOk.Text = "Зберегти";
            bttnOk.UseVisualStyleBackColor = true;
            bttnOk.Click += new System.EventHandler(bttnOk_Click);

            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 99);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(68, 13);
            label4.TabIndex = 5;
            label4.Text = "Стиль точки";

            ComBoxSymType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ComBoxSymType.FormattingEnabled = true;
            ComBoxSymType.Items.AddRange(new object[] {
            "None",
            "Default",
            "VDash",
            "HDash",
            "TriangleDown",
            "Star",
            "Plus",
            "XCross",
            "Circle",
            "Triangle",
            "Diamond",
            "Square"});
            ComBoxSymType.Location = new System.Drawing.Point(80, 96);
            ComBoxSymType.Name = "ComBoxSymType";
            ComBoxSymType.Size = new System.Drawing.Size(98, 21);
            ComBoxSymType.TabIndex = 6;
            ComBoxSymType.SelectedIndexChanged += new System.EventHandler(ComBoxSymType_SelectedIndexChanged);

            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 135);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(63, 13);
            label5.TabIndex = 7;
            label5.Text = "Стиль линії";

            ComBoxLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ComBoxLine.FormattingEnabled = true;
            ComBoxLine.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "Dot",
            "DashDot"});
            ComBoxLine.Location = new System.Drawing.Point(80, 132);
            ComBoxLine.Name = "ComBoxLine";
            ComBoxLine.Size = new System.Drawing.Size(98, 21);
            ComBoxLine.TabIndex = 8;
            ComBoxLine.SelectedIndexChanged += new System.EventHandler(ComBoxLine_SelectedIndexChanged);

            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(204, 248);
            Controls.Add(bttnOk);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(comboBoxName);
            Controls.Add(button1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            Name = "EditGraphForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Load += new System.EventHandler(EditGraphForm_Load);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
            _curves = curves;
            LoadList(0);
        }


        private void LoadList(int SelectIndex)
        {
            comboBoxName.Items.Clear();
            foreach (ZedGraph.CurveItem item in _curves)
            {
                comboBoxName.Items.Add(item.Label.Text);
            }
            if (SelectIndex >= comboBoxName.Items.Count) return;
            comboBoxName.SelectedIndex = SelectIndex;
        }

        private void EditGraphForm_Load(object sender, EventArgs e)
        {
        }

        private void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = _curves[comboBoxName.SelectedIndex].Label.Text;
            pnlColor.BackColor = _curves[comboBoxName.SelectedIndex].Color;
            ZedGraph.LineItem curve = (ZedGraph.LineItem)_curves[comboBoxName.SelectedIndex];
            ComBoxSymType.Text = curve.Symbol.Type.ToString();
            if (curve.Line.Style != DashStyle.DashDotDot && curve.Line.Style != DashStyle.Custom)
            { ComBoxLine.Text = curve.Line.Style.ToString(); }

            bttnOk.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bttnOk.Enabled = true;
        }

        private void bttnOk_Click(object sender, EventArgs e)
        {
            int ind = comboBoxName.SelectedIndex;
            ZedGraph.LineItem curve = (ZedGraph.LineItem)_curves[ind];
            curve.Label.Text = textBox1.Text;
            curve.Color = pnlColor.BackColor;
            ZedGraph.SymbolType SmType = (ZedGraph.SymbolType)Enum.Parse(typeof(ZedGraph.SymbolType), ComBoxSymType.Items[ComBoxSymType.SelectedIndex].ToString());
            curve.Symbol = new ZedGraph.Symbol(SmType, pnlColor.BackColor);
            DashStyle Dash = (DashStyle)Enum.Parse(typeof(DashStyle), ComBoxLine.Items[ComBoxLine.SelectedIndex].ToString());
            curve.Line.Style = Dash;

            bttnOk.Enabled = false;
            EventChangedCurve();
            LoadList(ind);
        }

        private void bttnColor_Click(object sender, EventArgs e)
        {
            if (CurveColorDlg.ShowDialog() != DialogResult.OK) return;
            pnlColor.BackColor = CurveColorDlg.Color;

            bttnOk.Enabled = true;
        }

        private void ComBoxSymType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bttnOk.Enabled = true;
        }

        private void ComBoxLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            bttnOk.Enabled = true;
        }

    }
}
