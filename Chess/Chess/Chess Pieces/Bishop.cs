using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chess_Piece
{
    internal class Bishop : ChessPiece
    {
        public Bishop(Point startingPos, bool isWhite) : base(startingPos, isWhite)
        {
            PieceType = PieceType.Bishop;
            PieceImage = Image.FromFile(Path.Combine(entryDirectoryLocation, "Resources", $"{getColour()} Bishop.png"));
        }

        public override void PossibleMoves(ChessPiece[,] config)
        {
            movesAvailable = Helpers.diagonalLinesFromPoint(CurrPos, config, isWhite, true);
        }
    }
}
