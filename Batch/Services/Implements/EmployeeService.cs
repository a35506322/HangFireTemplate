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
    /// <summary>
    /// 通知未打卡人員排程
    ///  [DisplayName("排程作業: {0} - {1}")] 應用在HangFire 頁面顯示
    ///  [AutomaticRetry(Attempts = 0)] 失敗不排進駐列
    /// </summary>
    /// <param name="creattor"></param>
    /// <param name="jobName"></param>
    /// <param name="getNotifyUncheckedEmployeesRequest"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    [DisplayName("排程作業: {0} - {1}")]
    [AutomaticRetry(Attempts = 0)]
    public async Task BatchNotifyUncheckedEmployees(string creattor , string jobName,GetNotifyUncheckedEmployeesRequest getNotifyUncheckedEmployeesRequest, PerformContext context)
    {
        EmployeeWorkTimeRecordsEntity employeeWorkTimeRecordsEntity = new EmployeeWorkTimeRecordsEntity()
        {
            WorkDay = getNotifyUncheckedEmployeesRequest.WorkDay
        };

        // 全體員工
        var employeesEntities = await _employeeRepositroy.GetEmployees();

        // 某天有打卡人員
        var records = await this._employeeWorkTimeRecordRepository.GetEmployeeWorkTimeRecords(employeeWorkTimeRecordsEntity);

        var allEmployees = employeesEntities.Select(x => x.EmployeeID);

        var checkedEemployees = records.Select(x => x.EmployeeID);

        var exceptEemployees = allEmployees.Except(checkedEemployees);

        // 排程 Console
        if (exceptEemployees.Count() > 0)
        {
            // context.WriteLine 會在HangFire Processing顯示
            context.WriteLine($"{getNotifyUncheckedEmployeesRequest.WorkDay} 未打卡人員有：");
            foreach (var employee in exceptEemployees)
            {
                context.WriteLine(employee);
            }
        }
        else 
        {
            context.WriteLine($"{getNotifyUncheckedEmployeesRequest.WorkDay} 皆以打卡");
        }
        
    }

    public async Task<ResultResponse> GetEmployees()
    {
       var employeeEntities = await _employeeRepositroy.GetEmployees();
       var employeeResponse = this._mapper.Map<IEnumerable<GetEmployeeResponse>>(employeeEntities);
       return new ResultResponse() { Data = employeeResponse };
    }

    public async Task<ResultResponse> GetWorkTimeRecord(GetWorkTimeRecordRequest getWorkTimeRecordRequest)
    {
        var employeeWorkTimeRecordsEntity = this._mapper.Map<EmployeeWorkTimeRecordsEntity>(getWorkTimeRecordRequest);
        var records = await this._employeeWorkTimeRecordRepository.GetEmployeeWorkTimeRecords(employeeWorkTimeRecordsEntity);
        return new ResultResponse() { Data = records };
    }

    public async Task<ResultResponse> InsertOnWorkTime(PostOnWorkTimeRequest insertOnWorkTimeRequest)
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
