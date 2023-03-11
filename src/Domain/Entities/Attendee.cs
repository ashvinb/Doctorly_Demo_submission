using Domain.Common;

namespace Domain.Entities
{
    public class Attendee : BaseEntity
    {
        public string? Name { get; set; }
        
        public string? EmailAddress { get; set; }
    }
}
