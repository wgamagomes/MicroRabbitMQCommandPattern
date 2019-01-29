using Domain.Core;
using Domain.Core.Handler;
using System.Threading.Tasks;

namespace Domain.Handler
{
    public class WhateverEventHandler : IEventHandler<StuffEvent>
    {
        public Task Handler(StuffEvent @event)
        {
            //Do here your business logic 

            return Task.CompletedTask;
        }
    }
}
