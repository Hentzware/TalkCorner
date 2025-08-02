using AutoMapper;

namespace TalkCorner.Application.Features.Board.GetBoardById;

public class GetBoardByIdProfile : Profile
{
    public GetBoardByIdProfile()
    {
        CreateMap<Domain.Entities.Board, GetBoardByIdDto>()
            .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUser.DisplayName))
            .ForMember(dest => dest.ThreadCount, opt => opt.MapFrom(src => src.Threads.Count))
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Threads.SelectMany(t => t.Posts).Count()));

        CreateMap<Domain.Entities.Board, GetBoardByIdBoardListItemDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.ThreadCount, opt => opt.MapFrom(src => src.Threads.Count));

        CreateMap<Domain.Entities.Thread, GetBoardByIdThreadListItemDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUserId))
            .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUser.DisplayName))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count));

        CreateMap<Domain.Entities.User, GetBoardByIdUserDto>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName));
    }
}