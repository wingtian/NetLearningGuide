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

        public class LinqModelExtend
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Marks { get; set; }
        }
        public class LinqModelFinal
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime CreateDate { get; set; }
            public string Marks { get; set; }
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

        [Fact]
        public Task SelectTestCase1()
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
            var listExtend = new List<LinqModelExtend>()
            {
                new LinqModelExtend(){Name = "James",Age = 1,Marks = "Test1"},
                new LinqModelExtend(){Name = "James",Age = 2,Marks = "Test2"},
                new LinqModelExtend(){Name = "Tom",Age = 2,Marks = "Test2"},
            };

            var temp = (from li in list
                        join ex in listExtend on new {li?.Name, Age = li == null ? 0 : li.Age } equals new {ex.Name, ex.Age } into tem
                        from main in tem.DefaultIfEmpty()
                        select new LinqModelFinal()
                        {
                            Name = li.Name,
                            Age = li.Age,
                            Marks = main?.Marks,
                            CreateDate = li.CreateDate
                        })
                .GroupBy(p => new { p.Name, p.Age, p.CreateDate })
                .Select(x => x.FirstOrDefault()).ToList();
            temp.Any(x => x.Name == "James" && x.Age == 1 && x.Marks == "Test1").ShouldBeTrue();
            temp.Any(x => x.Name == "James" && x.Age == 2 && x.Marks == "Test2").ShouldBeTrue();
            temp.Any(x => x.Name == "Gluee" && x.Age == 1 && string.IsNullOrEmpty(x.Marks)).ShouldBeTrue();
            temp.Any(x => x.Name == "Gluee" && x.Age == 2 && string.IsNullOrEmpty(x.Marks)).ShouldBeTrue();
            return Task.CompletedTask;
        }
    }
}
