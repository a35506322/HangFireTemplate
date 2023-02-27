namespace HangFireTemplate.Batch.Common.AutoMapperProfiles;

public class EmployeeProfile:Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeEntity, GetEmployeeResponse>();
    }
}
