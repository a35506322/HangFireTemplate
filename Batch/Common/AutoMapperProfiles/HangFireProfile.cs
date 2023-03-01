namespace HangFireTemplate.Batch.Common.AutoMapperProfiles;

public class HangFireProfile : Profile
{
    public HangFireProfile()
    {
        CreateMap<GetHangFireJobAndStateRequest, HangFireJobAndStateEntity>();
        CreateMap<HangFireJobAndStateEntity, HangFireJobAndStateResponse>();
    }
}
