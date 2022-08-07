using API.Configuration;
using Application.DtoModels.Models.UpdatePlayer;

namespace API.Controllers
{
    public class PlayerController : BaseController
    {
        #region Properties

        private readonly ISender _mediator;

        #endregion

        public PlayerController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdatePlayerDto item)
        {
            item.Id = id;
            await _mediator.Send(item);
            return NoContent();
        }
    }
}
