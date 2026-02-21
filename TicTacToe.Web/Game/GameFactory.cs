using TicTacToe.Domain.Board.Abstractions;
using TicTacToe.Domain.Game.Abstractions;

namespace TicTacToe.Web.Game
{
    public class GameFactory : IGameFactory
    {
        private Func<IBoard> _boardFactory;
        private Func<IBoard, IGame> _gameFactory;
        public GameFactory(Func<IBoard, IGame> gameFactory, Func<IBoard> boardFactory)
        {
            _gameFactory= gameFactory;
            _boardFactory= boardFactory;
        }
        public IGame CreateGame()
        {
            var board = _boardFactory();
            var game = _gameFactory(board);
            return game;
        }
    }
}
