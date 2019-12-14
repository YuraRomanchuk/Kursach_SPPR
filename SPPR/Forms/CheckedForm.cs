using System;
using System.Windows.Forms;

namespace DSS_Busol.Forms
{
    public partial class CheckedForm : Form
    {
        public CheckedForm(string[] NameList)
        {
            InitializeComponent();
            LoadList(NameList);
        }
        public CheckedForm(string[] NameList, string HeaderTable)
        {
            InitializeComponent();

            LoadList(NameList);
            label1.Text = HeaderTable;
        }

        private void LoadList(string[] rowNames)
        {
            foreach (string str in rowNames)
            {
                checkedListBox1.Items.Add(str);
            }
        }

        public string[] CheckedListNames()
        {
            string[] res = new string[checkedListBox1.CheckedItems.Count];
            int number = 0;
            foreach (object obj in checkedListBox1.CheckedItems)
            {
                res[number] = obj.ToString();
                number++;
            }
            return res;
        }

        public int[] CheckedList()
        {
            int[] res = new int[checkedListBox1.CheckedItems.Count];
            int count = 0;
            foreach (int index in checkedListBox1.CheckedIndices)
            {
                res[count] = index;
                count++;
            }
            return res;
        }

        public string HeaderTable
        {
            set
            {
                label1.Text = value;
            }
        }
        public void Checking(string CheckString)
        {
            try
            {
                CheckString = CheckString.ToLower();
                switch (CheckString)
                {
                    case "all":
                        for (int i = 0; i < checkedListBox1.Items.Count; i++)
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                        break;
                    case "none":
                        foreach (int indecCheck in checkedListBox1.CheckedIndices)
                        {
                            checkedListBox1.SetItemChecked(indecCheck, false);
                        }
                        break;
                    default:
                        string[] indx = CheckString.Split(';');
                        for (int i = 0; i < indx.Length; i++)
                        {
                            int indc = int.Parse(indx[i]) - 1;
                            checkedListBox1.SetItemChecked(indc, true);
                        }
                        break;
                }
            }
            catch (Exception inner)
            {
                throw new CheckedFormExceprtion("Error in CheckedForm.", inner);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Checking("All");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Checking("None");
        }
    }
}
