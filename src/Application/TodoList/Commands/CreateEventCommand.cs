using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoList.Commands
{
    public class CreateEventCommand : IRequest<int>
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public List<int>? Attendees { get; set; }
    }

    public class CreateEventCommandCommandHandler : IRequestHandler<CreateEventCommand, int>
    {

        private readonly IApplicationDbContext _context;

        public CreateEventCommandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventItem = new Domain.Entities.EventItem();

            eventItem.Title = request.Title;
            eventItem.Description = request.Description;
            eventItem.StartTime = request.StartTime;
            eventItem.EndTime = request.EndTime;

            _context.EventItem.Add(eventItem);

            var id = await _context.SaveChangesAsync(cancellationToken);

            if(id > 0)
            {
                foreach (var attendeeId in request.Attendees)
                {
                    var eventAttendee = new Domain.Entities.EventAttendee();
                    eventAttendee.EventId = eventItem.Id;
                    eventAttendee.AttendeeId = attendeeId;
                    _context.EventAttendee.Add(eventAttendee);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return eventItem.Id;
        }
    }
}
