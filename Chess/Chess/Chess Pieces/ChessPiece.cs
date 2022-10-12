using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chess_Piece
{
    abstract internal class ChessPiece : IDisposable
    {
        protected readonly string entryDirectoryLocation;
        public bool Protected { get; set; }
        public Point CurrPos { get; set; }
        public bool Turn { get; set; }
        public readonly bool isWhite;
        public BitArray[] movesAvailable;
        public PictureBox PiecePictureBox { get; set; }
        public Image PieceImage { get; set; }
        public PieceType PieceType { get; set; }

        public ChessPiece(Point startingPos, bool isWhite)
        {
            entryDirectoryLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            CurrPos = startingPos;
            this.isWhite = isWhite;
        }

        private void onClick(object? sender, MouseEventArgs e)
        {
            if (Turn)
            {
                Helpers.selectSquares(movesAvailable);
                ChessGame.selectedPiece = this;

            }
        }

        protected string getColour() => isWhite ? "White" : "Black";

        public Point coordinatePos(double squareWidth, double squareHeight) =>
            new Point((int)Math.Floor(squareWidth * 1.0 / 2 - PieceImage.Width / 2),
            (int)Math.Floor(squareHeight * 1.0 / 2 - PieceImage.Height / 2));


        public PictureBox initPieceOnForm(int squareWidth, int squareHeight)
        {
            PieceImage = Helpers.ResizeImage(PieceImage, 90, 90);

            PiecePictureBox = new PictureBox();
            PiecePictureBox.Location = coordinatePos(squareWidth, squareHeight);
            PiecePictureBox.Name = String.Format(" {0}{1}", CurrPos.X, CurrPos.Y);
            PiecePictureBox.Size = new Size(PieceImage.Width, PieceImage.Height);
            PiecePictureBox.TabIndex = 0;
            PiecePictureBox.TabStop = false;
            PiecePictureBox.Image = PieceImage;
            PiecePictureBox.BackColor = Color.Transparent;
            PiecePictureBox.MouseClick += onClick;
            return PiecePictureBox;
        }

        protected void resetMoves()
        {
            movesAvailable = new BitArray[8];

            for (int i = 0; i < 8; i++)
            {
                movesAvailable[i] = new BitArray(8);
            }
        }

        public bool canMove()
        {
            foreach (BitArray row in movesAvailable)
            {
                foreach(bool bit in row)
                {
                    if (bit)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public abstract void PossibleMoves(ChessPiece[,] config);

        public BitArray[] PossibleMovesNoChange(ChessPiece[,] config)
        {
            BitArray[] temp = Helpers.initArray(movesAvailable);
            PossibleMoves(config);
            BitArray[] result = movesAvailable;
            movesAvailable = temp;
            return result;
        }

        public void Dispose()
        {
            PiecePictureBox.Dispose();
            PieceImage.Dispose();
        }
    }
}
