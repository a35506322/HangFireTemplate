﻿namespace HangFireTemplate.Batch.Controllers;

[Route("batch/employee/[action]")]
[ApiController]
public class EmployeeBatchController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    public EmployeeBatchController(IEmployeeService employeeService)
    {
        this._employeeService = employeeService;
    }
    /// <summary>
    /// 通知位打卡人員打卡
    /// </summary>
    /// <response code="200">code:2000 成功
    /// code:4000 查無此員工、已重複打卡，請確認</response>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [OpenApiTags("排程相關")]
    public IActionResult NotifyUncheckedEmployees([FromQuery] GetNotifyUncheckedEmployeesRequest getNotifyUncheckedEmployeesRequest)
    {
        /*
         1. 不用理會HangFire背景作業的錯誤，HangFire自己會擷取下來，它不會經歷.net core 生命週期拋錯
         2.  BackgroundJob.Enqueue 是 async await 非同步執行
         */
        BackgroundJob.Enqueue(() => this._employeeService.BatchNotifyUncheckedEmployees($"管理者", "通知未打卡人員打卡", getNotifyUncheckedEmployeesRequest, null));
        return Ok(); ;
    }
}
