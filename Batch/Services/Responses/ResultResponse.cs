namespace HangFireTemplate.Batch.Responses;

public class ResultResponse
{
    /// <summary>
    /// 回應狀態碼
    /// </summary>
    public ReturnCodeEnum Code{ get; set; } = ReturnCodeEnum.Ok;
    /// <summary>
    /// 回應狀態訊息
    /// </summary>
    public string Message { get; set; } = string.Empty;
    /// <summary>
    /// 資料
    /// </summary>
    public object Data { get; set; } = null;

    public string TraceId { get; set; } = Guid.NewGuid().ToString();
}
