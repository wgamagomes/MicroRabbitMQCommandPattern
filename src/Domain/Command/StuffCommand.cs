using Domain.Core.Command;
using System;

namespace Domain.Command
{
    public class StuffCommand : ICommand
    {
        public string Description { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Description);
        }
    }
}
