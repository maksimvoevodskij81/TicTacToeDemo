
using TicTacToe.Domain.Game.Contracts;

namespace TicTacToe.Domain.Board.Contracts
{
    public sealed record PlaceCellResult(bool IsSuccess, string? ErrorCode, string? ErrorMessage);

}
