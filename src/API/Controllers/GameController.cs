using API.Configuration;
using Application.DtoModels.Models.AddGame;
using Application.DtoModels.Models.GetGame;
using Application.DtoModels.Models.UpdateGame;

namespace API.Controllers
{
    public class GameController : BaseController
    {
        #region Properties

        private readonly ISender _mediator;

        #endregion
        public GameController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Create a new Game
        /// </summary>
        /// <param name="item">the information of game</param>
        /// <returns>a object IActionResult with the result of call to the api</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [HttpPost]
        public async Task<IActionResult> Add(AddGameDto item)
        {
            return Created(string.Empty, await _mediator.Send(item));
        }

        /// <summary>
        /// Get a game by Id of game
        /// </summary>
        /// <param name="id">the if of game</param>
        /// <returns>a object IActionResult with the result of call to the api</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetGameByIdDto { Id = id }));
        }

        /// <summary>
        /// Update a game for finish
        /// </summary>
        /// <param name="id">the id of game</param>
        /// <param name="item">a object UpdateGameDto with the information for update the game</param>
        /// <returns>a object IActionResult with the result of call to the api</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateGameDto item)
        {
            item.Id = id;
            await _mediator.Send(item);
            return NoContent();
        }
    }
}
