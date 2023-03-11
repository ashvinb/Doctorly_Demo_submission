using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoList.Commands
{
    public class CreateEventCancellationCommand : IRequest<int>
    {
        public int? Id { get; set; }

    }

    public class CreateEventCancellationCommandCommandHandler : IRequestHandler<CreateEventCancellationCommand, int>
    {

        private readonly IApplicationDbContext _context;

        public CreateEventCancellationCommandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateEventCancellationCommand request, CancellationToken cancellationToken)
        {
            var eventItem = _context.EventItem.Where(x => x.Id == request.Id).FirstOrDefault();

            if (eventItem != null)
            {
                eventItem.IsDeleted = true;
                _context.EventItem.Update(eventItem);
                await _context.SaveChangesAsync(cancellationToken);
                return eventItem.Id;
            }

            return 0;
        }
    }
}
