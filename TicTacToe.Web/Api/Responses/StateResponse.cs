using TicTacToe.Domain.Game.Contracts;

namespace TicTacToe.Web.Api.Responses
{
    public sealed record StateResponse(Guid GameId, GameState State);

}
