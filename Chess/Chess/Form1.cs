using Chess.Chess_Piece;

namespace Chess
{
    public partial class ChessForm : Form
    {
        private static ChessGame currGame;

        public ChessForm()
        {
            InitializeComponent();
            createSquares();
            currGame = new ChessGame(squares);
        }

        public static void makeNewGame()
        {
            currGame.Dispose();
            currGame = new ChessGame(squares);
        }
    }
}