using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Chess.Chess_Piece
{
    internal class Queen : ChessPiece
    {
        public Queen(Point startingPos, bool isWhite) : base(startingPos, isWhite)
        {
            PieceType = PieceType.Queen;
            PieceImage = Image.FromFile(Path.Combine(entryDirectoryLocation, "Resources", $"{getColour()} Queen.png"));
        }

        public override void PossibleMoves(ChessPiece[,] config)
        {
            resetMoves();
            BitArray[] verticleMoves = Helpers.horizontalVerticleLinesFromPoint(CurrPos, config, isWhite, true);
            BitArray[] diagonalMoves = Helpers.diagonalLinesFromPoint(CurrPos, config, isWhite, true);

            for (int i = 0; i < 8; i++)
            {
                movesAvailable[i] = verticleMoves[i].Or(diagonalMoves[i]);
            }
        }
    }
}
