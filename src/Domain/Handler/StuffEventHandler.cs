﻿using Domain.Core;
using Domain.Core.Handler;
using System.Threading.Tasks;

namespace Domain.Handler
{
    public class WhateverEventHandler : IEventHandler<Event>
    {
        public Task Handler(Event @event)
        {
            //Do here your business logic 

            return Task.CompletedTask;
        }
    }
}