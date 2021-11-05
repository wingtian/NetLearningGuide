using Mediator.Net;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;

namespace NetLearningGuide.UnitTest.Demo
{
    public class AutoMapperTest : DemoBase
    {
        [Fact]
        public async Task AutoMapperHandlerTest()
        {
            var command = new DemoMappingCommand() { UserName = "Cay", Age = 18, Birthday = Convert.ToDateTime("2021-06-01"), Relations = new List<string>() { "ABC", "BCD" } };
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoMappingCommand, CommonResponse<DemoMappingDto>>(command);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                result.Data.UserName.ShouldBe(command.UserName);
                result.Data.UserAge.ShouldBe(command.Age);
                result.Data.UserBirthday.ShouldBe(command.Birthday);
                result.Data.Relation.ShouldBe(command.Relations);
                result.Data.Comment.ShouldBeNull();
            });
        }
        [Fact]
        public async Task AutoMapperServiceTest()
        {
            var command = new DemoMappingServiceCommand() { UserName = "Cay", Age = 18, Birthday = Convert.ToDateTime("2021-06-01"), Relations = new List<string>() { "ABC", "BCD" } };
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoMappingServiceCommand, CommonResponse<DemoMappingDto>>(command);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                result.Data.UserName.ShouldBe(command.UserName);
                result.Data.UserAge.ShouldBe(command.Age);
                result.Data.UserBirthday.ShouldBe(command.Birthday);
                result.Data.Relation.ShouldBe(command.Relations);
                result.Data.Comment.ShouldBeNull();
            });
        }

        [Fact]
        public Task AutoMapperFunctionalTestCase1()
        {
            var command = new DemoMappingServiceCommand() { UserName = "Cay", Age = 18, Birthday = Convert.ToDateTime("2021-06-01"), Relations = new List<string>() { "ABC", "BCD" } };
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DemoMappingServiceCommand, DemoMappingDto>()
                .ForMember(dest => dest.UserAge, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.UserBirthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Relation, opt => opt.MapFrom(src => src.Relations))).CreateMapper();
            var result = mapper.Map<DemoMappingDto>(command);
            result.UserName.ShouldBe(command.UserName);
            result.UserAge.ShouldBe(command.Age);
            result.UserBirthday.ShouldBe(command.Birthday);
            result.Relation.ShouldBe(command.Relations);
            result.Comment.ShouldBeNull();
            return Task.CompletedTask;
        }
        [Fact]
        public Task AutoMapperFunctionalTestCase2()
        {
            var inPut = new TestMapster()
            {
                Id = Guid.NewGuid().ToString(),
                Ta = 1.11m,
                Time = DateTime.Now
            };
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TestMapster, TestMapster>()).CreateMapper();
            var outPut = mapper.Map<TestMapster>(inPut);
            inPut.Id = Guid.NewGuid().ToString();
            inPut.Ta = 2.22m;
            inPut.Time = DateTime.Now.AddDays(1);
            outPut.Id.ShouldNotBe(inPut.Id);
            outPut.Ta.ShouldNotBe(inPut.Ta);
            outPut.Time.ShouldNotBe(inPut.Time);
            return Task.CompletedTask;
        }
        [Fact]
        public Task AutoMapperFunctionalTestCase3()
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
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TestMapster, TestMapster>()).CreateMapper();
            var outPut = mapper.Map<List<TestMapster>>(inPut);
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
        public Task AutoMapperFunctionalTestCase4()
        {
            var inPut1 = new List<TestAutoMappter>() {
                new TestAutoMappter("A")
                {
                    Ta = 111m,
                    Key  = "A"
                },
                new TestAutoMappter( "B")
                {
                    Ta = 111m,
                    Key  = "B"
                },
            };
            var inPut2 = new List<TestAutoMappter>() {
                new TestAutoMappter( "A1")
                {
                    Ta = 222m,
                    Key  = "AA"
                },
                new TestAutoMappter("B1")
                {
                    Ta = 222m,
                    Key  = "BB"
                },
                new TestAutoMappter("C1")
                {
                    Ta = 222m,
                    Key  = "CC"
                },
            };
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TestMapster, TestMapster>()
                    //.ReverseMap()
                    .ForMember(x => x.Key, opt => opt.Ignore())
                    ).CreateMapper();
            var outPut1 = mapper.Map(inPut1, inPut2);
            inPut2.Count.ShouldBe(2);
            outPut1.Count.ShouldBe(2);
            return Task.CompletedTask;
        }
        public class TestAutoMappter
        {
            public TestAutoMappter(string id)
            {
                Id = id;
            }
            public string Id { get; protected set; }
            public decimal Ta { get; set; }
            public DateTime Time { get; set; }
            public string Key { get; set; }
        }
    }
}
