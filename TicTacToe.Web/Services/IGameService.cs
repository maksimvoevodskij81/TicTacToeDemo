using TicTacToe.Domain.Game.Contracts;
using TicTacToe.Web.Api.Requests;
using TicTacToe.Web.Api.Responses;

namespace TicTacToe.Web.Services
{
    public interface IGameService
    {
        public StateResponse Create();
        public StateResponse? GetState(Guid id);
        public MoveResult? MakeMove(Guid id, MakeMoveRequest moveRequest);
    }
}
