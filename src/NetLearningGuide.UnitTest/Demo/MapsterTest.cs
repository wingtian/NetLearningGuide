using Mapster;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.Demo
{
    public class MapsterTest
    {
        [Fact]
        public Task MapSingleTest()
        {
            var inPut = new TestMapster()
            {
                Id = Guid.NewGuid().ToString(),
                Ta = 1.11m,
                Time = DateTime.Now
            };
            var outPut = inPut.Adapt<TestMapster>();
            inPut.Id = Guid.NewGuid().ToString();
            inPut.Ta = 2.22m;
            inPut.Time = DateTime.Now.AddDays(1);
            outPut.Id.ShouldNotBe(inPut.Id);
            outPut.Ta.ShouldNotBe(inPut.Ta);
            outPut.Time.ShouldNotBe(inPut.Time);
            return Task.CompletedTask;
        }

        [Fact]
        public Task MapListTest()
        {
            var inPut = new List<TestMapster>() {
                new TestMapster()
                {
                    Id = Guid.NewGuid().ToString(),
                    Ta = 1.11m,
                    Time = DateTime.Now
                },
                new TestMapster()
                {
                    Id = Guid.NewGuid().ToString(),
                    Ta = 3.33m,
                    Time = DateTime.Now
                },
            };
            var outPut = inPut.Adapt<List<TestMapster>>();
            inPut.ForEach(x =>
            {
                x.Id = Guid.NewGuid().ToString();
                x.Ta = 2.22m;
                x.Time = DateTime.Now.AddDays(1);
            });
            outPut.ForEach(x =>
            {
                inPut.Any(y => y.Id == x.Id).ShouldBeFalse();
                inPut.Any(y => y.Ta == x.Ta).ShouldBeFalse();
                inPut.Any(y => y.Time == x.Time).ShouldBeFalse();
            });
            return Task.CompletedTask;
        }

        [Fact]
        public Task MappingTestCase1()
        {
            var inPut = new TestMapster()
            {
                Id = Guid.NewGuid().ToString(),
                Ta = 1.11m,
                Time = DateTime.Now
            };
            var output = inPut.Adapt<TestMapping>();
            output.Id.ShouldBe(inPut.Id);
            output.Ta.ShouldBe(inPut.Ta);
            output.Time.ShouldBe(inPut.Time);
            return Task.CompletedTask;
        }
        [Fact]
        public Task MappingTestCase2()
        {
            TypeAdapterConfig<TestMapster, TestMapping>.NewConfig()
                .Map(s => s.TestId, d => d.Id)
                .Map(s => s.TestTa, d => d.Ta)
                .Map(s => s.TestTime, d => d.Time);
            var inPut = new TestMapster()
            {
                Id = Guid.NewGuid().ToString(),
                Ta = 1.11m,
                Time = DateTime.Now
            };
            var output = inPut.Adapt<TestMapping>();
            output.Id.ShouldBe(inPut.Id);
            output.Ta.ShouldBe(inPut.Ta);
            output.Time.ShouldBe(inPut.Time);
            output.TestId.ShouldBe(inPut.Id);
            output.TestTa.ShouldBe(inPut.Ta);
            output.TestTime.ShouldBe(inPut.Time);
            return Task.CompletedTask;
        }
        [Fact]
        public Task MappingTestCase3()
        {
            var inPut = new List<TestMapsterUpper>() {
                new TestMapsterUpper()
                {
                    Id = Guid.NewGuid().ToString(),
                    Ta = 1.11m,
                    Time = DateTime.Now
                },
                new TestMapsterUpper()
                {
                    Id = Guid.NewGuid().ToString(),
                    Ta = 3.33m,
                    Time = DateTime.Now
                },
            };
            var outPut = inPut.Adapt<List<TestMappingUpper>>();
            var inPuts = new TestMapsterUpper()
            {
                Id = Guid.NewGuid().ToString(),
                Ta = 1.11m,
                Time = DateTime.Now
            };
            var outPuts = inPut.Adapt<TestMappingUpper>();
            outPuts.ID.ShouldBe(default);
            outPuts.TA.ShouldBe(default);
            outPuts.TIME.ShouldBe(default);
            return Task.CompletedTask;
        }
        [Fact]
        public Task MappingTestCase4()
        {
            TypeAdapterConfig<TestMapster, TestMapping>.NewConfig();
            var inPut = new TestMapster()
            {
                Id = Guid.NewGuid().ToString(),
                Ta = 1.11m,
                Time = DateTime.Now
            };
            var output = inPut.Adapt<TestMapping>();
            output.Ta = 2.22m; 
            inPut.Ta = 3.00m; 
            output.Ta.ShouldBe(2.22m);
            inPut.Ta.ShouldBe(3.00m);
            return Task.CompletedTask;
        }
    }

    public class TestMapster
    {
        public string Id { get; set; }
        public decimal Ta { get; set; }
        public DateTime Time { get; set; }
    }
    public class TestMapping
    {
        public string Id { get; set; }
        public decimal Ta { get; set; }
        public DateTime Time { get; set; }
        public string TestId { get; set; }
        public decimal TestTa { get; set; }
        public DateTime TestTime { get; set; }
    }

    public class TestMapsterUpper
    {
        public string Id { get; set; }
        public decimal Ta { get; set; }
        public DateTime Time { get; set; }
    }
    public class TestMappingUpper
    {
        public string ID { get; set; }
        public decimal TA { get; set; }
        public DateTime TIME { get; set; }
    }
}
