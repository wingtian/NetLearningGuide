using Mediator.Net.Contracts;
using System;
using System.Collections.Generic;

namespace NetLearningGuide.Message.Commands.Demo
{
    public class DemoMappingCommand : ICommand
    {
        public string UserName { get; set; }

        public DateTime Birthday { get; set; }

        public int Age { get; set; }

        public List<string> Relations { get; set; }
    }
}
