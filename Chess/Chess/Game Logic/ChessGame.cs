using Chess.Chess_Piece;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class ChessGame : IDisposable
    {
        public static ChessPiece selectedPiece;
        private PawnPromoteForm promotionForm;
        public int turnCount { get; set; }
        public bool WhiteTurn { get => turnCount % 2 == 1; }
        private List<ChessPiece> blackDead = new List<ChessPiece>(), whiteDead = new List<ChessPiece>();
        private List<ChessPiece> blackPieces = new List<ChessPiece>(), whitePieces = new List<ChessPiece>();

        ChessPiece[,] pieces = new ChessPiece[8, 8];

        public ChessGame(ChessSquare[,] square)
        {
            pieces[0, 0] = new Rook(new Point(0, 0), false);
            pieces[1, 0] = new Knight(new Point(1, 0), false);
            pieces[2, 0] = new Bishop(new Point(2, 0), false);
            pieces[3, 0] = new Queen(new Point(3, 0), false);
            pieces[4, 0] = new King(new Point(4, 0), false);
            pieces[5, 0] = new Bishop(new Point(5, 0), false);
            pieces[6, 0] = new Knight(new Point(6, 0), false);
            pieces[7, 0] = new Rook(new Point(7, 0), false);
            pieces[0, 7] = new Rook(new Point(0, 7), true);
            pieces[1, 7] = new Knight(new Point(1, 7), true);
            pieces[2, 7] = new Bishop(new Point(2, 7), true);
            pieces[3, 7] = new Queen(new Point(3, 7), true);
            pieces[4, 7] = new King(new Point(4, 7), true);
            pieces[5, 7] = new Bishop(new Point(5, 7), true);
            pieces[6, 7] = new Knight(new Point(6, 7), true);
            pieces[7, 7] = new Rook(new Point(7, 7), true);

            for (int i = 0; i < 8; i++)
            {
                pieces[i, 1] = new Pawn(new Point(i, 1), false);
                pieces[i, 6] = new Pawn(new Point(i, 6), true);
            }

            for (int i = 0; i < 8; i++)
            {
                blackPieces.Add(pieces[i, 0]);
                blackPieces.Add(pieces[i, 1]);
                whitePieces.Add(pieces[i, 6]);
                whitePieces.Add(pieces[i, 7]);
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    square[i, j].SquarePictureBox.MouseClick += OnSquareClick;
                    if (pieces[i, j] != null)
                    {            
                        square[i, j].SquarePictureBox.Controls.Add(pieces[i, j].initPieceOnForm(square[i,j].SquarePictureBox.Width, square[i,j].SquarePictureBox.Height));
                        square[i, j].changePiece(pieces[i, j]);
                        pieces[i, j].PiecePictureBox.MouseClick += OnPieceClick;
                    }
                }
            }

            newTurn();
        }

        private void OnPieceClick(object sender, MouseEventArgs e)
        {
            PictureBox piecePic = (PictureBox)sender;

            ChessPiece piece = null;

            foreach (var p in pieces)
            {
                if (p != null && p.PiecePictureBox == piecePic)
                {
                    piece = p;
                }
            }

            if (piece.isWhite != WhiteTurn) OnSquareClick(ChessForm.squares[piece.CurrPos.X, piece.CurrPos.Y].SquarePictureBox, e);
        }

        private void OnSquareClick(object sender, MouseEventArgs e)
        {
            PictureBox square = (PictureBox)sender;

            int x = Int32.Parse(square.Name[0].ToString()), y = Int32.Parse(square.Name[1].ToString());

            ChessSquare clickedSquare = ChessForm.squares[x, y];

            if (clickedSquare.IsSelected && promotionForm == null)
            {
                moveChecks(new Point(x, y));
                movePiece(selectedPiece, new Point(x, y));

                Helpers.unselectSquares();
                if (selectedPiece.PieceType == PieceType.Pawn && (selectedPiece.CurrPos.Y == (selectedPiece.isWhite ? 0 : 7)))
                {
                    promotionForm = new PawnPromoteForm(promotionFormSubmit);
                    promotionForm.Show();
                }
                else
                {
                    selectedPiece = null;
                    newTurn();
                }
            }
        }

        private void promotionFormSubmit(object sender, EventArgs e)
        {
            List<ChessPiece> replace = WhiteTurn ? whitePieces : blackPieces;
            replace.Remove(selectedPiece);

            ChessPiece toAdd;

            switch(promotionForm.Selection)
            {
                case "Queen":
                    toAdd = new Queen(selectedPiece.CurrPos, WhiteTurn);
                    break;

                case "Rook":
                    toAdd = new Rook(selectedPiece.CurrPos, WhiteTurn);
                    break;

                case "Bishop":
                    toAdd = new Bishop(selectedPiece.CurrPos, WhiteTurn);
                    break;

                case "Knight":
                    toAdd = new Knight(selectedPiece.CurrPos, WhiteTurn);
                    break;

                default:
                    toAdd = new Queen(selectedPiece.CurrPos, WhiteTurn);
                    break;
            }

            ChessSquare toReplace = ChessForm.squares[selectedPiece.CurrPos.X, selectedPiece.CurrPos.Y];

            toAdd.initPieceOnForm(toReplace.SquarePictureBox.Width, toReplace.SquarePictureBox.Height);
            toAdd.PiecePictureBox.MouseClick += OnPieceClick;

            toReplace.changePiece(toAdd);
            pieces[selectedPiece.CurrPos.X, selectedPiece.CurrPos.Y] = toAdd;
            replace.Add(toAdd);

            promotionForm = null;
            selectedPiece = null;
            newTurn();
        }

        private void movePiece(ChessPiece pieceFrom, Point moveTo)
        {
            ChessSquare SquareFrom = ChessForm.squares[pieceFrom.CurrPos.X, pieceFrom.CurrPos.Y];
            ChessSquare SquareTo = ChessForm.squares[moveTo.X, moveTo.Y];
            SquareFrom.changePiece(null);
            ChessPiece occupiedPiece = SquareTo.changePiece(pieceFrom);

            pieces[pieceFrom.CurrPos.X, pieceFrom.CurrPos.Y] = null;
            pieces[moveTo.X, moveTo.Y] = pieceFrom;

            pieceFrom.CurrPos = moveTo;

            if (occupiedPiece != null)
            {
                if (occupiedPiece.isWhite)
                {
                    whitePieces.Remove(occupiedPiece);
                    whiteDead.Add(occupiedPiece);
                }
                else
                {
                    blackPieces.Remove(occupiedPiece);
                    blackDead.Add(occupiedPiece);
                }
            }
        }

        private bool lieOnSameDiagonal(Point p1, Point p2) => (p2.Y - p1.Y) == (p2.X - p1.X) || (p2.Y - p1.Y) == -(p2.X - p1.X);

        private void moveChecks(Point moveTo) { 
            if (selectedPiece.PieceType == PieceType.Rook)
            {
                if (selectedPiece.CurrPos == new Point(0, 0))
                {
                    ((King)blackPieces.Where(x => x.PieceType == PieceType.King).Single()).LeftCastle = false;
                }
                else if (selectedPiece.CurrPos == new Point(0, 7))
                {
                    ((King)blackPieces.Where(x => x.PieceType == PieceType.King).Single()).RightCastle = false;
                } else if (selectedPiece.CurrPos == new Point(7, 0))
                {
                    ((King)whitePieces.Where(x => x.PieceType == PieceType.King).Single()).LeftCastle = false;
                } else if (selectedPiece.CurrPos == new Point(7, 7))
                {
                    ((King)whitePieces.Where(x => x.PieceType == PieceType.King).Single()).RightCastle = false;
                }
            } else if (selectedPiece.PieceType == PieceType.King)
            {
                if (Math.Abs(moveTo.X - selectedPiece.CurrPos.X) >= 2)
                {
                    if (moveTo.X <= selectedPiece.CurrPos.X)
                    {
                        movePiece(pieces[0, selectedPiece.CurrPos.Y], new Point(moveTo.X + 1, moveTo.Y));
                    } else
                    {
                        movePiece(pieces[7, selectedPiece.CurrPos.Y], new Point(moveTo.X - 1, moveTo.Y));
                    }
                }

                ((King)selectedPiece).LeftCastle = false;
                ((King)selectedPiece).RightCastle = false;
            } else if (selectedPiece.PieceType == PieceType.Pawn)
            {
                if (Math.Abs(moveTo.Y - selectedPiece.CurrPos.Y) == 2)
                {
                    ((Pawn)selectedPiece).EnPessant = true;
                } else if (moveTo.X != selectedPiece.CurrPos.X && pieces[moveTo.X, moveTo.Y] == null)
                {
                    ChessPiece enPessantKill = pieces[moveTo.X, selectedPiece.CurrPos.Y];
                    pieces[moveTo.X, selectedPiece.CurrPos.Y] = null;
                    (WhiteTurn ? blackPieces : whitePieces).Remove(enPessantKill);
                    (WhiteTurn ? blackDead : whiteDead).Add(enPessantKill);
                    ChessForm.squares[moveTo.X, selectedPiece.CurrPos.Y].changePiece(null);
                    ((Pawn)selectedPiece).EnPessant = false;
                }else
                {
                    ((Pawn)selectedPiece).EnPessant = false;
                }
            }
        }

        public void newTurn()
        {
            turnCount++;

            foreach (ChessPiece piece in pieces)
            {
                if (piece != null)
                {
                    piece.Protected = false;
                }
            }

            checkMoves();
        }

        private void checkMoves()
        {
            Point kingPos = new Point(0, 0);
            ChessPiece king = null, otherKing = null;
            foreach (var piece in pieces)
            {
                if (piece != null)
                {
                    piece.Turn = piece.isWhite == WhiteTurn;
                    piece.PossibleMoves(pieces);

                    if (piece.PieceType == PieceType.King)
                    {
                        if (piece.isWhite == WhiteTurn)
                        {
                            kingPos = piece.CurrPos;
                            king = piece;
                        } else
                        {
                            otherKing = piece; 
                        }
                    }
                }
            }

            king.PossibleMoves(pieces);
            otherKing.PossibleMoves(pieces);

            List<ChessPiece> enemyPieces = WhiteTurn ? blackPieces : whitePieces;
            List<ChessPiece> turnPieces = WhiteTurn ? whitePieces : blackPieces;
            List<BitArray[]> attackingPieces = new List<BitArray[]>();
            bool check = false;

            BitArray[] fromKingDiagonal = Helpers.diagonalLinesFromPoint(kingPos, pieces, !WhiteTurn);
            BitArray[] fromKingVertical = Helpers.horizontalVerticleLinesFromPoint(kingPos, pieces, !WhiteTurn);

            foreach (ChessPiece enemy in enemyPieces)
            {
                if (enemy.movesAvailable[kingPos.Y][kingPos.X] == true)
                {
                    check = true;
                    if (enemy.PieceType == PieceType.Knight)
                    {
                        BitArray[] temp = new BitArray[8];
                        for (int i = 0; i< 8; i++)
                        {
                            temp[i] = new BitArray(8);
                        }

                        temp[enemy.CurrPos.Y].Set(enemy.CurrPos.X, true);
                        temp[kingPos.Y].Set(kingPos.X, true);

                        attackingPieces.Add(temp);
                    }
                    else
                    {
                        attackingPieces.Add(Helpers.lineBetweenTwoPoints(enemy.CurrPos, kingPos));
                    }
                } else if (enemy.PieceType == PieceType.Queen || enemy.PieceType == PieceType.Rook || enemy.PieceType == PieceType.Bishop)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            bool diagonalCheck = fromKingDiagonal[j][i] == true && enemy.movesAvailable[j][i] == true && pieces[i, j] != null
                                && lieOnSameDiagonal(kingPos, enemy.CurrPos);
                            
                            bool verticleCheck = fromKingVertical[j][i] == true && enemy.movesAvailable[j][i] == true && pieces[i, j] != null
                                && ((enemy.CurrPos.X == kingPos.X) || (enemy.CurrPos.Y == kingPos.Y));

                            if (diagonalCheck || verticleCheck)
                            {
                                Helpers.OperateTwoBitArrays(pieces[i, j].movesAvailable, Helpers.lineBetweenTwoPoints(enemy.CurrPos, kingPos), "AND");
                            }
                        }
                    }
                }
            }


            BitArray[] NotKingMoves = Helpers.initArray();

            pieces[kingPos.X, kingPos.Y] = null;

            Helpers.OperateListOfBitArrays(NotKingMoves, enemyPieces.Select(x => x.PossibleMovesNoChange(pieces)).ToList(), "OR");
            Helpers.OperateTwoBitArrays(NotKingMoves, null, "NOT");
            Helpers.OperateTwoBitArrays(king.movesAvailable, NotKingMoves, "AND");

            pieces[kingPos.X, kingPos.Y] = king;

            if (check)
            {
                foreach (ChessPiece turnPiece in turnPieces)
                {
                    if (turnPiece.PieceType != PieceType.King)
                    {
                        Helpers.OperateListOfBitArrays(turnPiece.movesAvailable, attackingPieces, "AND");
                    }
                }
            }

            foreach (ChessPiece turnPiece in turnPieces)
            {
                if (turnPiece.canMove())
                {
                    return;
                }
            }

            endGame(check);
        }

        public void endGame(bool draw)
        {
            if (!draw)
            {
                MessageBox.Show("Game ended in a draw");
            } else if (WhiteTurn)
            {
                MessageBox.Show("Black Wins!");
            } else
            {
                MessageBox.Show("White Wins");
            }

            ChessForm.makeNewGame();
        }

        public void Dispose()
        {
            selectedPiece = null;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ChessForm.squares[i, j].changePiece(null);
                    ChessForm.squares[i, j].SquarePictureBox.MouseClick -= OnSquareClick;

                    if (pieces[i, j] != null)
                    {
                        pieces[i, j].Dispose();
                        pieces[i, j] = null;
                    }
                }
            }

            blackDead.Clear();
            blackPieces.Clear();
            whiteDead.Clear();
            whitePieces.Clear();
        }
    }
}
