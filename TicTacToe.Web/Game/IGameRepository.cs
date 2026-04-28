
using TicTacToe.Domain.Game.Contracts;

namespace TicTacToe.Web.Game
{
    public interface IGameRepository
    {
        public Guid Create();
        public GameState GetById(Guid id);
        public Guid Save(Guid id, byte[] expectedRowVersion, GameState gameEntity);
    }
}
