

namespace TicTacToe.Domain.Game.Contracts
{
    public enum PlayerSymbol
    {
       Empty = 0, X = 1, O = 2
    }

    public enum GameStatus
    {
        InProgress = 0, Won = 1, Draw = 2
    }

    public sealed record GameState(GameStatus Status, PlayerSymbol CurrentPlayer, PlayerSymbol? Winner, PlayerSymbol[] GameBoard);

    public sealed record MoveResult(bool IsSuccess, string? ErrorCode, string? ErrorMessage, GameState GameState);
}
