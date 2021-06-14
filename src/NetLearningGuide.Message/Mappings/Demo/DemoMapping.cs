using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;

namespace NetLearningGuide.Message.Mappings.Demo
{
    public class DemoMapping : NetProfile
    {
        public DemoMapping()
        {
            CreateMap<DemoMappingCommand, DemoMappingDto>()
                .ForMember(dest => dest.UserAge, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.UserBirthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Relation, opt => opt.MapFrom(src => src.Relations));
            CreateMap<DemoMappingServiceCommand, DemoMappingDto>()
                .ForMember(dest => dest.UserAge, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.UserBirthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Relation, opt => opt.MapFrom(src => src.Relations));
        }
    }
}
