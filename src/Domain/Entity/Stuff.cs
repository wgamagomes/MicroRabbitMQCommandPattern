using Domain.Core;

namespace Domain
{
    public class Stuff : Entity
    {
        public string OwnerName { get; set; }

        public Stuff(string ownerName)
        {
            OwnerName = ownerName;
        }
    }
}
