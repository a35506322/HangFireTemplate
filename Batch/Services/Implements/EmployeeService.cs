using HangFireTemplate.Batch.Repositories.Entities;
using HangFireTemplate.Batch.Services.Requests;

namespace HangFireTemplate.Batch.Services.Implements;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepositroy;
    private readonly IEmployeeWorkTimeRecordRepository _employeeWorkTimeRecordRepository;
    public EmployeeService(IEmployeeRepository employeeRepositroy
        , IMapper mapper
        , IEmployeeWorkTimeRecordRepository employeeWorkTimeRecordRepository)
    { 
        this._employeeRepositroy = employeeRepositroy;
        this._employeeWorkTimeRecordRepository = employeeWorkTimeRecordRepository;
        this._mapper = mapper;
    }
    public async Task<ResultResponse> GetEmployees()
    {
       var employeeEntities = await _employeeRepositroy.GetEmployees();
       var employeeResponse = this._mapper.Map<IEnumerable<GetEmployeeResponse>>(employeeEntities);
       return new ResultResponse() { Data = employeeResponse };
    }

    public async Task<ResultResponse> InsertOnWorkTime(InsertOnWorkTimeRequest insertOnWorkTimeRequest)
    {
        var employeeWorkTimeRecordsEntity = this._mapper.Map<EmployeeWorkTimeRecordsEntity>(insertOnWorkTimeRequest);

        DateTime now = DateTime.Now;
        employeeWorkTimeRecordsEntity.WorkDay = now.ToString("yyyy/MM/dd");
        employeeWorkTimeRecordsEntity.OnWorkTime = now;

        bool isExist = await this.IsExist(employeeWorkTimeRecordsEntity.EmployeeID);
        if (!isExist)
            return new ResultResponse() { Code = ReturnCodeEnum.VerificationFailed, Message = "查無此員工" };

        bool isRepeatPunch = await this.IsRepeatPunch(employeeWorkTimeRecordsEntity.EmployeeID,employeeWorkTimeRecordsEntity.WorkDay);
        if(isRepeatPunch)
            return new ResultResponse() { Code = ReturnCodeEnum.VerificationFailed, Message = "已重複打卡，請確認" };

        var isSuccess = await this._employeeWorkTimeRecordRepository.InsertEmployeeWorkTimeRecord(employeeWorkTimeRecordsEntity);
        return new ResultResponse() { Message = "打卡成功" };
    }

    private async Task<bool> IsExist(int EmployeeID)
    {
        var employeeEntities = await this._employeeRepositroy.GetEmployees(new EmployeeEntity() { EmployeeID = EmployeeID });

        if (employeeEntities.Count() == 1)
            return true;
        else
            return false;
    }

    private async Task<bool> IsRepeatPunch(int EmployeeID,string WorkDay)
    {
        var  records  = await this._employeeWorkTimeRecordRepository.GetEmployeeWorkTimeRecords(new EmployeeWorkTimeRecordsEntity() { EmployeeID = EmployeeID, WorkDay = WorkDay });

        if (records.Count() >= 1 )
            return true;
        else
            return false;
    }
}
