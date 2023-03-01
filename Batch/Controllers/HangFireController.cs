using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangFireTemplate.Batch.Controllers;

[Route("api/[controller]")]
[ApiController]
[OpenApiTags("HangFire排程資訊")]
public class HangFireController : ControllerBase
{
    private readonly IHangFireService _hangFireService;
    public HangFireController(IHangFireService hangFireService)
    {
        this._hangFireService = hangFireService;
    }

    /// <summary>
    /// 取得HangFire排程相關資料
    /// </summary>
    /// <response code="200"></response>
    /// <returns></returns>
    [HttpGet]
    [Route("GetHangFireJobAndStates")]
    [ProducesResponseType(typeof(ResultResponse),200)]
    public async Task<IActionResult> GetHangFireJobAndStates([FromQuery] GetHangFireJobAndStateRequest getHangFireJobAndStateRequest)
    {
       var result = await this._hangFireService.GetHangFireJobAndStates(getHangFireJobAndStateRequest);
        return Ok(result);
    }
}
