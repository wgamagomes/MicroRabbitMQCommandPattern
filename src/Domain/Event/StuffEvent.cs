using Domain.Core;

namespace Domain
{
    public class StuffEvent : Event
    {
        public string Description { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Description);
        }
    }
}
