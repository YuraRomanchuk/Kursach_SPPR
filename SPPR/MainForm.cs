using DSS_Busol.AuxiliaryClasses;
using DSS_Busol.Forms;
using DSS_Busol.MathClasses;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DSS_Busol
{
    public partial class MainForm : Form
    {
        public Vector InputRow;
        public string InputRowName;
        public Color darkColor, mediumColor, lightColor;
        #region Row
        public Panel rowPanel;
        public DataGridView rowTable;
        public ToolStrip rowMenuStrip;
        public ToolStripDropDownButton  editRowButton, analysisRowButton;
        public ToolStripMenuItem normRowItem, normRowItem1, normRowItem2,
            logRowItem, logRowItem1, logRowItem2, graphRowItem, statRowItem, autocorRowItem, stationarRowItem;
        public Methods metod;
        #endregion
        #region Model
        public Panel modelPanel;
        public Label choseModelTitle, choseRangeTitle, choseCriteriaTitle, choseOptimizationMethodTitle, extraTitle;
        public Label arTitle, maTitle;
        public Label arFromTitle, arToTitle, maFromTitle, maToTitle;
        public TextBox arFromText, arToText, maFromText, maToText;
        public ComboBox criteriaNames;
        public ComboBox optMethods;
        public CheckBox armaA0;
        public Label emptyTitle;
        public Button startButton;
        //   public Label 
        #endregion
        #region Parameters
        public Panel parametersPanel;
        public Label parametersPanelTitle;
        public DataGridView parametersTable;
        public Button forecastButton;
        #endregion
        #region ResultRow
        public Panel resultRowPanel;
        public Panel resultRowTablePanel;
        public Label resultRowPanelTitle;
        public DataGridView resultRowTable;
        public ToolStrip resultRowMenuStrip;
        public ToolStripDropDownButton analysisResultRowButton;
        public ToolStripButton saveResultRowButton;
        public ToolStripMenuItem graphResultRowItem, correlationResultRowItem;
        #endregion
        public string _paramsAR, _paramsMA;
        public int optAR;
        public int optMA;
        public double DW, SSE, RQ, INT, NDW, NSSE, NRQ;
        public ARMA ARMA;
        public TextBox textPanel;
        public Vector OutputRow;
        public Vector OutputParameters;
        public Matrix Parameters;
        public bool ifLinear;
        public int model;
        public MainForm()
        {
            InitializeComponent();
            components = new System.ComponentModel.Container();
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Text = "SPPR for bugs classification and clasterization in regression testing";
            MaximizeBox = true;
            darkColor = Color.Black;
            mediumColor = Color.White;
            lightColor = Color.White;
            Size = new Size(1100, 600);
            ClientSize = new Size(1125, 600);
            AutoSize = true;
            AutoScroll = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Load += new EventHandler(_basicFormLoad);
            #region Row
            rowPanel = new Panel()
            {
                Location = new Point(0, 0),
                Size = new Size(200, 400),
                BackColor = mediumColor,
                BorderStyle = BorderStyle.Fixed3D
            };
            #region Menu
            rowMenuStrip = new ToolStrip()
            {
                Location = new Point(25, 25),
                Size = new Size(150, 25),
                TabIndex = 0,
                Text = "rowMenuStrip",
                Name = "rowMenuStrip",
                BackColor = lightColor,
                ForeColor = darkColor

            };
            editRowButton = new ToolStripDropDownButton()
            {
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Image = Image.FromFile("editImage.png"),
                Name = "editButton",
                Size = new Size(25, 25),
                Text = "Обробка",
                Enabled = true
            };
           

            Controls.Add(rowPanel);
            #endregion
            rowTable = new DataGridView()
            {
                Location = new Point(0, rowMenuStrip.Bottom),
                BackgroundColor = lightColor,
                ColumnCount = 2,
                ReadOnly = true,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowHeadersVisible = false,
                ColumnHeadersVisible = true,
                ColumnHeadersHeight = 25,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                Enabled = false,

            };
            rowTable.EnableHeadersVisualStyles = false;
            rowTable.ColumnHeadersDefaultCellStyle.ForeColor = lightColor;
            rowTable.ColumnHeadersDefaultCellStyle.BackColor = darkColor;
            rowTable.DefaultCellStyle.ForeColor = darkColor;
            rowTable.DefaultCellStyle.BackColor = lightColor;
            rowTable.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            rowTable.Columns[0].Width = 50;
            rowTable.Columns[1].Width = 100;
            rowTable.Columns[0].HeaderText = "n";
            rowTable.Columns[1].HeaderText = "Y(n)";
            #endregion
            #region Model
            modelPanel = new Panel()
            {
                Location = new Point(rowPanel.Right, 0),
                Size = new Size(ClientSize.Width - 3 * rowPanel.Width, rowPanel.Height),
                BackColor = mediumColor,
                BorderStyle = BorderStyle.Fixed3D
            };
            Controls.Add(modelPanel);
            #endregion
            #region Parameters
            parametersPanel = new Panel()
            {
                Location = new Point(modelPanel.Right, 0),
                Size = new Size(rowPanel.Width+50, rowPanel.Height),
                BackColor = mediumColor,
                BorderStyle = BorderStyle.Fixed3D
            };
            Controls.Add(parametersPanel);
            #endregion
            #region Result Row
            resultRowPanel = new Panel()
            {
                Location = new Point(parametersPanel.Right, 0),
                Size = new Size(rowPanel.Width+50, rowPanel.Height),
                BackColor = mediumColor,
                BorderStyle = BorderStyle.Fixed3D
            };
            Controls.Add(resultRowPanel);
            #endregion
            #region Text Panel
            textPanel = new TextBox()
            {
                ReadOnly = true,
                Multiline = true,
                Location = new Point(0, rowPanel.Bottom),
                Size = new Size(ClientSize.Width, ClientSize.Height - rowPanel.Bottom),
                BackColor = Color.White,
                ForeColor = Color.Black,
                ScrollBars = ScrollBars.Vertical,
                TextAlign = HorizontalAlignment.Center,
                Font = new Font(new FontFamily("Times New Roman"), 12)
            };
            Controls.Add(textPanel);
            _nonlinearRow();
            #endregion Text Panel
        }

        private void _loadRowClick(object sender, EventArgs e)
        {
            var openFDialog = new OpenFileDialog()
            {
                Filter = "Text files (*.txt) | *.txt"
            };
            DialogResult dialRes = openFDialog.ShowDialog();
            if (dialRes != DialogResult.OK) return;
            if (dialRes != DialogResult.OK) return;
            try
            {
                InputRow = new Vector(openFDialog.FileName);
                textPanel.Text += "Завантажено ряд" + "\r\n";
                textPanel.Text += "Кількість елементів: " + InputRow.n.ToString() + "\r\n";
                if (_nonlinearityTest())
                {
                    ifLinear = false;
                    _nonlinearRow();
                    textPanel.Text += "Ряд є нелінійним\r\n";
                }
                else
                {
                    ifLinear = true;
                    _linearRow();
                    textPanel.Text += "Ряд є лінійним\r\n";
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Помилка при завантаженні даних з файлу. Невірний формат даних.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            rowTable.Enabled = true;
            editRowButton.Enabled = true;
            button2.Enabled = true;
            RefreshRowDataGridView();
        }
        private void _saveRowClick(object sender, EventArgs e)
        {
            SaveFileDialog savFileDial = new SaveFileDialog();
            DialogResult res;
            savFileDial.FileName = InputRowName + ".txt";
            savFileDial.Filter = "Text files (*.txt) | *.txt";
            res = savFileDial.ShowDialog();
            if (res != DialogResult.OK) return;
            string nameFile = savFileDial.FileName;
            bool isTXT = nameFile.EndsWith(".txt");
            if (!isTXT) nameFile += ".txt";
            FileStream fStream = new FileStream(nameFile, FileMode.Create, FileAccess.Write);
            StreamWriter strmWrite = new StreamWriter(fStream);
            for (var i = 0; i < InputRow.n - 1; i++) strmWrite.WriteLine(InputRow[i]);
            strmWrite.Write(InputRow[InputRow.n - 1]);
            strmWrite.Close();
            fStream.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _loadRowClick(sender,e);
        }

        private void _saveResultClick(object sender, EventArgs e)
        {
            SaveFileDialog savFileDial = new SaveFileDialog();
            DialogResult res;
            savFileDial.FileName = InputRowName + ".txt";
            savFileDial.Filter = "Text files (*.txt) | *.txt";
            res = savFileDial.ShowDialog();
            if (res != DialogResult.OK) return;
            string nameFile = savFileDial.FileName;
            bool isTXT = nameFile.EndsWith(".txt");
            if (!isTXT) nameFile += ".txt";
            FileStream fStream = new FileStream(nameFile, FileMode.Create, FileAccess.Write);
            StreamWriter strmWrite = new StreamWriter(fStream);
            for (var i = 0; i < OutputRow.n - 1; i++) strmWrite.WriteLine(OutputRow[i]);
            strmWrite.Write(OutputRow[OutputRow.n - 1]);
            strmWrite.Close();
            fStream.Close();
        }
        private void _norm1RowClick(object sender, EventArgs e)
        {
            var res = new double[InputRow.n];
            for (var i = 0; i < res.Length; i++) res[i] = InputRow[i];
            if (!DataConvertationClass.NormalizationRow(ref res, 0))
            {
                MessageBox.Show("Неможливо провести нормування");
            }
            else
            {
                InputRow = new Vector(res.Length, res);
                InputRowName += "_normed";
                RefreshRowDataGridView();
                textPanel.Text += "Було проведення нормування ряду на відрізок [0,1] \r\n";
            }
        }
        private void _norm2RowClick(object sender, EventArgs e)
        {
            var res = new double[InputRow.n];
            for (var i = 0; i < res.Length; i++) res[i] = InputRow[i];
            if (!DataConvertationClass.NormalizationRow(ref res, -1))
            {
                MessageBox.Show("Неможливо провести нормування");
            }
            else
            {
                InputRow = new Vector(res.Length, res);
                InputRowName += "_normed";
                RefreshRowDataGridView();
                textPanel.Text += "Було проведення нормування ряду на відрізок [-1,1] \r\n";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _statRowClick(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _autocorRowClick(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _graphRowClick(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _stationarRowClick(sender, e);
        }

        private void _log1RowClick(object sender, EventArgs e)
        {
            var res = new double[InputRow.n];
            for (var i = 0; i < res.Length; i++) res[i] = InputRow[i];
            if (!DataConvertationClass.LnRow(ref res))
            {
                MessageBox.Show("Неможливо провести логарифмування");
            }
            else
            {
                InputRow = new Vector(res.Length, res);
                InputRowName += "_lned";
                RefreshRowDataGridView();
                textPanel.Text += "Було проведення натуральне логарифмування ряду \r\n";
            }
        }
        private void _log2RowClick(object sender, EventArgs e)
        {
            var res = new double[InputRow.n];
            for (var i = 0; i < res.Length; i++) res[i] = InputRow[i];
            if (!DataConvertationClass.LogRow(ref res, 10))
            {
                MessageBox.Show("Неможливо провести логарифмування");
            }
            else
            {
                InputRow = new Vector(res.Length, res);
                InputRowName += "_loged";
                RefreshRowDataGridView();
                textPanel.Text += "Було проведення десяткове логарифмування ряду \r\n";
            }
        }
        private void _graphRowClick(object sender, EventArgs e)
        {
            var GraphRowWindow = new GraphForm();
            GraphRowWindow.Add(InputRowName, InputRow.a);
            GraphRowWindow.SetRanColor();
            GraphRowWindow.Draw();
            GraphRowWindow.Show();
        }
        private void _graphResultRowClick(object sender, EventArgs e)
        {
            var GraphRowWindow = new GraphForm();
            GraphRowWindow.Add(InputRowName + "*", OutputRow.a);
            GraphRowWindow.SetRanColor();
            GraphRowWindow.Draw();
            GraphRowWindow.Show();
        }
        private void _statRowClick(object sender, EventArgs e)
        {
            var StatisticalWindows = new StatisticalAnalysisForm();
            StatisticalWindows.WriteStatistical(InputRow.a, InputRowName);
            StatisticalWindows.Show();
        }
        private void _autocorRowClick(object sender, EventArgs e)
        {
            InpDegreeForm InpDegreeWindow = new InpDegreeForm(InputRow.n);
            DialogResult res = InpDegreeWindow.ShowDialog();
            if (res != DialogResult.OK) return;
            PACFForm PACFWindows = new PACFForm(InputRowName, InputRow.a, InpDegreeWindow.Degree);
            PACFWindows.Show();
        }
        private void _stationarRowClick(object sender, EventArgs e)
        {
            int max = (int)(0.1 * InputRow.a.Length);
            NonstatDialogForm window = new NonstatDialogForm(max);
            DialogResult res = window.ShowDialog();
            if (res != DialogResult.OK) return;
            Matrix H = StaticMethods.MatrixForNonstatTest(InputRow.a, window.IsAVG, window.IsTrend, window.ParamsAR);
            double[] tmp = new double[InputRow.a.Length];
            InputRow.a.CopyTo(tmp, 0);
            DataConvertationClass.DifferRow(ref tmp, 1);
            double[] diffDataAr = new double[H.CountRows];
            int start = InputRow.a.Length - H.CountRows;
            Array.Copy(tmp, start, diffDataAr, 0, diffDataAr.Length);
            Matrix dY = new Matrix(diffDataAr);
            Matrix B = StaticMethods.LSM(H, dY);
            Matrix Resid = Matrix.Increase(H, B);
            Resid = Matrix.Diff(Resid, dY);
            double disp = Math.Pow(StaticMethods.NormVec(Resid), 2);
            disp = disp / (H.CountRows - H.CountColumns);
            Matrix SE = Matrix.Transpose(H);
            SE = Matrix.Increase(SE, H);
            SE = Matrix.Inverse(SE);
            int ind = 0;
            if (window.IsAVG) ind++;
            if (window.IsTrend) ind++;
            double Bcrt = B[ind, 0] / Math.Sqrt(SE[ind, ind] * disp);
            if (!String.IsNullOrEmpty(window.ParamsAR)) ind++;
            OutHTMLForm ResWindow = new OutHTMLForm();
            ResWindow.HTMLText = CreateHTMLTable(Bcrt, InputRowName, ind);
            ResWindow.Show();
        }

        private void WriteLine(string text)
        {
            textPanel.Text += text + "\r\n";
        }

        private void _startARClick(object sender, EventArgs e)
        {
            try
            {
                model = 0;
                optAR = 1; optMA = 0;
                if (optMethods.SelectedIndex == 0) metod = Methods.LSM;
                else metod = Methods.RLSM;
                for (int i = 1; i < 2; i++)
                    for (int j = 0; j < 1; j++)
                    {
                        _paramsAR = "";
                        _paramsMA = "";
                        {
                            for (int k = 1; k <= i; k++)
                            {
                                _paramsAR += k.ToString();
                                if (k != i) _paramsAR += ";";
                            }
                            if (j == 0)
                            {
                                _paramsMA = "";
                            }
                            else
                            {
                                for (int k = 1; k <= j; k++)
                                {
                                    _paramsMA += k.ToString();
                                    if (k != j) _paramsMA += ";";
                                }
                            }
                        }
                        ARMA = new ARMA();
                        ARMA.ParamsAR = _paramsAR;
                        ARMA.ParamsMA = _paramsMA;
                        ARMA.RowName = InputRowName;
                        ARMA.IsAVG = armaA0.Checked;
                        ARMA.DataAr = InputRow.a;
                        if (ARMA.P == 0 & ARMA.Q == 0) return;
                        ARMA.H = CreateHMatrix();
                        Matrix Y = CreateYMatrix(ARMA.DataAr.Length - ARMA.P);
                        if (metod == Methods.LSM) { ARMA.B = StaticMethods.LSM(ARMA.H, Y); }
                        else { ARMA.B = StaticMethods.RLSM(ARMA.H, Y, 10); }
                        Matrix HB = Matrix.Increase(ARMA.H, ARMA.B);
                        ARMA.Resid = Matrix.Diff(Y, HB);
                        if (ARMA.Q != 0)
                        {
                            ARMA.H = CreateHMatrix(ARMA.Resid);
                            Y = CreateYMatrix(ARMA.H.CountRows);
                            ARMA.Resid = ARMA.Resid.Clone(ARMA.Resid.CountRows - Y.CountRows, ARMA.Resid.CountRows - 1, 0, 0);
                            //Y = Matrix.Diff(Y, ARMA.Resid);
                            ARMA.B = StaticMethods.LSM(ARMA.H, Y);
                        }
                        double[] Y_ = new double[ARMA.DataAr.Length];
                        double[] Ypr = new double[ModY().Length];
                        for (int l = 0; l < ARMA.DataAr.Length; l++) Y_[l] = ARMA.DataAr[l];
                        for (int l = 0; l < ModY().Length; l++) Ypr[l] = ModY()[l];
                        int CntParams = ARMA.P + ARMA.Q;
                        int d = Y_.Length - Ypr.Length;
                        if (d < 0) { throw new StatisticalAnalisFormException(); }
                        double[] resid = new double[Ypr.Length];
                        double[] tmp = new double[Ypr.Length];
                        for (int i_ = 0; i_ < resid.Length; i_++)
                        {
                            resid[i_] = Y_[i_ + d] - Ypr[i_];
                            tmp[i_] = Y_[i_ + d];
                        }
                        Y_ = tmp;
                        double dw = Statistical.DW(resid);
                        double Rq = Statistical.R2(Y_, Ypr);
                        double sse = Statistical.SEofRegression(Y_, Ypr);
                        double intC = Statistical.IntCriteria(Y_, Ypr);
                        if (i == 1 && j == 0)
                        {
                            DW = dw;
                            RQ = Rq;
                            SSE = sse;
                            INT = intC;
                        }
                        optAR = i;
                        optMA = j;
                        SSE = sse;
                        RQ = Rq;
                        DW = dw;
                        INT = intC;
                        OutputRow = new Vector(ModY());
                        Parameters = ARMA.B;
                    }
                WriteLine("SSE: " + SSE.ToString());
                WriteLine("DW: " + DW.ToString());
                WriteLine("RQ: " + RQ.ToString());
                WriteLine("Int: " + INT.ToString());
                OutputParameters = new Vector(Parameters.CountRows);
                for (var i = 0; i < OutputParameters.n; i++) OutputParameters[i] = Parameters[i, 0];
                _resultPanelShow();
                _parametersPanelShow();
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректные введенные данные");
            }
        }
        private void _startARMAClick(object sender, EventArgs e)
        {
            try
            {
                model = 1;
                int i0 = Convert.ToInt16(arFromText.Text);
                int i1 = Convert.ToInt16(arToText.Text);
                int j0 = Convert.ToInt16(maFromText.Text);
                int j1 = Convert.ToInt16(maToText.Text);
                optAR = i0; optMA = i1;
                if (optMethods.SelectedIndex == 0) metod = Methods.LSM;
                else metod = Methods.RLSM;
                for (int i = i0; i < i1; i++)
                    for (int j = j0; j < j1; j++)
                    {
                        _paramsAR = "";
                        _paramsMA = "";
                        {
                            for (int k = 1; k <= i; k++)
                            {
                                _paramsAR += k.ToString();
                                if (k != i) _paramsAR += ";";
                            }
                            if (j == 0)
                            {
                                _paramsMA = "";
                            }
                            else
                            {
                                for (int k = 1; k <= j; k++)
                                {
                                    _paramsMA += k.ToString();
                                    if (k != j) _paramsMA += ";";
                                }
                            }
                        }
                        ARMA = new ARMA();
                        ARMA.ParamsAR = _paramsAR;
                        ARMA.ParamsMA = _paramsMA;
                        ARMA.RowName = InputRowName;
                        ARMA.IsAVG = armaA0.Checked;
                        ARMA.DataAr = InputRow.a;
                        if (ARMA.P == 0 & ARMA.Q == 0) return;
                        ARMA.H = CreateHMatrix();
                        Matrix Y = CreateYMatrix(ARMA.DataAr.Length - ARMA.P);
                        if (metod == Methods.LSM) { ARMA.B = StaticMethods.LSM(ARMA.H, Y); }
                        else { ARMA.B = StaticMethods.RLSM(ARMA.H, Y, 10); }
                        Matrix HB = Matrix.Increase(ARMA.H, ARMA.B);
                        ARMA.Resid = Matrix.Diff(Y, HB);
                        if (ARMA.Q != 0)
                        {
                            ARMA.H = CreateHMatrix(ARMA.Resid);
                            Y = CreateYMatrix(ARMA.H.CountRows);
                            ARMA.Resid = ARMA.Resid.Clone(ARMA.Resid.CountRows - Y.CountRows, ARMA.Resid.CountRows - 1, 0, 0);
                            //Y = Matrix.Diff(Y, ARMA.Resid);
                            ARMA.B = StaticMethods.LSM(ARMA.H, Y);
                        }
                        double[] Y_ = new double[ARMA.DataAr.Length];
                        double[] Ypr = new double[ModY().Length];
                        for (int l = 0; l < ARMA.DataAr.Length; l++) Y_[l] = ARMA.DataAr[l];
                        for (int l = 0; l < ModY().Length; l++) Ypr[l] = ModY()[l];
                        int CntParams = ARMA.P + ARMA.Q;
                        int d = Y_.Length - Ypr.Length;
                        if (d < 0) { throw new StatisticalAnalisFormException(); }
                        double[] resid = new double[Ypr.Length];
                        double[] tmp = new double[Ypr.Length];
                        for (int i_ = 0; i_ < resid.Length; i_++)
                        {
                            resid[i_] = Y_[i_ + d] - Ypr[i_];
                            tmp[i_] = Y_[i_ + d];
                        }
                        Y_ = tmp;
                        double ndw = Statistical.NormedDW(Y_, Ypr);
                        double nRq = Statistical.NormedR2(Y_, Ypr);
                        double nsse = Statistical.NormedSSE(Y_, Ypr);
                        double intC = Statistical.IntCriteria(Y_, Ypr);
                        double dw = Statistical.DW(resid);
                        double Rq = Statistical.R2(Y_, Ypr);
                        double sse = Statistical.SSE(Y_, Ypr);
                        if (i == i0 && j == j0)
                        {
                            DW = dw;
                            RQ = Rq;
                            SSE = sse;
                            NDW = ndw;
                            NRQ = nRq;
                            NSSE = nsse;
                            INT = intC;
                        }
                        switch (criteriaNames.SelectedIndex)
                        {
                            case 0: //SSE
                                {
                                    if (NSSE > nsse)
                                    {
                                        optAR = i;
                                        optMA = j;
                                        DW = dw;
                                        RQ = Rq;
                                        SSE = sse;
                                        NSSE = nsse;
                                        NRQ = nRq;
                                        NDW = ndw;
                                        INT = intC;
                                        OutputRow = new Vector(ModY());
                                        Parameters = ARMA.B;
                                    }
                                }
                                break;
                            case 1: //DW
                                {
                                    if (NDW > ndw)
                                    {
                                        optAR = i;
                                        optMA = j;
                                        DW = dw;
                                        RQ = Rq;
                                        SSE = sse;
                                        NSSE = nsse;
                                        NRQ = nRq;
                                        NDW = ndw;
                                        INT = intC;
                                        OutputRow = new Vector(ModY());
                                        Parameters = ARMA.B;
                                    }

                                }
                                break;
                            case 2: //RQ
                                {
                                    if (NRQ>nRq)
                                    {
                                        optAR = i;
                                        optMA = j;
                                        DW = dw;
                                        RQ = Rq;
                                        SSE = sse;
                                        NSSE = nsse;
                                        NRQ = nRq;
                                        NDW = ndw;
                                        INT = intC;
                                        OutputRow = new Vector(ModY());
                                        Parameters = ARMA.B;
                                    }
                                }
                                break;
                            case 3: //INT
                                {
                                    if (INT > intC)
                                    {
                                        optAR = i;
                                        optMA = j;
                                        DW = dw;
                                        RQ = Rq;
                                        SSE = sse;
                                        NSSE = nsse;
                                        NRQ = nRq;
                                        NDW = ndw;
                                        INT = intC;
                                        OutputRow = new Vector(ModY());
                                        Parameters = ARMA.B;
                                    }
                                }
                                break;
                        }
                    }

                string temp = "";
                switch (criteriaNames.SelectedIndex)
                {
                    case 0: //SSE
                        {
                            temp = "SSE";
                        }
                        break;
                    case 1: //DW
                        {
                            temp = "DW";
                        }
                        break;
                    case 2: //RQ
                        {
                            temp = "RQ";
                        }
                        break;
                    case 3: //INT
                        {
                            temp = "INT";
                        }
                        break;
                }
                WriteLine("Найкраща " + temp + " при");
                WriteLine("Авторегресія: " + optAR.ToString());
                WriteLine("Ковзне середнє: " + optMA.ToString());
                WriteLine("SSE: " + SSE.ToString());
                WriteLine("DW: " + DW.ToString());
                WriteLine("RQ: " + RQ.ToString());
                WriteLine("Int: " + INT.ToString());
                OutputParameters = new Vector(Parameters.CountRows);
                for (var i = 0; i < OutputParameters.n; i++) OutputParameters[i] = Parameters[i, 0];
                _parametersPanelShow();
                _resultPanelShow();
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректные введенные данные");
            }
        }

        private void _startLogisticARClick(object sender, EventArgs e)
        {
            try
            {
                model = 2;
                int i0 = Convert.ToInt16(arFromText.Text);
                int i1 = Convert.ToInt16(arToText.Text);
                optAR = i0; optMA = i1;
                if (optMethods.SelectedIndex == 0) metod = Methods.LSM;
                else metod = Methods.RLSM;
                for (int i = i0; i < i1; i++)
                    for (int j = 1; j < 2; j++)
                    {
                        _paramsAR = "";
                        _paramsMA = "";
                        {
                            for (int k = 1; k <= i; k++)
                            {
                                _paramsAR += k.ToString();
                                if (k != i) _paramsAR += ";";
                            }
                            if (j == 0)
                            {
                                _paramsMA = "";
                            }
                            else
                            {
                                for (int k = 1; k <= j; k++)
                                {
                                    _paramsMA += k.ToString();
                                    if (k != j) _paramsMA += ";";
                                }
                            }
                        }
                        ARMA = new ARMA();
                        ARMA.ParamsAR = _paramsAR;
                        ARMA.ParamsMA = _paramsMA;
                        ARMA.RowName = InputRowName;
                        ARMA.IsAVG = armaA0.Checked;
                        ARMA.DataAr = InputRow.a;
                        if (ARMA.P == 0 & ARMA.Q == 0) return;
                        ARMA.H = CreateHMatrix();
                        Matrix Y = CreateYMatrix(ARMA.DataAr.Length - ARMA.P);
                        if (metod == Methods.LSM) { ARMA.B = StaticMethods.LSM(ARMA.H, Y); }
                        else { ARMA.B = StaticMethods.RLSM(ARMA.H, Y, 10); }
                        Matrix HB = Matrix.Increase(ARMA.H, ARMA.B);
                        ARMA.Resid = Matrix.Diff(Y, HB);
                        if (ARMA.Q != 0)
                        {
                            ARMA.H = CreateHMatrix(ARMA.Resid);
                            Y = CreateYMatrix(ARMA.H.CountRows);
                            ARMA.Resid = ARMA.Resid.Clone(ARMA.Resid.CountRows - Y.CountRows, ARMA.Resid.CountRows - 1, 0, 0);
                            //Y = Matrix.Diff(Y, ARMA.Resid);
                            ARMA.B = StaticMethods.LSM(ARMA.H, Y);
                        }
                        double[] Y_ = new double[ARMA.DataAr.Length];
                        double[] Ypr = new double[ModY().Length];
                        for (int l = 0; l < ARMA.DataAr.Length; l++) Y_[l] = ARMA.DataAr[l];
                        for (int l = 0; l < ModY().Length; l++) Ypr[l] = ModY()[l];
                        int CntParams = ARMA.P + ARMA.Q;
                        int d = Y_.Length - Ypr.Length;
                        if (d < 0) { throw new StatisticalAnalisFormException(); }
                        double[] resid = new double[Ypr.Length];
                        double[] tmp = new double[Ypr.Length];
                        for (int i_ = 0; i_ < resid.Length; i_++)
                        {
                            resid[i_] = Y_[i_ + d] - Ypr[i_];
                            tmp[i_] = Y_[i_ + d];
                        }
                        Y_ = tmp;
                        double dw = Statistical.NormedDW(Y_, Ypr);
                        double Rq = Statistical.NormedR2(Y_, Ypr);
                        double sse = Statistical.NormedSSE(Y_, Ypr);
                        if (i == i0 && j == 1)
                        {
                            DW = dw;
                            RQ = Rq;
                            SSE = sse;
                        }
                        switch (criteriaNames.SelectedIndex)
                        {
                            case 0: //SSE
                                {
                                    if (SSE > sse)
                                    {
                                        optAR = i;
                                        optMA = j;
                                        SSE = sse;
                                        RQ = Rq;
                                        DW = dw;
                                        OutputRow = new Vector(ModY());
                                        Parameters = ARMA.B;
                                    }
                                }
                                break;
                            case 1: //DW
                                {
                                    if (Math.Abs(2 - DW) > Math.Abs(2 - dw))
                                    {
                                        optAR = i;
                                        optMA = j;
                                        SSE = sse;
                                        RQ = Rq;
                                        DW = dw;
                                        OutputRow = new Vector(ModY());
                                        Parameters = ARMA.B;
                                    }

                                }
                                break;
                            case 2: //RQ
                                {
                                    if (Math.Abs(1 - RQ) > Math.Abs(1 - Rq))
                                    {
                                        optAR = i;
                                        optMA = j;
                                        SSE = sse;
                                        RQ = Rq;
                                        DW = dw;
                                        OutputRow = new Vector(ModY());
                                        Parameters = ARMA.B;
                                    }
                                }
                                break;
                        }
                    }
                string temp = "";
                switch (criteriaNames.SelectedIndex)
                {
                    case 0: //SSE
                        {
                            temp = "SSE";
                        }
                        break;
                    case 1: //DW
                        {
                            temp = "DW";
                        }
                        break;
                    case 2: //RQ
                        {
                            temp = "RQ";
                        }
                        break;
                }
                WriteLine("Найкраща " + temp + " при");
                WriteLine("Порядок: " + optAR.ToString());
                WriteLine("SSE: " + SSE.ToString());
                WriteLine("DW: " + DW.ToString());
                WriteLine("RQ: " + RQ.ToString());
                OutputParameters = new Vector(Parameters.CountRows);
                for (var i = 0; i < OutputParameters.n; i++) OutputParameters[i] = Parameters[i, 0];
                _resultPanelShow();
                _parametersPanelShow();
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректные введенные данные");
            }
        }

        private void _showParametersPanel()
        {

        }

        /// <summary>
        /// Створює код для таблиці результатів тесту на нестаціонарність для відображення на WebBrowser
        /// </summary>
        /// <param name="val">Результат тесту.</param>
        /// <returns>HTML-код таблиці результатів</returns>
        private String CreateHTMLTable(double val, string RowName, int typeDF)
        {
            string header = "Тест Дікі-Фуллера. Ряд \"" + RowName + "\"";
            string Tkp1;
            string Tkp5;

            switch (typeDF)
            {
                case 0:
                    Tkp1 = "-2.57";
                    Tkp5 = "-1.94";
                    break;
                case 1:
                    Tkp1 = "-3.43";
                    Tkp5 = "-2.86";
                    break;
                case 2:
                    Tkp1 = "-3.96";
                    Tkp5 = "-3.41";
                    break;
                default:
                    Tkp1 = "";
                    Tkp5 = "";
                    break;
            }
            StringBuilder code = new StringBuilder();
            code.Append("<html><body><table align=\"center\" border=\"3\" cols=\"3\">");
            code.Append("<tr bgcolor=\"#999999\"><td align=\"center\" colspan=\"4\">" + header + "</td></tr>");
            code.Append("<tr align=\"center\"><td>Тобч.</td><td>Ткр. при 5%</td><td>Ткр. при 1%</td></tr>");
            code.Append("<tr align=\"center\"><td>" + val.ToString("F3") + "</td><td>" + Tkp5 + "</td><td>" + Tkp1 + "</td></tr>");
            code.Append("</table></body></html>");
            return code.ToString();

        }

        private double[] ModY()
        {
            int s = ARMA.P;
            int f = ARMA.DataAr.Length;
            double[] Ypr = new double[f - s];
            for (int i = s; i < f; i++)
            {
                Ypr[i - s] = SPFunc(i);
            }
            return Ypr;
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
        private Matrix CreateHMatrix()
        {
            string[] strAR = new string[0];
            int mx = 0;
            if (!String.IsNullOrEmpty(ARMA.ParamsAR))
            {
                strAR = ARMA.ParamsAR.Split(';');
                mx = int.Parse(strAR[strAR.Length - 1]);
            }
            Matrix Hr = new Matrix(ARMA.DataAr.Length - mx, strAR.Length + 1);
            for (int i = Hr.CountRows - 1; i >= 0; i--)
            {
                Hr[i, 0] = 1;
                for (int j = 0; j < strAR.Length; j++)
                {
                    int p = int.Parse(strAR[j]);
                    Hr[i, j + 1] = ARMA.DataAr[mx + i - p];
                }
            }
            return Hr;
        }

        private Matrix CreateHMatrix(Matrix Resid)
        {
            string[] strAR = new string[0];
            string[] strMA = new string[0];
            int mxAR = 0;
            int mxMA = 0;
            if (!String.IsNullOrEmpty(ARMA.ParamsAR)) { strAR = ARMA.ParamsAR.Split(';'); mxAR = int.Parse(strAR[strAR.Length - 1]); }
            if (!String.IsNullOrEmpty(ARMA.ParamsMA)) { strMA = ARMA.ParamsMA.Split(';'); mxMA = int.Parse(strMA[strMA.Length - 1]); }
            int mx = mxAR + mxMA;
            Matrix Hr = new Matrix(ARMA.DataAr.Length - mx, 1 + strAR.Length + strMA.Length);
            for (int i = Hr.CountRows - 1; i >= 0; i--)
            {
                Hr[i, 0] = 1;
                for (int j = 0; j < strAR.Length; j++)
                {
                    int p = int.Parse(strAR[j]);
                    Hr[i, j + 1] = ARMA.DataAr[mx + i - p];
                }
                for (int j = 0; j < strMA.Length; j++)
                {
                    int q = int.Parse(strMA[j]);
                    Hr[i, j + 1 + strAR.Length] = Resid[i + mxMA - q, 0];
                }
            }

            return Hr;
        }
        /// <summary>
        /// Створює вектор Y
        /// </summary>
        /// <returns>Вектор Y</returns>
        private Matrix CreateYMatrix(int sizeY)
        {
            double[] yAR = new double[sizeY];
            int w = ARMA.DataAr.Length - sizeY;
            for (int i = 0; i < yAR.Length; i++)
            {
                yAR[i] = ARMA.DataAr[w + i];
            }
            Matrix Y = new Matrix(yAR);
            return Y;
        }

        private bool _nonlinearityTest()
        {
            double R_1 = 0;
            double R_2 = 0;
            var metod = Methods.LSM;
            for (int i = 1; i <= 2; i++)
                for (int j = 0; j < 1; j++)
                {
                    _paramsAR = "";
                    _paramsMA = "";
                    {
                        for (int k = 1; k <= i; k++)
                        {
                            _paramsAR += k.ToString();
                            if (k != i) _paramsAR += ";";
                        }
                        if (j == 0)
                        {
                            _paramsMA = "";
                        }
                        else
                        {
                            for (int k = 1; k <= j; k++)
                            {
                                _paramsMA += k.ToString();
                                if (k != j) _paramsMA += ";";
                            }
                        }
                    }
                    ARMA = new ARMA();
                    ARMA.ParamsAR = _paramsAR;
                    ARMA.ParamsMA = _paramsMA;
                    ARMA.RowName = InputRowName;
                    ARMA.IsAVG = true;
                    bool flag;
                    ARMA.DataAr = InputRow.a;
                    ARMA.H = CreateHMatrix();
                    Matrix Y = CreateYMatrix(ARMA.DataAr.Length - ARMA.P);
                    if (metod == Methods.LSM) { ARMA.B = StaticMethods.LSM(ARMA.H, Y); }
                    else { ARMA.B = StaticMethods.RLSM(ARMA.H, Y, 10); }
                    Matrix HB = Matrix.Increase(ARMA.H, ARMA.B);
                    ARMA.Resid = Matrix.Diff(Y, HB);
                    if (ARMA.Q != 0)
                    {
                        ARMA.H = CreateHMatrix(ARMA.Resid);
                        Y = CreateYMatrix(ARMA.H.CountRows);
                        ARMA.Resid = ARMA.Resid.Clone(ARMA.Resid.CountRows - Y.CountRows, ARMA.Resid.CountRows - 1, 0, 0);
                        ARMA.B = StaticMethods.LSM(ARMA.H, Y);
                    }
                    double[] Y_ = new double[ARMA.DataAr.Length];
                    double[] Ypr = new double[ModY().Length];
                    for (int l = 0; l < ARMA.DataAr.Length; l++) Y_[l] = ARMA.DataAr[l];
                    for (int l = 0; l < ModY().Length; l++) Ypr[l] = ModY()[l];
                    int CntParams = ARMA.P + ARMA.Q;
                    int d = Y_.Length - Ypr.Length;
                    if (d < 0) { throw new StatisticalAnalisFormException(); }
                    double[] resid = new double[Ypr.Length];
                    double[] tmp = new double[Ypr.Length];
                    for (int i_ = 0; i_ < resid.Length; i_++)
                    {
                        resid[i_] = Y_[i_ + d] - Ypr[i_];
                        tmp[i_] = Y_[i_ + d];
                    }
                    Y_ = tmp;
                    double Rq = Statistical.R2(Y_, Ypr);
                    if (i == 1) R_1 = Rq;
                    else R_2 = Rq;
                }
            if (R_2 > R_1) return true;
            else return false;
        }

        /// <summary>
        /// Оновлює інформацію про ряди даних
        /// </summary>
        public void RefreshRowDataGridView()
        {
            rowTable.RowCount = InputRow.n;
            for (int i = 0; i < InputRow.n; i++)
            {
                rowTable[0, i].Value = i;
                rowTable[1, i].Value = InputRow[i].ToString();
            }
        }
        public void RefreshResultRowDataGridView()
        {
            resultRowTable.RowCount = OutputRow.n;
            for (int i = 0; i < OutputRow.n; i++)
            {
                resultRowTable[0, i].Value = i;
                resultRowTable[1, i].Value = OutputRow[i].ToString();
            }
        }
        public void RefreshARParametersDataGridView()
        {
            parametersTable.RowCount = OutputParameters.n;
            for (int i = 0; i < ((armaA0.Checked) ? OutputParameters.n : OutputParameters.n - 1); i++)
            {
                parametersTable[0, i].Value = "a" + ((armaA0.Checked) ? i : i + 1).ToString();
                parametersTable[1, i].Value = OutputParameters[i].ToString();
            }
        }
        public void RefreshARMAParametersDataGridView()
        {
            parametersTable.RowCount = OutputParameters.n;
            var arcount = optAR;
            if (armaA0.Checked) arcount++;
            for (int i = 0; i < arcount; i++)
            {
                parametersTable[0, i].Value = "a" + ((armaA0.Checked) ? i : i + 1).ToString();
                parametersTable[1, i].Value = OutputParameters[i].ToString();
            }
            for (int i = arcount; i < OutputParameters.n; i++)
            {
                parametersTable[0, i].Value = "b" + (i - arcount).ToString();
                parametersTable[1, i].Value = OutputParameters[i].ToString();
            }
        }
        public void RefreshLogARParametersDataGridView()
        {
            parametersTable.RowCount = OutputParameters.n;
            var arcount = optAR;
            if (armaA0.Checked) arcount++;
            for (int i = 0; i < arcount; i++)
            {
                parametersTable[0, i].Value = "a" + ((armaA0.Checked) ? i : i + 1).ToString();
                parametersTable[1, i].Value = OutputParameters[i].ToString();
            }
        }
        private void _linearRow()
        {

            modelPanel.Controls.Clear();
            choseModelTitle = new Label()
            {
                Location = new Point(0, 0),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = modelPanel.Width,
                Height = 25,
                BackColor = darkColor,
                ForeColor = lightColor,
                Text = "Модель для лінійного ряду — АР(1)"
            };
            modelPanel.Controls.Add(choseModelTitle);
            choseOptimizationMethodTitle = new Label()
            {
                Location = new Point(0, choseModelTitle.Bottom),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = modelPanel.Width,
                Height = 25,
                BackColor = darkColor,
                ForeColor = lightColor,
                Text = "Оберіть метод наближення"
            };
            modelPanel.Controls.Add(choseOptimizationMethodTitle);
            optMethods = new ComboBox()
            {
                Location = new Point((modelPanel.Width - 150) / 2, choseOptimizationMethodTitle.Bottom),
                Size = new Size(150, 25),
                BackColor = lightColor,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            optMethods.Items.AddRange(
                new string[]
                {
                    "МНК",
                    "Рекурсивний МНК"
                }
                );
            optMethods.SelectedIndex = 0;
            modelPanel.Controls.Add(optMethods);
            extraTitle = new Label()
            {
                Location = new Point(0, optMethods.Bottom),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = modelPanel.Width,
                Height = 25,
                BackColor = darkColor,
                ForeColor = lightColor,
                Text = "Додатково"
            };
            modelPanel.Controls.Add(extraTitle);
            armaA0 = new CheckBox()
            {
                Location = new Point((modelPanel.Width - 100) / 2, extraTitle.Bottom),
                Size = new Size(100, 25),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Врахувати а0",
                Checked = false
            };
            modelPanel.Controls.Add(armaA0);
            emptyTitle = new Label()
            {
                Location = new Point(0, armaA0.Bottom),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = modelPanel.Width,
                Height = 25,
                BackColor = darkColor,
                ForeColor = lightColor,
                Text = ""
            };
            modelPanel.Controls.Add(emptyTitle);
            startButton = new Button()
            {
                Location = new Point((modelPanel.Width - 100) / 2, emptyTitle.Bottom + 25),
                Size = new Size(100, 25),
                Text = "Старт ...",
                BackColor = lightColor,
            };
            RemoveClickEvent(startButton);
            startButton.Click += new EventHandler(_startARClick);
            modelPanel.Controls.Add(startButton);
        }

        private void _nonlinearRow()
        {
            modelPanel.Controls.Clear();
            choseModelTitle = new Label()
            {
                Location = new Point(0, 0),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 14),
                Width = modelPanel.Width,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "Модель АРКС"
            };
            modelPanel.Controls.Add(choseModelTitle);
            //OSIO
            choseRangeTitle = new Label()
            {
                Location = new Point(0, 20),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = modelPanel.Width,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "Оберіть діапазони параметрів",
            };
            modelPanel.Controls.Add(choseRangeTitle);
            arTitle = new Label()
            {
                Location = new Point(25, 75),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Width = 75,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "Порядок АР:"
            };
            modelPanel.Controls.Add(arTitle);
            arFromTitle = new Label()
            {
                Location = new Point(100, 75),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Width = 25,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "від"
            };
            modelPanel.Controls.Add(arFromTitle);
            arFromText = new TextBox()
            {
                Location = new Point(125, 76),
                TextAlign = HorizontalAlignment.Center,
                Size = new Size(25, 25),
                Font = new Font(new FontFamily("Times New Roman"), 10),
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "1"
            };
            modelPanel.Controls.Add(arFromText);
            arToTitle = new Label()
            {
                Location = new Point(150, 75),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Width = 25,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = " до"
            };
            modelPanel.Controls.Add(arToTitle);
            arToText = new TextBox()
            {
                Location = new Point(175, 76),
                TextAlign = HorizontalAlignment.Center,
                Size = new Size(25, 25),
                Font = new Font(new FontFamily("Times New Roman"), 10),
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "15"
            };
            modelPanel.Controls.Add(arToText);
            maTitle = new Label()
            {
                Location = new Point(225, 75),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Width = 75,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "Порядок КС:"
            };
            modelPanel.Controls.Add(maTitle);
            maFromTitle = new Label()
            {
                Location = new Point(300, 75),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Width = 25,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "від"
            };
            modelPanel.Controls.Add(maFromTitle);
            maFromText = new TextBox()
            {
                Location = new Point(325, 76),
                TextAlign = HorizontalAlignment.Center,
                Size = new Size(25, 25),
                Font = new Font(new FontFamily("Times New Roman"), 10),
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "0"
            };
            modelPanel.Controls.Add(maFromText);
            maToTitle = new Label()
            {
                Location = new Point(350, 75),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Width = 25,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = " до"
            };
            modelPanel.Controls.Add(maToTitle);
            maToText = new TextBox()
            {
                Location = new Point(375, 76),
                TextAlign = HorizontalAlignment.Center,
                Size = new Size(25, 25),
                Font = new Font(new FontFamily("Times New Roman"), 10),
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "15"
            };
            modelPanel.Controls.Add(maToText);
            choseOptimizationMethodTitle = new Label()
            {
                Location = new Point(0, 100),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = modelPanel.Width,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "Оберіть метод наближення"
            };
            modelPanel.Controls.Add(choseOptimizationMethodTitle);
            optMethods = new ComboBox()
            {
                Location = new Point((modelPanel.Width - 150) / 2, 125),
                Size = new Size(150, 25),
                BackColor = lightColor,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            optMethods.Items.AddRange(
                new string[]
                {
                    "МНК",
                    "Рекурсивний МНК"
                }
                );
            optMethods.SelectedIndex = 0;
            modelPanel.Controls.Add(optMethods);
            choseCriteriaTitle = new Label()
            {
                Location = new Point(0, 150),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = modelPanel.Width,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "Оберіть критерій оптимальності моделі"
            };
            modelPanel.Controls.Add(choseCriteriaTitle);
            criteriaNames = new ComboBox()
            {
                Location = new Point((modelPanel.Width - 150) / 2, 175),
                Size = new Size(150, 25),
                BackColor = lightColor,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            criteriaNames.Items.AddRange(
                new string[]
                { "Сумма квадратов похибок",

                    "Дурбіна-Уотсона",
                    "Коефіцієнт детермінації",
                    
                    "Комбінований"
                }
                );
            criteriaNames.SelectedIndex = 0;
            modelPanel.Controls.Add(criteriaNames);
            extraTitle = new Label()
            {
                Location = new Point(0, criteriaNames.Bottom),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = modelPanel.Width,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "Додатково"
            };
            modelPanel.Controls.Add(extraTitle);
            armaA0 = new CheckBox()
            {
                Location = new Point((modelPanel.Width - 100) / 2, extraTitle.Bottom),
                Size = new Size(100, 25),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Врахувати а0",
                Checked = false
            };
            modelPanel.Controls.Add(armaA0);
            emptyTitle = new Label()
            {
                Location = new Point(0, armaA0.Bottom),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = modelPanel.Width,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = ""
            };
            modelPanel.Controls.Add(emptyTitle);
            startButton = new Button()
            {
                Location = new Point((modelPanel.Width - 100) / 2, emptyTitle.Bottom + 25),
                Size = new Size(100, 25),
                Text = "Старт ...",
                BackColor = lightColor,
            };
            RemoveClickEvent(startButton);
            startButton.Click += new EventHandler(_startARMAClick);
            modelPanel.Controls.Add(startButton);
        }
        private void RemoveClickEvent(Button b)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick",
                BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(b);
            PropertyInfo pi = b.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
            list.RemoveHandler(obj, list[obj]);
        }
        private void _parametersPanelShow()
        {
            parametersPanel.Controls.Clear();
            parametersPanelTitle = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = parametersPanel.Width,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "Параметри моделі"
            };
            parametersPanel.Controls.Add(parametersPanelTitle);
            parametersTable = new DataGridView()
            {
                Location = new Point(25, parametersPanelTitle.Bottom),
                Size = new Size(200, 300),
                BackgroundColor = lightColor,
                ColumnCount = 2,
                ReadOnly = true,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowHeadersVisible = false,
                ColumnHeadersVisible = true,
                ColumnHeadersHeight = 25,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
            };
            parametersTable.EnableHeadersVisualStyles = true;
            parametersTable.ColumnHeadersDefaultCellStyle.ForeColor = lightColor;
            parametersTable.ColumnHeadersDefaultCellStyle.BackColor = darkColor;
            parametersTable.DefaultCellStyle.ForeColor = darkColor;
            parametersTable.DefaultCellStyle.BackColor = lightColor;
            parametersTable.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            parametersTable.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            parametersTable.Columns[0].Width = 100;
            parametersTable.Columns[1].Width = 100;
            parametersTable.Columns[0].HeaderText = "Параметр";
            parametersTable.Columns[1].HeaderText = "Значення";
            parametersPanel.Controls.Add(parametersTable);
            forecastButton = new Button()
            {
                Location = new Point(70, parametersTable.Bottom + 25),
                Size = new Size(100, 25),
                Text = "Прогноз ...",
                BackColor = lightColor,
            };
            forecastButton.Click += new EventHandler(_forecastClick);
            parametersPanel.Controls.Add(forecastButton);
            if (model == 0) RefreshARParametersDataGridView();
            if (model == 1) RefreshARMAParametersDataGridView();
            if (model == 2) RefreshLogARParametersDataGridView();
        }
        private void _forecastClick(object sender, EventArgs e)
        {
            _paramsAR = "";
            _paramsMA = "";
            {
                for (int k = 1; k <= optAR; k++)
                {
                    _paramsAR += k.ToString();
                    if (k != optAR) _paramsAR += ";";
                }
                if (optMA == 0)
                {
                    _paramsMA = "";
                }
                else
                {
                    for (int k = 1; k <= optMA; k++)
                    {
                        _paramsMA += k.ToString();
                        if (k != optMA) _paramsMA += ";";
                    }
                }
            }
            metod = Methods.LSM;
            ARMA = new ARMA();
            ARMA.ParamsAR = _paramsAR;
            ARMA.ParamsMA = _paramsMA;
            ARMA.RowName = InputRowName;
            ARMA.IsAVG = true;
            bool flag;
            ARMA.DataAr = InputRow.a;
            if (ARMA.P == 0 & ARMA.Q == 0) return;
            ARMA.H = CreateHMatrix();
            Matrix Y = CreateYMatrix(ARMA.DataAr.Length - ARMA.P);
            if (metod == Methods.LSM) { ARMA.B = StaticMethods.LSM(ARMA.H, Y); }
            else { ARMA.B = StaticMethods.RLSM(ARMA.H, Y, 10); }
            Matrix HB = Matrix.Increase(ARMA.H, ARMA.B);
            ARMA.Resid = Matrix.Diff(Y, HB);
            if (ARMA.Q != 0)
            {
                ARMA.H = CreateHMatrix(ARMA.Resid);
                Y = CreateYMatrix(ARMA.H.CountRows);
                ARMA.Resid = ARMA.Resid.Clone(ARMA.Resid.CountRows - Y.CountRows, ARMA.Resid.CountRows - 1, 0, 0);
                ARMA.B = StaticMethods.LSM(ARMA.H, Y);
            }
            double[] Y_ = new double[ARMA.DataAr.Length];
            double[] Ypr = new double[ModY().Length];
            for (int l = 0; l < ARMA.DataAr.Length; l++) Y_[l] = ARMA.DataAr[l];
            for (int l = 0; l < ModY().Length; l++) Ypr[l] = ModY()[l];
            int CntParams = ARMA.P + ARMA.Q;
            int d = Y_.Length - Ypr.Length;
            if (d < 0) { throw new StatisticalAnalisFormException(); }
            double[] resid = new double[Ypr.Length];
            double[] tmp = new double[Ypr.Length];
            for (int i_ = 0; i_ < resid.Length; i_++)
            {
                resid[i_] = Y_[i_ + d] - Ypr[i_];
                tmp[i_] = Y_[i_ + d];
            }
            Y_ = tmp;
            double dw = Statistical.DW(resid);
            double Rq = Statistical.R2(Y_, Ypr);
            double sse = Statistical.SEofRegression(Y_, Ypr);
            ForecastForm PrognosisWindow = new ForecastForm(ARMA);
            PrognosisWindow.Show();
        }
        private void _resultPanelShow()
        {
            resultRowPanel.Controls.Clear();
            resultRowPanelTitle = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Times New Roman"), 10),
                Width = resultRowPanel.Width,
                Height = 25,
                BackColor = lightColor,
                ForeColor = darkColor,
                Text = "Вихідний ряд"
            };
            resultRowPanel.Controls.Add(resultRowPanelTitle);
            resultRowTablePanel = new Panel()
            {
                Location = new Point(25, 25),
                Size = new Size(150+50, 350),
                BackColor = lightColor
            };
            resultRowPanel.Controls.Add(resultRowTablePanel);
            #region Menu
            resultRowMenuStrip = new ToolStrip()
            {
                Location = new Point(25, 25),
                Size = new Size(150, 25),
                TabIndex = 0,
                Text = "resultRowMenuStrip",
                Name = "resultRowMenuStrip",
                BackColor = lightColor,
                ForeColor = darkColor

            };
            saveResultRowButton = new ToolStripButton()
            {
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Image = Image.FromFile("fileImage.png"),
                Name = "fileButton",
                Size = new Size(25, 25),
                Text = "Зберегти як ...",
            };
            analysisResultRowButton = new ToolStripDropDownButton()
            {
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Image = Image.FromFile("graphImage.png"),
                Name = "graphButton",
                Size = new Size(25, 25),
                Text = "Аналіз",
            };
            graphResultRowItem = new ToolStripMenuItem()
            {
                Text = "Графік"
            };
            graphResultRowItem.Click += new EventHandler(_graphResultRowClick);
            analysisResultRowButton.DropDownItems.AddRange(new ToolStripItem[]
            {
                graphResultRowItem,
            }
            );
            resultRowMenuStrip.Items.Add(saveResultRowButton);
            resultRowMenuStrip.Items.AddRange(new ToolStripDropDownButton[]
            {
                analysisResultRowButton
            });
            resultRowTablePanel.Controls.Add(resultRowMenuStrip);

            Controls.Add(resultRowPanel);
            #endregion
            resultRowTable = new DataGridView()
            {
                Location = new Point(0, resultRowMenuStrip.Bottom),
                Size = new Size(resultRowTablePanel.Width+50, resultRowTablePanel.Height - resultRowMenuStrip.Bottom),
                BackgroundColor = lightColor,
                ColumnCount = 2,
                ReadOnly = true,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowHeadersVisible = false,
                ColumnHeadersVisible = true,
                ColumnHeadersHeight = 25,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,

            };
            resultRowTable.EnableHeadersVisualStyles = false;
            resultRowTable.ColumnHeadersDefaultCellStyle.ForeColor = darkColor;
            resultRowTable.ColumnHeadersDefaultCellStyle.BackColor = lightColor;
            resultRowTable.DefaultCellStyle.ForeColor = darkColor;
            resultRowTable.DefaultCellStyle.BackColor = lightColor;
            resultRowTable.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            resultRowTable.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            resultRowTable.Columns[0].Width = 75;
            resultRowTable.Columns[1].Width = 150;
            resultRowTable.Columns[0].HeaderText = "Параметр";
            resultRowTable.Columns[1].HeaderText = "Значення";
            resultRowTablePanel.Controls.Add(resultRowTable);
            RefreshResultRowDataGridView();
        }

  


 
        private void _basicFormLoad(object sender, System.EventArgs e)
        {

            CenterToScreen();
        }
    }

}
