using Domain.Core;

namespace Domain
{
    public class Stuff : Entity
    {
        public string Description { get; set; }

        public Stuff(string description)
        {
            Description = description;
        }
    }
}
