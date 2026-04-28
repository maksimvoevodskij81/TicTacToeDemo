
namespace TicTacToe.Infrastructure.Persistence.Entities
{
    public class GameEntity
    {
        public Guid Id { get; set; }
        public string StateJson { get; set; } = "";
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
