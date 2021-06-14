using System;
using System.Collections.Generic;

namespace NetLearningGuide.Message.Dtos.Demo
{
    public class DemoMappingDto
    {
        public string UserName { get; set; }

        public DateTime UserBirthday { get; set; }

        public int UserAge { get; set; }

        public List<string> Relation { get; set; }
        public string Comment { get; set; }
    }
}
