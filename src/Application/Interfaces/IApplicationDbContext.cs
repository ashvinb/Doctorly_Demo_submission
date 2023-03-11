using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<Domain.Entities.EventItem> EventItem { get; }

        DbSet<Domain.Entities.EventAttendee> EventAttendee { get; }

        DbSet<Domain.Entities.Attendee> Attendee { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
