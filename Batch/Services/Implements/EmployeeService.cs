namespace HangFireTemplate.Batch.Services.Implements;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepositroy;
    public EmployeeService(IEmployeeRepository employeeRepositroy
        , IMapper mapper)
    { 
        this._employeeRepositroy = employeeRepositroy;
        this._mapper = mapper;
    }
    public async Task<ResultResponse> GetEmployees()
    {
       var employeeEntities = await _employeeRepositroy.GetEmployees();
        return new ResultResponse() { Data = employeeEntities };
    }
}
