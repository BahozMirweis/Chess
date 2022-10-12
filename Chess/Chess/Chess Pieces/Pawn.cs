using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chess_Piece
{
    internal class Pawn : ChessPiece
    {
        public bool EnPessant { get; set; }
        private bool enPessantCount = false;
        public Pawn(Point startingPos, bool isWhite) : base(startingPos, isWhite)
        {
            PieceType = PieceType.Pawn;
            PieceImage = Image.FromFile(Path.Combine(entryDirectoryLocation, "Resources", $"{getColour()} Pawn.png"));
        }

        public override void PossibleMoves(ChessPiece[,] config)
        {
            resetMoves();

            if (EnPessant)
            {
                if (enPessantCount)
                {
                    EnPessant = false;
                } else
                {
                    enPessantCount = true;
                }
            }

            int direction = isWhite ? -1 : 1;

            if ((CurrPos.Y - direction) % 7 == 0 && config[CurrPos.X, CurrPos.Y + direction*2] == null && config[CurrPos.X, CurrPos.Y + direction] == null)
            {
                movesAvailable[CurrPos.Y + direction*2].Set(CurrPos.X, true);
            }

            if (config[CurrPos.X, CurrPos.Y + direction] == null)
            {
                movesAvailable[CurrPos.Y + direction].Set(CurrPos.X, true);
            }

            if (CurrPos.X < 7 && config[CurrPos.X + 1, CurrPos.Y + direction] != null)
            {
                if (config[CurrPos.X + 1, CurrPos.Y + direction].isWhite != isWhite)
                {
                    movesAvailable[CurrPos.Y + direction].Set(CurrPos.X + 1, true);
                } else
                {
                    config[CurrPos.X + 1, CurrPos.Y + direction].Protected = true;
                }
            } 

            if (CurrPos.X > 0 && config[CurrPos.X - 1, CurrPos.Y + direction] != null)
            {
                if (config[CurrPos.X - 1, CurrPos.Y + direction].isWhite != isWhite)
                {
                    movesAvailable[CurrPos.Y + direction].Set(CurrPos.X - 1, true);
                } else
                {
                    config[CurrPos.X - 1, CurrPos.Y + direction].Protected = true;
                }
            }

            // Enpessant checks
            if (CurrPos.X < 7 && config[CurrPos.X + 1, CurrPos.Y] != null && config[CurrPos.X + 1, CurrPos.Y].isWhite != isWhite
                && config[CurrPos.X + 1, CurrPos.Y].PieceType == PieceType.Pawn && ((Pawn)config[CurrPos.X + 1, CurrPos.Y]).EnPessant == true)
            {
                movesAvailable[CurrPos.Y + direction].Set(CurrPos.X + 1, true);
            }

            if (CurrPos.X > 0 && config[CurrPos.X - 1, CurrPos.Y] != null && config[CurrPos.X - 1, CurrPos.Y].isWhite != isWhite
    && config[CurrPos.X - 1, CurrPos.Y].PieceType == PieceType.Pawn && ((Pawn)config[CurrPos.X - 1, CurrPos.Y]).EnPessant == true)
            {
                movesAvailable[CurrPos.Y + direction].Set(CurrPos.X - 1, true);
            }
        }
    }
}
