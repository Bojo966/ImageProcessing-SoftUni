using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelTestApplication
{
    public partial class Form1 : Form
    {
        private const int NumberOfCols = 20;
        private const int NumberOfRows = 10;
        private Control[,] currentScreenState = new Control[NumberOfRows, NumberOfCols];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void GenerateMatrixBtn_Click(object sender, EventArgs e)
        {
            int index = 0;
            Random random = new Random();
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfCols; col++)
                {
                    int red = random.Next(0, 255);
                    this.pixelScreen.Controls[index].BackColor = Color.FromArgb(255, red, 0, 0);
                    this.currentScreenState[row, col] = this.pixelScreen.Controls[index];
                    index++;
                }
            }
        }

        private void ApplyBlurBtn_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfCols; col++)
                {
                    int red = GetBlurrePixelValue(row, col);
                    this.currentScreenState[row, col].BackColor = Color.FromArgb(255, red, 0, 0);
                }
            }
        }

        private int GetBlurrePixelValue(int currentRow, int currentCol)
        {
            int startRow = Math.Max(0, currentRow - 1);
            int startCol = Math.Max(0, currentCol - 1);
            int endRow = Math.Min(NumberOfRows, currentRow + 1);
            int endCol = Math.Min(NumberOfCols, currentCol + 1);
            int red = 0;
            int numberOfSums = 0;
            for (int row = startRow; row < endRow; row++)
            {
                for (int col = startCol; col < endCol; col++)
                {
                    red += this.currentScreenState[row, col].BackColor.R;
                    numberOfSums++;
                }
            }
            return red/numberOfSums;
        }
    }
}