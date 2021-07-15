using Mediator.Net;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;
using Shouldly;
using System;
using System.Collections.Generic;
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
        public Task AutoMapperFunctionalTest()
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
    }
}
