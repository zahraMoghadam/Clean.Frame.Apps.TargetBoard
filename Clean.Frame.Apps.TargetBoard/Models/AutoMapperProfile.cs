using AutoMapper;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;

namespace Clean.Frame.Apps.TargetBoard.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UnitDto, Unit>();
            CreateMap<TargetDto, Target>();
            CreateMap<AspectMainBoardDto, Aspect>().ForMember(d=>d.Title,map=>map.MapFrom(x=>x.AspectTitle)).
                ForMember(d => d.Score, map => map.MapFrom(x => x.AspectScore)).
                ForMember(d => d.Weight, map => map.MapFrom(x => x.AspectWeight)).
                ForMember(d => d.Progress, map => map.MapFrom(x => x.AspectProgress));
            CreateMap<MainBoardDto, MainBoard>();
            CreateMap<AspectDto, Aspect>();
            CreateMap<TargetJsonViewModel, TargetDto>();
            CreateMap<AspectJsonViewModel, AspectDto>();
            CreateMap<MainBoardJsonViewModel, MainBoardDto>();
            CreateMap<AspectDto, AspectMainBoardDto>().ForMember(d => d.AspectTitle, map => map.MapFrom(x => x.Title)).
                ForMember(d => d.AspectScore, map => map.MapFrom(x => x.Score)).
                ForMember(d => d.AspectWeight, map => map.MapFrom(x => x.Weight)).
                ForMember(d => d.AspectProgress, map => map.MapFrom(x => x.Progress));

            CreateMap<Unit, UnitDto>();
            CreateMap<MainBoard, MainBoardDto>()
                .ForMember(d => d.UnitName, map => map.MapFrom(s => s.Unit.Name));
            CreateMap<Target, TargetDto>();
            CreateMap<Aspect, AspectDto>();
            CreateMap<TargetDto, TargetJsonViewModel>();
            CreateMap<AspectDto, AspectJsonViewModel>();
            CreateMap<MainBoardDto, MainBoardJsonViewModel>();
        }
    }
}
