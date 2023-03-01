using HangFireTemplate.Batch.Services.Requests.Get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangFireTemplate.Batch.Controllers;

[Route("batch/hangfire/[action]")]
[ApiController]
[OpenApiTags("排程相關")]
public class HangFireBatchController : ControllerBase
{
    private readonly IHangFireService _hangFireService;
    public HangFireBatchController(IHangFireService hangFireService)
    {
        this._hangFireService = hangFireService;
    }

    /// <summary>
    /// 發送排程錯誤訊息
    /// </summary>
    /// <response code="200"></response>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType( 200)]
    public IActionResult BatchSentFailHangFireDetail ([FromQuery] GetHangFireJobAndStateRequest getHangFireJobAndStateRequest)
    {
        BackgroundJob.Enqueue(() => this._hangFireService.BatchSentFailHangFireDetail
        ("管理者", "發送排程錯誤訊息", getHangFireJobAndStateRequest, null));
        return Ok();
    }
}
