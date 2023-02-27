using HangFireTemplate.Batch.Services.Requests.Get;
using HangFireTemplate.Batch.Services.Requests.Post;

namespace HangFireTemplate.Batch.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService employeeService)
    {  
        this._employeeService = employeeService;
    }

    /// <summary>
    /// 取得員工們
    /// </summary>
    /// <response code="200">code:2000 成功</response>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ResultResponse), 200)]
    [OpenApiTags("員工相關")]
    public async Task<IActionResult> GetEmployees()
    {
        var result = await this._employeeService.GetEmployees();
        return Ok(result);
    }

    /// <summary>
    /// 查詢員工打卡時間
    /// </summary>
    /// <param name="getWorkTimeRecordRequest"></param>
    /// <response code="200">code:2000 成功</response>
    /// <returns></returns>
    [HttpPost]
    [Route("GetWorkTimeRecord")]
    [ProducesResponseType(typeof(ResultResponse), 200)]
    [OpenApiTags("打卡相關")]
    public async Task<IActionResult> GetWorkTimeRecord([FromQuery] GetWorkTimeRecordRequest getWorkTimeRecordRequest)
    {
        var result = await this._employeeService.GetWorkTimeRecord(getWorkTimeRecordRequest);
        return Ok(result);
    }

    /// <summary>
    /// 打卡上班
    /// </summary>
    /// <param name="insertOnWorkTimeRequest"></param>
    /// <response code="200">code:2000 成功
    /// code:4000 查無此員工、已重複打卡，請確認</response>
    /// <returns></returns>
    [HttpPost]
    [Route("InsertOnWorkTime")]
    [ProducesResponseType(typeof(ResultResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [OpenApiTags("打卡相關")]
    public async Task<IActionResult> InsertOnWorkTime([FromBody] PostOnWorkTimeRequest insertOnWorkTimeRequest)
    {   
        var result = await this._employeeService.InsertOnWorkTime(insertOnWorkTimeRequest);
        return Ok(result);
    }
}
