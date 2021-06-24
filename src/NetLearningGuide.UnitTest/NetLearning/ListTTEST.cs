using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning
{
    public class ListTtest
    {
        [Fact]
        public Task ListTValueAssignmentTestCase1()
        {
            var list = new List<InputTest>()
            {
                new InputTest() { Id = "abc", Age = 1, Time = DateTime.MinValue } ,
                new InputTest() { Id = "bcd", Age = 2, Time = DateTime.MinValue.AddYears(1) }
            };
            var model = list.FirstOrDefault(x => x.Id == "abc");
            if (model != null)
            {
                model.Id = "efg";
                model.Age = 3;
                model.Time = DateTime.Now;
                list.Any(x => x.Id == model.Id).ShouldBeTrue();
                list.Any(x => x.Age == model.Age).ShouldBeTrue();
                list.Any(x => x.Time == model.Time).ShouldBeTrue();
            }
            return Task.CompletedTask;
        }
        [Fact]
        public Task ListTValueAssignmentTestCase2()
        {
            var list = new List<InputTest>()
            {
                new InputTest() { Id = "abc", Age = 1, Time = DateTime.MinValue } ,
                new InputTest() { Id = "bcd", Age = 2, Time = DateTime.MinValue.AddYears(1) }
            };
            var model = list.FirstOrDefault(x => x.Id == "abc");
            var list2 = new List<InputTest>() { model };
            list2.ForEach(y =>
            {
                y.Id = "efg";
                y.Age = 3;
                y.Time = DateTime.Now;
                list.Any(x => x.Id == y.Id).ShouldBeTrue();
                list.Any(x => x.Age == y.Age).ShouldBeTrue();
                list.Any(x => x.Time == y.Time).ShouldBeTrue();
            });
            return Task.CompletedTask;
        }
    private class InputTest
    {
        public string Id { get; set; }
        public int Age { get; set; }
        public DateTime Time { get; set; }
    }
}
}
