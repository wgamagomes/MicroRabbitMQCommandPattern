using Domain.Core.Event;

namespace Domain.Event
{
    public class StuffEvent : IEvent
    {
        public string Description { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Description);
        }
    }
}
