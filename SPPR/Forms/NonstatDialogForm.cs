using System;
using System.Windows.Forms;

namespace DSS_Busol.Forms
{
    public partial class NonstatDialogForm : Form
    {

        private string _paramsAR;

        public NonstatDialogForm(int orderAR)
        {
            InitializeComponent();
            _paramsAR = "";
            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = orderAR;
        }


        public int CountParams
        {
            get
            {
                int count = 1;
                if (checkBox1.CheckState == CheckState.Checked) { count++; }
                if (checkBox2.CheckState == CheckState.Checked) { count++; }
                if (!String.IsNullOrEmpty(_paramsAR))
                {
                    string[] str = _paramsAR.Split(';');
                    count += str.Length;
                }
                return count;
            }
        }

        public bool IsAVG
        {
            get
            {
                if (checkBox1.CheckState == CheckState.Checked) return true;
                else return false;
            }
        }

        public bool IsTrend
        {
            get
            {
                if (checkBox2.CheckState == CheckState.Checked) return true;
                else return false;
            }
        }

        public string ParamsAR
        {
            get
            {
                return _paramsAR;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] nameList = CreatedListName();
            CheckedForm CheckWindow = new CheckedForm(nameList, "Виберіть параметри АР:");
            CheckWindow.Checking("All");
            DialogResult res = CheckWindow.ShowDialog();
            if (res != DialogResult.OK) return;
            int[] indArr = CheckWindow.CheckedList();
            _paramsAR = SetStringParamsAr(indArr);
        }

        private string[] CreatedListName()
        {
            int size = (int)numericUpDown1.Value;
            string[] names = new string[size];
            for (int i = 0; i < size; i++)
            {
                names[i] = "C" + (i + 1).ToString();
            }
            return names;
        }

        private string SetStringParamsAr(int number)
        {
            string res = "";
            for (int i = 0; i < number; i++)
            {
                res += (i + 1).ToString() + ";";
            }
            if (!String.IsNullOrEmpty(res)) { res = res.Remove(res.Length - 1, 1); }
            return res;
        }

        private string SetStringParamsAr(int[] arr)
        {
            string res = "";
            for (int i = 0; i < arr.Length; i++)
            {
                res += (arr[i] + 1).ToString() + ";";
            }
            if (!String.IsNullOrEmpty(res)) { res = res.Remove(res.Length - 1, 1); }
            return res;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int number = (int)numericUpDown1.Value;
            _paramsAR = SetStringParamsAr(number);
        }
    }
}
