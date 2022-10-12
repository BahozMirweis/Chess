using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chess_Piece
{
    internal class Rook : ChessPiece
    {
        public Rook(Point startingPos, bool isWhite) : base(startingPos, isWhite)
        {
            PieceType = PieceType.Rook;
            PieceImage = Image.FromFile(Path.Combine(entryDirectoryLocation, "Resources", $"{getColour()} Rook.png"));
        }

        public override void PossibleMoves(ChessPiece[,] config)
        {
            movesAvailable = Helpers.horizontalVerticleLinesFromPoint(CurrPos, config, isWhite, true);
        }
    }
}
