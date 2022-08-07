using API.Configuration;
using Application.DtoModels.Models.GetBoard;

namespace API.Controllers
{
    public class BoardController : BaseController
    {
        #region Properties

        private readonly ISender _mediator;

        #endregion

        public BoardController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get a board of the game with the configuration
        /// </summary>
        /// <param name="id">the if of board</param>
        /// <returns>a object IActionResult with the result of call to the api</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetBoardByIdDto { Id = id }));
        }
    }
}
