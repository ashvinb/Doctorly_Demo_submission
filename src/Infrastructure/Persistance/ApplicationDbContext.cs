using System.Reflection;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator)
        : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<EventItem> EventItem => Set<EventItem>();

        public DbSet<EventAttendee> EventAttendee => Set<EventAttendee>();

        public DbSet<Attendee> Attendee => Set<Attendee>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            //await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
