using NetLearningGuide.Util;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class LinqTest
    {
        [Fact]
        public Task DistinctTestCase1()
        {
            var list = new List<LinqModel>()
            {
                new LinqModel(){Name = "James",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "James",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "James",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
            };
            list = list.Distinct().ToList();
            list.Count.ShouldBe(6);
            return Task.CompletedTask;
        }
        [Fact]
        public Task DistinctTestCase2()
        {
            var list = new List<LinqModel>()
            {
                new LinqModel(){Name = "James",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "James",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "James",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
            };
            list = list.DistinctBy(p => new { p.Name, p.Age, p.CreateDate }).ToList();
            list.Count.ShouldBe(4);
            return Task.CompletedTask;
        }
        [Fact]
        public Task GroupByTestCase1()
        {
            var list = new List<LinqModel>()
            {
                new LinqModel(){Name = "James",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "James",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "James",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
            };
            list = list.GroupBy(p => new { p.Name, p.Age, p.CreateDate }).Select(x => x.FirstOrDefault()).ToList();
            list.Count.ShouldBe(4);
            return Task.CompletedTask;
        }
        public class LinqModel
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime CreateDate { get; set; }
        }
        [Fact]
        public Task GroupByTestCase2()
        {
            var list = new List<LinqModel>()
            {
                new LinqModel(){Name = "James",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "James",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "James",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
            };
            list = list.GroupBy(p => new LinqModel { Name = p.Name, Age = p.Age, CreateDate = p.CreateDate }).Select(x => x.FirstOrDefault()).ToList();
            list.Count.ShouldBe(6);
            return Task.CompletedTask;
        }
    }
}
