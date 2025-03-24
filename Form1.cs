using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalMatrixCalculator
{
    public partial class Form1 : Form
    {
        private int m_RowCount = 3;
        private int m_ColumnCount = 3;
        private int m_SignalCount = 3;

        private Button[,] m_ButtonArray;
        private Label[,] m_LabelArray;

        private string m_VisitedNodeCountTip = "Visited Node Count:  {0},  Total Node Count:  {1}";

        private int m_TotalNodeCount;

        private CancellationTokenSource m_CTS;

        public Form1()
        {
            InitializeComponent();

            textBox_RowCount.Text = "3";
            textBox_ColumnCount.Text = "3";
            textBox_SingleCount.Text = "3";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            EnsureValidSingle(textBox_RowCount);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            EnsureValidSingle(textBox_ColumnCount);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            EnsureValidSingle(textBox_SingleCount);
        }

        private void EnsureValidSingle(TextBox _textBox)
        {
            if (!int.TryParse(_textBox.Text, out int signal))
                _textBox.Text = "2";
            else
            {
                if (signal < 2)
                    _textBox.Text = "2";
                else if (signal > 5)
                    _textBox.Text = "5";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button;
            Label label;

            if (m_ButtonArray != null)
            {
                int oldRowCount = m_ButtonArray.GetLength(0);
                int oldColumnCount = m_ButtonArray.GetLength(1);

                for (int row = 0; row < oldRowCount; row++)
                {
                    for (int column = 0; column < oldColumnCount; column++)
                    {
                        button = m_ButtonArray[row, column];

                        button.MouseDown -= OnClickMatrixButton;
                        this.Controls.Remove(button);
                        button.Dispose();

                        label = m_LabelArray[row, column];
                        this.Controls.Remove(label);
                        label.Dispose();
                    }
                }
            }

            label_OperationGuide.Visible = true;

            int.TryParse(textBox_RowCount.Text, out m_RowCount);
            int.TryParse(textBox_ColumnCount.Text, out m_ColumnCount);
            int.TryParse(textBox_SingleCount.Text, out m_SignalCount);

            m_ButtonArray = new Button[m_RowCount, m_ColumnCount];
            m_LabelArray = new Label[m_RowCount, m_ColumnCount];

            for (int row = 0; row < m_RowCount; row++)
            {
                for (int column = 0; column < m_ColumnCount; column++)
                {
                    button = new Button();

                    this.Controls.Add(button);

                    button.MouseDown += OnClickMatrixButton;
                    button.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                    button.SetBounds(20 + row * 60, this.Height - column * 60 - 140, 40, 40);
                    button.Name = $"{row},{column}";
                    button.Text = $"0";

                    m_ButtonArray[row, column] = button;

                    //=====================================

                    label = new Label();

                    this.Controls.Add(label);

                    label.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                    label.SetBounds(20 + row * 60, this.Height - column * 60 - 100, 40, 10);
                    label.Text = $"({row},{column})";
                    label.TextAlign = ContentAlignment.MiddleCenter;

                    m_LabelArray[row, column] = label;
                }
            }
        }

        private void OnClickMatrixButton(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    AddButtonSingle(button);
                    break;
                case MouseButtons.Right:

                    int signal = 0;
                    int.TryParse(button.Text.Split('\n')[0], out signal);

                    if (signal == -1)
                        return;

                    string[] nameStringArray = button.Name.Split(',');
                    int.TryParse(nameStringArray[0], out int row);
                    int.TryParse(nameStringArray[1], out int column);

                    AddButtonSingle(button);
                    if (row > 0) AddButtonSingle(m_ButtonArray[row - 1, column]);
                    if (row < m_RowCount - 1) AddButtonSingle(m_ButtonArray[row + 1, column]);
                    if (column > 0) AddButtonSingle(m_ButtonArray[row, column - 1]);
                    if (column < m_ColumnCount - 1) AddButtonSingle(m_ButtonArray[row, column + 1]);

                    break;

                case MouseButtons.Middle:
                    IgnoreButton(button);
                    break;
            }
        }

        private void AddButtonSingle(Button _button)
        {
            int.TryParse(_button.Text.Split('\n')[0], out int signal);

            if (signal == -1)
                return;

            signal++;

            if (signal >= m_SignalCount)
                signal = 0;

            SetButtonSingle(_button, signal);
        }

        private void SetButtonSingle(Button _button, int _single)
        {
            _button.Text = _single.ToString();
        }

        private void IgnoreButton(Button _button)
        {
            int.TryParse(_button.Text.Split('\n')[0], out int signal);

            SetButtonSingle(_button, signal == -1 ? 0 : -1);
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (m_CTS != null)
                return;

            label_Step.Text = string.Empty;

            int[,] initialState = new int[m_RowCount, m_ColumnCount];
            List<(int, int)> ignorePointList = new List<(int, int)>();

            for (int row = 0; row < m_RowCount; row++)
            {
                for (int column = 0; column < m_ColumnCount; column++)
                {
                    int.TryParse(m_ButtonArray[row, column].Text, out int signal);

                    if (signal == -1)
                        ignorePointList.Add((row, column));
                    else
                        initialState[row, column] = signal;
                }
            }

            m_TotalNodeCount = (int)Math.Round(Math.Pow(m_SignalCount, m_RowCount * m_ColumnCount - ignorePointList.Count));

            m_CTS = new CancellationTokenSource();
            var stepList = await MatrixHandler.Bfs(initialState, ignorePointList, m_RowCount, m_ColumnCount, m_SignalCount, ShowVisitedNodeCount, m_CTS.Token);

            if (stepList != null)
            {

                StringBuilder stringBuilder = new StringBuilder();

                for (var i = 0; i < stepList.Count; i++)
                {
                    stringBuilder.AppendLine($"{i + 1}:  {stepList[i].ToString()}");
                }

                label_Step.Text = stringBuilder.ToString();
            }
            else
            {
                label_Step.Text = "NoResult  无结果";
            }

            m_CTS = null;
        }

        private void ShowVisitedNodeCount(int _count)
        {
            label_VisitedNodeCount.Text = string.Format(m_VisitedNodeCountTip, _count, m_TotalNodeCount);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (m_CTS != null)
            {
                m_CTS.Cancel();

                await Task.Delay(10);

                m_CTS = null;
            }
        }
    }
}
