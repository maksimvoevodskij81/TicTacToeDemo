using TicTacToe.Domain.Game.Contracts;

namespace TicTacToe.Domain.Game.Abstractions
{
    public interface IGame
    {
        public GameState Start();
        public MoveResult MakeMove(PlayerSymbol player, int row, int column);
        public GameState GetState();
    }
}
