using TicTacToe.Domain.Game.Abstractions;

namespace TicTacToe.Web.Game
{
    public interface IGameFactory
    {
        public IGame CreateGame();
    }
}
