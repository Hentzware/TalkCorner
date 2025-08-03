using AutoMapper;

namespace TalkCorner.Application.Features.Thread.CreateThread;

public class CreateThreadProfile : Profile
{
    public CreateThreadProfile()
    {
        CreateMap<CreateThreadCommand, Domain.Entities.Thread>();
    }
}