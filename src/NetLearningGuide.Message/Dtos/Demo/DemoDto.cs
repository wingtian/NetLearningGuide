using System;

namespace NetLearningGuide.Message.Dtos.Demo
{
    public class DemoDto
    {
        public DemoDto(Guid id)
        {
            Id = id;
        }

        public Guid? Id { get; set; }
    }
}
