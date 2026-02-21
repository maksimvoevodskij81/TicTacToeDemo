using TicTacToe.Domain.Game.Abstractions;
using TicTacToe.Domain.Game.Contracts;
using TicTacToe.Web.Api.Requests;
using TicTacToe.Web.Api.Responses;
using TicTacToe.Web.Game;

namespace TicTacToe.Web.Services
{
    public class GameService : IGameService
    {
        private Dictionary<Guid, IGame> _games;
        private readonly IGameFactory _gameFactory;
        public GameService(IGameFactory gameFactory)
        {
            _games = new Dictionary<Guid, IGame>();
            _gameFactory = gameFactory;
        }

        public StateResponse Create()
        {
            var game = _gameFactory.CreateGame();
            var state = game.Start();
            var gameId = Guid.NewGuid();
            _games.Add(gameId, game);

            return new StateResponse(gameId, state);
        }

        public StateResponse? GetState(Guid id)
        {
            if (!_games.TryGetValue(id, out var game))
            {
                return null;
            }

            return new StateResponse(id, game.GetState());
        }

        public MoveResult? MakeMove(Guid id, MakeMoveRequest makeMoveRequest)
        {
            if (!_games.TryGetValue(id, out var game))
            {
                return null;
            }

            var moveResult = game.MakeMove(makeMoveRequest.Player, makeMoveRequest.Row, makeMoveRequest.Column);

            return moveResult;
        }
    }
}
