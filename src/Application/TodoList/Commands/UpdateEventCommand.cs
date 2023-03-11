using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoList.Commands
{
    public class UpdateEventCommand : IRequest<int>
    {
        public int? Id { get; set; }
        
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public List<int>? Attendees { get; set; }
    }

    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, int>
    {

        private readonly IApplicationDbContext _context;

        public UpdateEventCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventItem = _context.EventItem.Where(x => x.Id == request.Id).FirstOrDefault();

            if (eventItem != null)
            {
                eventItem.Title = request.Title;
                eventItem.Description = request.Description;
                eventItem.StartTime = request.StartTime;
                eventItem.EndTime = request.EndTime;
                
                _context.EventItem.Update(eventItem);
                await _context.SaveChangesAsync(cancellationToken);

                if (request.Attendees != null)
                {
                    var eventAttendees = _context.EventAttendee.Where(x => x.EventId == request.Id).ToList();
                    _context.EventAttendee.RemoveRange(eventAttendees);
                    await _context.SaveChangesAsync(cancellationToken);

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

            return 0;
        }
    }
}
