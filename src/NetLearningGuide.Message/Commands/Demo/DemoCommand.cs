﻿using Mediator.Net.Contracts;
using System;

namespace NetLearningGuide.Message.Commands.Demo
{
    public class DemoCommand : ICommand
    {
        public DemoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
