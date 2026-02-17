

namespace TicTacToe.Domain.Game.Contracts
{
    public enum PlayerSymbol
    {
       Empty = 0, X = 1, O = 2
    }

    public enum GameStatus
    {
        NotStarted = 0, InProgress = 1, Won = 2, Draw = 3
    }

    public sealed record GameState(GameStatus Status, PlayerSymbol CurrentPlayer, PlayerSymbol? Winner, PlayerSymbol[] GameBoard);

    public sealed record MoveResult(bool IsSuccess, string? ErrorCode, string? ErrorMessage, GameState GameState);
}
