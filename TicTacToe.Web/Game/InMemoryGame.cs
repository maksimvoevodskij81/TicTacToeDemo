using TicTacToe.Domain.Game.Abstractions;
using TicTacToe.Domain.Game.Contracts;
using TicTacToe.Web.Board.Constants;

namespace TicTacToe.Web.Game
{
    public class InMemoryGame : IGame
    {
        private PlayerSymbol [,] _board;
        private PlayerSymbol? _winner;
        private PlayerSymbol _currentPlayer;
        private GameStatus _gameStatus;
        public InMemoryGame() 
        {

            _board = new PlayerSymbol[BoardConstants.BoardSize, BoardConstants.BoardSize];
            _currentPlayer = PlayerSymbol.X;
            _winner = null;
            _gameStatus = GameStatus.NotStarted;
        }
        public GameState GetState()
        {
            var result = new GameState(_gameStatus, _currentPlayer, _winner, Flat2DBoard());
            return result;
        }

        public MoveResult MakeMove(PlayerSymbol player, int row, int column)
        {
            if (player != _currentPlayer)
            {
                return new MoveResult(false, "400", "It is not your turn", GetState());
            }
            _board.
            return new MoveResult();
        }

        public GameState Start()
        {
            BuildBoard();

            return GetState();  
        }

        private void BuildBoard()
        {
            for (int rowIndex = 0; rowIndex < BoardConstants.BoardSize; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < BoardConstants.BoardSize; columnIndex++)
                {
                    _board[rowIndex,columnIndex] =  PlayerSymbol.Empty;
                }
            }
        }

        private PlayerSymbol[] Flat2DBoard()
        {
            return _board.Cast<PlayerSymbol>().ToArray();
        }


    }
}
