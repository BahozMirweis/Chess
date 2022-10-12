using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chess_Piece
{
    internal class King : ChessPiece
    {
        public bool LeftCastle { get; set; }
        public bool RightCastle { get; set; }

        public King(Point startingPos, bool isWhite) : base(startingPos, isWhite)
        {
            PieceType = PieceType.King;
            PieceImage = Image.FromFile(Path.Combine(entryDirectoryLocation, "Resources", $"{getColour()} King.png"));
            LeftCastle = true;
            RightCastle = true;
        }

        public override void PossibleMoves(ChessPiece[,] config)
        {
            resetMoves();
            bool[] directionChecksX = new bool[] { CurrPos.X >= 1, CurrPos.X <= 6, true }, directionChecksY = new bool[] { CurrPos.Y >= 1, CurrPos.Y <= 6, true };
            int[] movesX = new int[] { CurrPos.X - 1, CurrPos.X + 1, CurrPos.X }, movesY = { CurrPos.Y - 1, CurrPos.Y + 1, CurrPos.Y };

            for (int i = 0; i < movesX.Length; i++)
            {
                for (int j = 0; j < movesY.Length; j++)
                {
                    if (directionChecksX[i] && directionChecksY[j])
                    {
                        if (config[movesX[i], movesY[j]] == null || (config[movesX[i], movesY[j]].isWhite != isWhite && !config[movesX[i], movesY[j]].Protected))
                        {
                            movesAvailable[movesY[j]].Set(movesX[i], true);
                        }  else
                        {
                            config[movesX[i], movesY[j]].Protected = true;
                        }
                    }
                }
            }

            BitArray[] leftRightMoves = Helpers.horizontalVerticleLinesFromPoint(CurrPos, config, !isWhite);

            if ((leftRightMoves[0][0] == true || leftRightMoves[7][0] == true) && LeftCastle)
            {
                movesAvailable[isWhite ? 7 : 0].Set(1, true);
            } 
            
            if ((leftRightMoves[0][7] == true || leftRightMoves[7][7] == true) && RightCastle)
            {
                movesAvailable[isWhite ? 7 : 0].Set(6, true);
            }
        }
    }
}
