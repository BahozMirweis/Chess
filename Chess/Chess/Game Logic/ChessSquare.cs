using Chess.Chess_Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class ChessSquare
    {
        public PictureBox SquarePictureBox { get; set; }
        private Bitmap image, selectedImage;
        public bool IsSelected { get { return isSelected; } }
        private bool isSelected;
        ChessPiece currPiece;
        public readonly Point location;

        public ChessSquare(PictureBox squarePictureBox, Bitmap image, Point location)
        {
            currPiece = null;
            SquarePictureBox = squarePictureBox;
            this.image = image;
            this.location = location;
            isSelected = false;
            selectedImage = new Bitmap(image);

            using (Graphics grf = Graphics.FromImage(selectedImage))
            {
                using (Brush brsh = new SolidBrush(ColorTranslator.FromHtml("#ff00ffff")))
                {
                    grf.FillEllipse(brsh, 0, 0, 19, 19);
                }
            }
        }

        public void selectSquare()
        {
            SquarePictureBox.Image = selectedImage;
            isSelected = true;
        }

        public void unselectSquare()
        {
            SquarePictureBox.Image = image;
            isSelected = false;
        }

        internal ChessPiece changePiece(ChessPiece changePiece) {
            if (currPiece != null)
            {
                SquarePictureBox.Controls.Remove(currPiece.PiecePictureBox);
            }

            if (changePiece != null)
            {
                SquarePictureBox.Controls.Add(changePiece.PiecePictureBox);
            }
            ChessPiece temp = currPiece;
            currPiece = changePiece;

            return temp;
        }
    }
}
