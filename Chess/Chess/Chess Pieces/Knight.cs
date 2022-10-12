using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chess_Piece
{
    internal class Knight : ChessPiece
    {
        public Knight(Point startingPos, bool isWhite) : base(startingPos, isWhite)
        {
            PieceType = PieceType.Knight;
            PieceImage = Image.FromFile(Path.Combine(entryDirectoryLocation, "Resources", $"{getColour()} Knight.png"));
        }

        public override void PossibleMoves(ChessPiece[,] config)
        {
            resetMoves();
            (int, int)[] movePairs = new (int, int)[] { (2, 1), (1, 2), (-1, 2), (2, -1), (-2, -1), (-1, -2), (-2, 1), (1, -2) };

            foreach (var pair in movePairs)
            {
                if (CurrPos.X + pair.Item1 >= 0 && CurrPos.X + pair.Item1 <= 7 && CurrPos.Y + pair.Item2 >= 0 && CurrPos.Y + pair.Item2 <= 7)
                {
                    if (config[CurrPos.X + pair.Item1, CurrPos.Y + pair.Item2] == null || config[CurrPos.X + pair.Item1, CurrPos.Y + pair.Item2].isWhite != isWhite)
                    {
                        movesAvailable[CurrPos.Y + pair.Item2].Set(CurrPos.X + pair.Item1, true);
                    } else
                    {
                        config[CurrPos.X + pair.Item1, CurrPos.Y + pair.Item2].Protected = true;
                    }
                }
            }
        }

    }
}
