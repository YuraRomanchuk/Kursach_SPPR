using System.Windows.Forms;

namespace DSS_Busol.Forms
{
    public partial class InpDegreeForm : Form
    {

        private Label label1;
        private NumericUpDown numericUpDown1;
        private Button button1;
        public InpDegreeForm(int maxDegree)
        {
            InitializeComponent();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)(numericUpDown1)).BeginInit();
            SuspendLayout();
  
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 21);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(87, 13);
            label1.TabIndex = 0;
            label1.Text = "Порядок ЧАКФ";

            numericUpDown1.Location = new System.Drawing.Point(117, 19);
            numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(76, 20);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});

            button1.DialogResult = DialogResult.OK;
            button1.Location = new System.Drawing.Point(15, 49);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(178, 23);
            button1.TabIndex = 2;
            button1.Text = "ОК";
            button1.UseVisualStyleBackColor = true;
    
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(212, 83);
            Controls.Add(button1);
            Controls.Add(numericUpDown1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "InpDegreeForm";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(numericUpDown1)).EndInit();
            ResumeLayout(false);
            PerformLayout();
            numericUpDown1.Maximum = maxDegree;
        }
 
        public int Degree
        {
            get
            {
                return (int)numericUpDown1.Value;
            }
        }
    }
}
