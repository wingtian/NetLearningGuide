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
        public Task JoinTestCase1()
        {
            var list = new List<LinqModel>()
            {
                new LinqModel(){Name = "James",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Vins",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Luke",Age = 1,CreateDate = DateTime.Today},
            };
            var listExtend = new List<LinqModelExtend>()
            {
                new LinqModelExtend(){Name = "James",Age = 1,Marks = "Test1"},
                new LinqModelExtend(){Name = "Tom",Age = 2,Marks = "Test2"},
                new LinqModelExtend(){Name = "AAA",Age = 2,Marks = "Test2"},
                new LinqModelExtend(){Name = "BB",Age = 2,Marks = "Test2"},
                new LinqModelExtend(){Name = "CC",Age = 2,Marks = "Test2"},
                new LinqModelExtend(){Name = "EE",Age = 2,Marks = "Test2"},
                new LinqModelExtend(){Name = "DD",Age = 2,Marks = "Test2"},
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
                        }).ToList();
            temp.Count.ShouldBe(4);
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

        [Fact]
        public Task FirstCase1()
        {
            try
            {
                var first = new List<decimal>().First();
                first.ShouldNotBe(0);
            }
            catch (Exception e)
            {
                e.Message.ShouldBe("Sequence contains no elements");
            }
            return Task.CompletedTask;
        }
        [Fact]
        public Task FirstOrDefaultCase1()
        {
            var model = new List<decimal>().FirstOrDefault();
            model.ShouldBe(default);
            return Task.CompletedTask;
        }
        [Fact]
        public Task SumCase2()
        {
            var model = new List<decimal>().Sum();
            model.ShouldBe(default);
            return Task.CompletedTask;
        }
        [Fact]
        public Task MaxCase1()
        {
            try
            {
                var model = new List<decimal>().Max();
                model.ShouldBe(default);
            }
            catch (Exception e)
            {
                e.Message.ShouldBe("Sequence contains no elements");
            }
            return Task.CompletedTask;
        }
        [Fact]
        public Task MaxCase2()
        {
            var model = new List<decimal>() { 1, 0 }.Max();
            model.ShouldBe(1);
            return Task.CompletedTask;
        }

        [Fact]
        public Task CountCase1()
        {
            new List<LinqModel>().Count(x => x.Age == 1).ShouldBe(0);
            return Task.CompletedTask;
        }
        [Fact]
        public Task JoinCase1()
        {
            var input = new List<InputCase>()
            {
                new InputCase(){Werks = "1234" ,Material = "123C",Description = "TESTT",Stock = 2},
                new InputCase(){Werks = "1234" ,Material = "123T",Description = "TESTd",Stock = 3},
                new InputCase(){Werks = "1234" ,Material = "123A",Description = "TESTA",Stock = 4},
            };
            var material = new List<MaterialTestCase>()
            {
                new MaterialTestCase(){Werks = "1234" ,Material = "123C",Description = "TEST",Stock = 1,Region = "123"},
                new MaterialTestCase(){Werks = "1234" ,Material = "123T",Description = "TEST",Stock = 1,Region = "123"},
                new MaterialTestCase(){Werks = "1234" ,Material = "123B",Description = "TEST",Stock = 1,Region = "123"},
            };
            var joinData = material.Join(input, p => new { p.Werks, p.Material }, c => new { c.Werks, c.Material },
                (ip, mt) =>
                {
                    var returnModel = ip;
                    returnModel.Description = mt.Description;
                    returnModel.Stock = mt.Stock;
                    return returnModel;
                }).ToList();
            joinData.Count.ShouldBe(2);
            joinData.Any(x => x.Werks == "1234" && x.Material == "123C" && x.Description == "TESTT" && x.Stock == 2 && x.Region == "123").ShouldBeTrue();
            joinData.Any(x => x.Werks == "1234" && x.Material == "123T" && x.Description == "TESTd" && x.Stock == 3 && x.Region == "123").ShouldBeTrue();
            return Task.CompletedTask;
        }
        [Fact]
        public Task JoinCase2()
        {
            var input = new List<InputCase>()
            {
                new InputCase(){Werks = "1234" ,Material = "123C",Description = "TESTT",Stock = 2},
                new InputCase(){Werks = "1234" ,Material = "123C",Description = "TESTT",Stock = 2},
                new InputCase(){Werks = "1234" ,Material = "123T",Description = "TESTd",Stock = 3},
                new InputCase(){Werks = "1234" ,Material = "123A",Description = "TESTA",Stock = 4},
            };
            var material = new List<MaterialTestCase>()
            {
                new MaterialTestCase(){Werks = "1234" ,Material = "123C",Description = "TEST",Stock = 1,Region = "123"},
                new MaterialTestCase(){Werks = "1234" ,Material = "123T",Description = "TEST",Stock = 1,Region = "123"},
                new MaterialTestCase(){Werks = "1234" ,Material = "123B",Description = "TEST",Stock = 1,Region = "123"},
            };
            var joinData = material.Join(input, p => new { p.Werks, p.Material }, c => new { c.Werks, c.Material },
                (ip, mt) =>
                {
                    var returnModel = ip;
                    returnModel.Description = mt.Description;
                    returnModel.Stock = mt.Stock;
                    return returnModel;
                }).ToList();
            joinData.Count.ShouldBe(3);
            joinData.Any(x => x.Werks == "1234" && x.Material == "123C" && x.Description == "TESTT" && x.Stock == 2 && x.Region == "123").ShouldBeTrue();
            joinData.Any(x => x.Werks == "1234" && x.Material == "123T" && x.Description == "TESTd" && x.Stock == 3 && x.Region == "123").ShouldBeTrue();
            return Task.CompletedTask;
        }
        [Fact]
        public Task GroupJoinCase1()
        {
            var input = new List<InputCase>()
            {
                new InputCase(){Werks = "1234" ,Material = "123C",Description = "TESTT",Stock = 2},
                new InputCase(){Werks = "1234" ,Material = "123C",Description = "TESTT",Stock = 2},
                new InputCase(){Werks = "1234" ,Material = "123T",Description = "TESTd",Stock = 3},
                new InputCase(){Werks = "1234" ,Material = "123T",Description = "TESTd",Stock = 3},
                new InputCase(){Werks = "1234" ,Material = "123A",Description = "TESTA",Stock = 4},
            };
            var material = new List<MaterialTestCase>()
            {
                new MaterialTestCase(){Werks = "1234" ,Material = "123C",Description = "TEST",Stock = 1,Region = "123"},
                new MaterialTestCase(){Werks = "1234" ,Material = "123T",Description = "TEST",Stock = 1,Region = "123"},
                new MaterialTestCase(){Werks = "1234" ,Material = "123B",Description = "TEST",Stock = 1,Region = "123"},
            };
            var joinData = material.GroupJoin(input, p => new { p.Werks, p.Material }, c => new { c.Werks, c.Material },
                (ip, mt) =>
                {
                    var returnModel = ip;
                    var inputCases = mt.ToList();
                    if (inputCases.Any())
                    {
                        returnModel.Description = inputCases.FirstOrDefault()?.Description;
                        returnModel.Stock = inputCases.Sum(x => x.Stock);
                    }
                    return returnModel;
                }).ToList();
            joinData.Count.ShouldBe(3);
            joinData.Any(x => x.Werks == "1234" && x.Material == "123C" && x.Description == "TESTT" && x.Stock == 4 && x.Region == "123").ShouldBeTrue();
            joinData.Any(x => x.Werks == "1234" && x.Material == "123T" && x.Description == "TESTd" && x.Stock == 6 && x.Region == "123").ShouldBeTrue();
            joinData.Any(x => x.Werks == "1234" && x.Material == "123B" && x.Description == "TEST" && x.Stock == 1 && x.Region == "123").ShouldBeTrue();
            return Task.CompletedTask;
        }
        public class InputCase
        {
            public string Werks { get; set; }
            public string Material { get; set; }
            public string Description { get; set; }
            public int Stock { get; set; }
        }

        public class MaterialTestCase
        {
            public string Werks { get; set; }
            public string Material { get; set; }
            public string Description { get; set; }
            public int Stock { get; set; }
            public string Region { get; set; }
        }
        [Fact]
        public Task CaseTestCase1()
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
            var query = list.Where(x => x.Name == "james").ToList();
            query.Count.ShouldBe(0);
            query = list.Where(x => x.Name == "James").ToList();//区分大小写
            query.Count.ShouldBe(3);
            return Task.CompletedTask;
        }
        [Fact]
        public Task CaseTestCase2()
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
            var query = list.Where(x => x.Name.Equals("james", StringComparison.OrdinalIgnoreCase)).ToList();//不区分大小写
            query.Count.ShouldBe(3);
            query = list.Where(x => x.Name.Equals("James", StringComparison.OrdinalIgnoreCase)).ToList();//不区分大小写
            query.Count.ShouldBe(3);
            return Task.CompletedTask;
        }
        [Fact]
        public Task GroupByTestCase3()
        {
            var list = new List<LinqModel>()
            {
                new LinqModel(){Name = "James",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "James",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "James",Age = 2,CreateDate = DateTime.Today},
                new LinqModel(){Name = "Gluee",Age = 1,CreateDate = DateTime.Today},
            };
            var test = list.GroupBy(x => x.Age)
                 .Select(g => new
                 { 
                     Age = g.Sum(t => t.Age)
                 }).ToList();
            //这里结果很奇怪
            test.Count.ShouldBe(2);
            test.Any(x=>x.Age == 6).ShouldBeTrue();
            test.Any(x => x.Age == 3).ShouldBeTrue();
            return Task.CompletedTask;
        }
    }
}
