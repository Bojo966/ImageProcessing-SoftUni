using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Tester
{
    class Program
    {

        static void Main()
        {
            Bitmap original = new Bitmap("woman.png");
            Bitmap boxBlurred = (Bitmap)original.Clone();

            ApplyBlur(original, boxBlurred);
            boxBlurred.Save("woman-boxBlurrged.png");
        }

        private static void ApplyBlur(Bitmap original, Bitmap blurrred)
        {
            int[,] currentKernel = new int[,]
            {
                {1,1,1},
                {1,1,1 },
                {1,1,1 }
            };
            for (int row = 1; row < original.Width; row++)
            {
                for (int col = 1; col < original.Height; col++)
                {
                    FillCurrentPixel(original, blurrred, currentKernel, row, col);
                }                                          
            }
        }

        private static void FillCurrentPixel(Bitmap original, Bitmap blurred, int[,] blurMatrix, int row, int col)
        {
            int[] rowsSteps = { -1, 0, 1 };
            int[] colsSteps = { -1, 0, 1 };
            double redPixelSum = 0.0;
            double greenPixelSum = 0.0;
            double bluePixelSum = 0.0;
            double alphaPixalSum = 0.0;
            int currentPixelWeightCounter = 0;
            for (int currentRow = 0; currentRow < blurMatrix.GetLength(0); currentRow++)
            {
                for (int currentCol = 0; currentCol < blurMatrix.GetLength(1); currentCol++)
                {
                    try
                    {
                        Color currentColor = original.GetPixel(row + rowsSteps[currentRow], col + colsSteps[currentCol]);
                        currentPixelWeightCounter += blurMatrix[currentRow, currentCol];
                        alphaPixalSum += (currentColor.A * blurMatrix[currentRow, currentCol]);
                        redPixelSum += (currentColor.R * blurMatrix[currentRow, currentCol]);
                        greenPixelSum += (currentColor.G * blurMatrix[currentRow, currentCol]);
                        bluePixelSum += (currentColor.B * blurMatrix[currentRow, currentCol]);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                    }
                    catch (ArgumentOutOfRangeException argOutOfRangeEx)
                    {
                    }
                }
            }
            byte red = (byte)Math.Abs((redPixelSum / currentPixelWeightCounter));
            byte green = (byte)Math.Abs((greenPixelSum / currentPixelWeightCounter));
            byte blue = (byte)Math.Abs((bluePixelSum / currentPixelWeightCounter));
            byte alpha = (byte)Math.Abs((alphaPixalSum / currentPixelWeightCounter));
            blurred.SetPixel(row, col, Color.FromArgb(alpha, red, green, blue));

        }
    }
}