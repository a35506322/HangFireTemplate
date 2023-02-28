namespace HangFireTemplate.Batch.Common.AutoMapperProfiles;

public class EmployeeWorkTimeRecordsProfile : Profile
{
    public EmployeeWorkTimeRecordsProfile()
    {
        CreateMap<PostOnWorkTimeRequest, EmployeeWorkTimeRecordsEntity>();
        CreateMap<GetWorkTimeRecordRequest, EmployeeWorkTimeRecordsEntity>();
        CreateMap<EmployeeWorkTimeRecordsEntity, GetWorkTimeRecordResponse>();
        CreateMap<GetNotifyUncheckedEmployeesRequest, GetWorkTimeRecordResponse>();
        
    }
}
