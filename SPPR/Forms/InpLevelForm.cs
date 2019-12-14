using System.Windows.Forms;

namespace DSS_Busol.Forms
{
    public partial class InpLevelForm : Form
    {

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button1;
        public InpLevelForm(double level)
        {
            InitializeComponent();
            label1 = new System.Windows.Forms.Label();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(numericUpDown1)).BeginInit();
            SuspendLayout();

            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 18);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(34, 13);
            label1.TabIndex = 0;
            label1.Text = "Поріг";

            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            numericUpDown1.Location = new System.Drawing.Point(52, 16);
            numericUpDown1.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(49, 20);
            numericUpDown1.TabIndex = 1;

            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            button1.Location = new System.Drawing.Point(15, 50);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(86, 23);
            button1.TabIndex = 2;
            button1.Text = "ОК";
            button1.UseVisualStyleBackColor = true;

            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(117, 85);
            Controls.Add(button1);
            Controls.Add(numericUpDown1);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "InpLevelForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(numericUpDown1)).EndInit();
            ResumeLayout(false);
            PerformLayout();
            if (level < 0 || level > 1) { level = 0.25; }
            numericUpDown1.Value = (decimal)level;
        }
        public double Level
        {
            get
            {
                return (double)numericUpDown1.Value;
            }
        }
    }
}
