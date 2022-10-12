using Chess.Chess_Piece;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal static class Helpers
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static void selectSquares(BitArray[] squaresToSelect)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (squaresToSelect[i][j])
                    {
                        ChessForm.squares[j, i].selectSquare();
                    }
                    else
                    {
                        ChessForm.squares[j, i].unselectSquare();
                    }
                }
            }
        }

        public static void unselectSquares()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ChessForm.squares[j, i].unselectSquare();
                }
            }
        }

        public static BitArray[] horizontalVerticleLinesFromPoint(Point start, ChessPiece[,] config, bool white, bool protect = false)
        {
            int count = 1;
            bool hor1 = true, hor2 = true, vert1 = true, vert2 = true;

            BitArray[] availablePoints = new BitArray[8];

            for (int i = 0; i < 8; i++)
            {
                availablePoints[i] = new BitArray(8);
            }

            while (Math.Min(start.X + count, start.Y + count) < 8 || Math.Max(start.X - count, start.Y - count) >= 0)
            {
                if (start.X + count < 8 && hor1)
                {
                    if (config[start.X + count, start.Y] == null)
                    {
                        availablePoints[start.Y].Set(start.X + count, true);
                    }
                    else if (config[start.X + count, start.Y].isWhite != white)
                    {
                        availablePoints[start.Y].Set(start.X + count, true);
                        hor1 = false;
                    } 
                    else
                    {
                        hor1 = false;
                        if (protect) config[start.X + count, start.Y].Protected = true;
                    }
                }

                if (start.X - count >= 0 && hor2)
                {
                    if (config[start.X - count, start.Y] == null)
                    {
                        availablePoints[start.Y].Set(start.X - count, true);
                    }
                    else if (config[start.X - count, start.Y].isWhite != white)
                    {
                        availablePoints[start.Y].Set(start.X - count, true);
                        hor2 = false;
                    } else
                    {
                        hor2 = false;
                        if (protect) config[start.X - count, start.Y].Protected = true;
                    }
                }

                if (start.Y + count < 8 && vert1)
                {

                    if (config[start.X, start.Y + count] == null)
                    {
                        availablePoints[start.Y + count].Set(start.X, true);
                    }
                    else if (config[start.X, start.Y + count].isWhite != white)
                    {
                        availablePoints[start.Y + count].Set(start.X, true);
                        vert1 = false;
                    } else
                    {
                        vert1 = false;
                        if (protect) config[start.X, start.Y + count].Protected = true;
                    }
                }

                if (start.Y - count >= 0 && vert2)
                {
                    if (config[start.X, start.Y - count] == null)
                    {
                        availablePoints[start.Y - count].Set(start.X, true);
                    }
                    else if (config[start.X, start.Y - count].isWhite != white)
                    {
                        availablePoints[start.Y - count].Set(start.X, true);
                        vert2 = false;
                    } else
                    {
                        vert2 = false;
                        if (protect) config[start.X, start.Y - count].Protected = true;
                    }
                }

                count += 1;
            }

            return availablePoints;
        }

        public static BitArray[] diagonalLinesFromPoint(Point start, ChessPiece[,] config, bool white, bool protect = false)
        {
            int count = 1;
            bool diag1 = true, diag2 = true, diag3 = true, diag4 = true;

            BitArray[] availablePoints = new BitArray[8];

            for (int i = 0; i < 8; i++)
            {
                availablePoints[i] = new BitArray(8);
            }

            while (Math.Min(start.X + count, start.Y + count) < 8 || Math.Max(start.X - count, start.Y - count) >= 0)
            {
                if (start.X + count < 8 && start.Y + count < 8 && diag1)
                {
                    if (config[start.X + count, start.Y + count] == null)
                    {
                        availablePoints[start.Y + count].Set(start.X + count, true);
                    }
                    else if (config[start.X + count, start.Y + count].isWhite != white)
                    {
                        availablePoints[start.Y + count].Set(start.X + count, true);
                        diag1 = false;
                    }
                    else
                    {
                        diag1 = false;
                        if (protect) config[start.X + count, start.Y + count].Protected = true;
                    }
                }

                if (start.X - count >= 0 && start.Y - count >= 0 && diag2)
                {
                    if (config[start.X - count, start.Y - count] == null)
                    {
                        availablePoints[start.Y - count].Set(start.X - count, true);
                    }
                    else if (config[start.X - count, start.Y - count].isWhite != white)
                    {
                        availablePoints[start.Y - count].Set(start.X - count, true);
                        diag2 = false;
                    }
                    else
                    {
                        diag2 = false;
                        if (protect) config[start.X - count, start.Y - count].Protected = true;
                    }
                }

                if (start.Y + count < 8 && start.X - count >= 0 && diag3)
                {

                    if (config[start.X - count, start.Y + count] == null)
                    {
                        availablePoints[start.Y + count].Set(start.X - count, true);
                    }
                    else if (config[start.X - count, start.Y + count].isWhite != white)
                    {
                        availablePoints[start.Y + count].Set(start.X - count, true);
                        diag3 = false;
                    }
                    else
                    {
                        diag3 = false;
                        if (protect) config[start.X - count, start.Y + count].Protected = true;
                    }
                }

                if (start.Y - count >= 0 && start.X + count < 8 && diag4)
                {
                    if (config[start.X + count, start.Y - count] == null)
                    {
                        availablePoints[start.Y - count].Set(start.X + count, true);
                    }
                    else if (config[start.X + count, start.Y - count].isWhite != white)
                    {
                        availablePoints[start.Y - count].Set(start.X + count, true);
                        diag4 = false;
                    }
                    else
                    {
                        diag4 = false;
                        if (protect) config[start.X + count, start.Y - count].Protected = true;
                    }
                }

                count += 1;
            }

            return availablePoints;
        }

        /// <summary>
        /// Will change the first element to operate on the rest of the array by operation string
        /// </summary>
        /// <param name="bitArrays"></param>
        /// <param name="operation">AND, XOR, OR</param>
        public static void OperateListOfBitArrays(BitArray[] toChange, List<BitArray[]> bitArrays, string operation)
        {
            foreach (var bitArray in bitArrays)
            {
                OperateTwoBitArrays(toChange, bitArray, operation);
            }
        }

        /// <summary>
        /// will change the first array of bitArrays according to the operation applied 
        /// </summary>
        /// <param name="bits1"></param>
        /// <param name="bits2"></param>
        /// <param name="operation">AND, XOR, NOT, OR (NOT will only change the first element not second)</param>
        public static void OperateTwoBitArrays(BitArray[] bits1, BitArray[] bits2, string operation)
        {
            for (int i = 0; i < bits1.Length; i++)
            {
                switch (operation)
                {
                    case "AND":
                        bits1[i].And(bits2[i]);
                        break;

                    case "XOR":
                        bits1[i].Xor(bits2[i]);
                        break;

                    case "OR":
                        bits1[i].Or(bits2[i]);
                        break;

                    case "NOT":
                        bits1[i].Not();
                        break;
                }
            }
        }

        public static BitArray[] lineBetweenTwoPoints(Point p1, Point p2)
        {
            int dirY = Math.Sign(p2.Y - p1.Y);
            int dirX = Math.Sign(p2.X - p1.X);

            BitArray[] result = new BitArray[8];

            for (int i = 0; i < 8; i++)
            {
                result[i] = new BitArray(8);
            }

            int numberOfIterations = Math.Max(Math.Abs(p2.Y - p1.Y), Math.Abs(p2.X - p1.X));
            int x = p1.X, y = p1.Y;

            for (int i = 0; i <= numberOfIterations; i++)
            {
                result[y + i * dirY].Set(x + i * dirX, true);
            }

            return result;
        }

        public static BitArray[] initArray(BitArray[]? bitarray = null)
        {
            BitArray[] result = new BitArray[8];

            for (int i = 0; i < 8; i++)
            {
                if (bitarray == null)
                {
                    result[i] = new BitArray(8);
                }
                else
                {
                    result[i] = new BitArray(bitarray[i]);
                }
            }

            return result;
        }
    }
}
