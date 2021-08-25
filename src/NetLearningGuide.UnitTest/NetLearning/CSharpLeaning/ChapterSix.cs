using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.CSharpLeaning
{
    public class ChapterSix
    {
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task ImplicitTestCase1()
        {
            var test = (UtmCoordinates)(new GpsCoordinates());
            test.Test.ShouldBe("HHH");
            return Task.CompletedTask;
        }

        class GpsCoordinates
        { 
            public static implicit operator UtmCoordinates(GpsCoordinates gps)
            {
                return new UtmCoordinates()
                {
                    Test = "HHH"
                };
            }
        }

        class UtmCoordinates
        {
            public string Test { get; set; }
        }
    }
}
