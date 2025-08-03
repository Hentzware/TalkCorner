using AutoMapper;

namespace TalkCorner.Application.Features.Thread.UpdateThread;

public class UpdateThreadProfile : Profile
{
    public UpdateThreadProfile()
    {
        CreateMap<UpdateThreadCommand, Domain.Entities.Thread>();
    }
}