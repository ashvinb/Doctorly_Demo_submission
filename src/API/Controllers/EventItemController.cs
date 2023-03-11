using Application.TodoList.Commands;
using Application.TodoList.Query;
using Microsoft.AspNetCore.Mvc;

namespace Doctorly_Demo.Controllers
{
    public class EventItemController : ApiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<EventViewModel>>> GetEventItems([FromQuery] GetEventsQuery query)
        {
            return await Mediator.Send(query);
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateEventCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<int>> Cancel(CreateEventCancellationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<int>> Update(UpdateEventCommand command)
        {
            return await Mediator.Send(command);
        }


    }
}
