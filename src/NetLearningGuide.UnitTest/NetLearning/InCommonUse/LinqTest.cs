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
                        join ex in listExtend on new { li?.Name, Age = li?.Age ?? 0 } equals new { ex.Name, ex.Age } into tem
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

        [Fact]
        public Task SumCase1()
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
            var value1 = list.Where(x => x.Name == "").Sum(x => x.Age);
            value1.ShouldBe(0);
            var value2 = list.Where(x => x.Name == "James").Sum(x => x.Age);
            value2.ShouldBe(4);
            return Task.CompletedTask;
        }
        [Fact]
        public Task AnyCase1()
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
            var value1 = list.Any(x => x.Name == "");
            value1.ShouldBeFalse();
            var value2 = list.Any(x => x.Name == "James");
            value2.ShouldBeTrue();
            return Task.CompletedTask;
        }

        [Fact]
        public Task ConcatCase1()
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
            var listConcat = new List<LinqModelFinal>()
            {
                new LinqModelFinal(){Age = 3,CreateDate = DateTime.Today,Marks = "Test",Name = "Apple"}
            };
            var name = (from model in list where model.Name == "James" select model.Name)
                .Concat(from linqModelFinal in listConcat select linqModelFinal.Name).Distinct().ToList();
            name.Count.ShouldBe(2);
            name.ShouldContain("James");
            name.ShouldContain("Apple");
            return Task.CompletedTask;
        }

        [Fact]
        public Task CalculateCase1()
        {
            List<decimal> input = new List<decimal>() { 1, 2.1m, 4 };
            var query = (from put in input select put * 2 * 3.14m).ToList();
            query.ShouldContain(6.28M);
            query.ShouldContain(13.188M);
            query.ShouldContain(25.12M);
            return Task.CompletedTask;
        }
    }
}
