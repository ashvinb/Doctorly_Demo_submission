using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoList.Query
{

    public class GetEventsQuery : IRequest<List<EventViewModel>>
    {
        public int? ListLength { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }

    public class GetEventsQueryCommandHandler : IRequestHandler<GetEventsQuery, List<EventViewModel>>
    {

        private readonly IApplicationDbContext _context;

        public GetEventsQueryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventViewModel>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var eventItem = _context.EventItem.Take(request.ListLength ?? 10);

            if (request.StartDate != null && request.EndDate == null)
            {
                eventItem = eventItem.Where(x => x.StartTime >= request.StartDate);
            }

            if (request.StartDate != null && request.EndDate != null)
            {
                eventItem = eventItem.Where(x => x.StartTime >= request.StartDate && x.EndTime <= request.EndDate);
            }

            var result = await eventItem.Select(x => new EventViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                StartDate = x.StartTime,
                EndDate = x.EndTime,
            }).ToListAsync();

            return result;
        }

        public async Task<List<AttendeeViewModel>> GetAttendees(int eventId)
        {
            var attendeeIds = await _context.EventAttendee
                .Where(x => x.EventId == eventId)
                .Select(x => x.AttendeeId)
                .ToListAsync();

            var attendees = await _context.Attendee
                .Where(x => attendeeIds.Contains(x.Id))
                .Select(x => new AttendeeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.EmailAddress
                })
                .ToListAsync();

            return attendees;
        }

        
    }
}
