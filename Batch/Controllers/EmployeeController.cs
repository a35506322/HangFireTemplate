namespace HangFireTemplate.Batch.Controllers;

[Route("api/[controller]")]
[ApiController]
[OpenApiTags("員工相關")]
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
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ResultResponse), 200)]
    public async Task<IActionResult> GetEmployees()
    {
        var result = await this._employeeService.GetEmployees();
        return Ok(result);
    }
}
