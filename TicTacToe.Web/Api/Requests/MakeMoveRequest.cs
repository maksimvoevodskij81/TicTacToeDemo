using TicTacToe.Domain.Game.Contracts;

namespace TicTacToe.Web.Api.Requests
{
    public sealed record MakeMoveRequest(PlayerSymbol Player, int Row, int Column);
 
}
