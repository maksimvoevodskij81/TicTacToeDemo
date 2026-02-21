using Microsoft.AspNetCore.Mvc;
using TicTacToe.Web.Api.Requests;
using TicTacToe.Web.Services;

namespace TicTacToe.Web.Api.ApiControllers
{
    [ApiController]
    [Route("games")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetGameById(Guid id)
        {
            var result = _gameService.GetState(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateGame()
        {
           var result = _gameService.Create();
            if (result is null) 
            { 
                return BadRequest(result); 
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("{id:guid}/moves")]

        public IActionResult MakeMove([FromRoute] Guid id, [FromBody] MakeMoveRequest moveRequest)
        {
            var result = _gameService.MakeMove(id, moveRequest);

            if(result is null)
            {
                return NotFound();
            }
            else if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
