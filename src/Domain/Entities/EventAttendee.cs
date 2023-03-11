using Domain.Common;

namespace Domain.Entities
{
    public class EventAttendee : BaseEntity
    {
        public int? EventId { get; set; }
        
        public int? AttendeeId { get; set; }

        public bool? IsAttendeeConfirmed { get; set; }
    }
}
