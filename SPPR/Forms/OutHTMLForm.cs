using System;
using System.Windows.Forms;

namespace DSS_Busol.Forms
{
    public partial class OutHTMLForm : Form
    {
        public OutHTMLForm()
        {
            InitializeComponent();
        }

        public OutHTMLForm(String HTMLText)
        {
            webBrowser1.DocumentText = HTMLText;
        }


        public String HTMLText
        {
            get
            {
                return webBrowser1.DocumentText;
            }
            set
            {
                webBrowser1.DocumentText = value;
            }
        }
    }
}
