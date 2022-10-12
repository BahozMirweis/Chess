using Chess.Chess_Piece;
using System.Text.Json.Serialization;

namespace Chess
{
    partial class ChessForm
    {
        public static ChessSquare[,] squares;

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void createSquares()
        {
            squares = new ChessSquare[8, 8]; 

            int squareWidth = this.ClientSize.Width / 8;
            int squareHeight = this.ClientSize.Height / 8;
            

            Bitmap darkSquare = new Bitmap(squareWidth, squareHeight);
            using (Graphics gfx = Graphics.FromImage(darkSquare))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(181, 136, 99)))
            {
                gfx.FillRectangle(brush, 0, 0, squareWidth, squareHeight);
            }

            Bitmap lightSquare = new Bitmap(squareWidth, squareHeight);
            using (Graphics gfx = Graphics.FromImage(lightSquare))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(240, 217, 181)))
            {
                gfx.FillRectangle(brush, 0, 0, squareWidth, squareHeight);
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var square = new System.Windows.Forms.PictureBox();
                    square.Location = new System.Drawing.Point(squareWidth*i, squareHeight*j);
                    square.Name = $"{i}{j}";
                    square.Size = new System.Drawing.Size(squareWidth, squareHeight);
                    square.TabIndex = 0;
                    square.TabStop = false;
                    square.Image = (i + j) % 2 == 0 ? lightSquare : darkSquare;
                    squares[i, j] = new ChessSquare(square, (i + j) % 2 == 0 ? lightSquare : darkSquare, new Point(i, j));
                    this.Controls.Add(square);
                }
            }
        }


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ChessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 960);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChessForm";
            this.Text = "Chess";
            this.ResumeLayout(false);

        }

        #endregion
    }
}