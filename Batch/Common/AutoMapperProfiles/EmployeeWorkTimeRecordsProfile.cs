namespace HangFireTemplate.Batch.Common.AutoMapperProfiles;

public class EmployeeWorkTimeRecordsProfile : Profile
{
    public EmployeeWorkTimeRecordsProfile()
    {
        CreateMap<InsertOnWorkTimeRequest, EmployeeWorkTimeRecordsEntity>();
    }
}
