using TicTacToe.Domain.Board.Abstractions;
using TicTacToe.Domain.Game.Abstractions;
using TicTacToe.Domain.Game.Contracts;

namespace TicTacToe.Web.Game
{
    public class InMemoryGame : IGame
    {
        private readonly IBoard _board;
        private PlayerSymbol _currentPlayer;
        public InMemoryGame(IBoard board) 
        {
            _board = board;
            _currentPlayer = PlayerSymbol.X;
        }

        public GameState GetState()
        {
            var winner = _board.CheckWinner();
            var board = _board.GetBoard();
            if (winner != null)
            {
                return  new GameState(GameStatus.Won, _currentPlayer, winner, board);
            }

            var status = _board.IsFull() ? GameStatus.Draw : GameStatus.InProgress;
            return new GameState(status, _currentPlayer, null, board);
        }

        public MoveResult MakeMove(PlayerSymbol player, int row, int column)
        {
            var state = GetState();

            switch (state.Status)
            {
                case GameStatus.Won: return new MoveResult(false, "GameFinished", "Game is already won.", state);
                case GameStatus.InProgress: return DoMove(player, row, column, state);
                case GameStatus.Draw: return new MoveResult(false, "GameFinished", "Game is a draw.", state);

                default: return new MoveResult(false, "InvalidGameStatus", $"Unsupported status: {state.Status}", state);
            }

        }

        public GameState Start()
        {
            _board.GetBoard();
            return GetState();  
        }

        private MoveResult DoMove(PlayerSymbol player, int row, int column, GameState gameState)
        {
            if (player != _currentPlayer)
            {
                return new MoveResult(false, "NotYourTurn", "It is not your turn", gameState);
            }

            var boardUpdate = _board.TryPlace(row, column, player);

            if (!boardUpdate.IsSuccess)
            {
                return new MoveResult(false, boardUpdate.ErrorCode, boardUpdate.ErrorMessage, gameState);
            }

            var winner = _board.CheckWinner();
          
            var isDraw = winner == null && _board.IsFull();

            if (winner == null && !isDraw)
            {
                _currentPlayer = _currentPlayer == PlayerSymbol.X ? PlayerSymbol.O : PlayerSymbol.X;
            }

            var newState = GetState();
            return new MoveResult(true, null, null, newState);
        }
    }
}
