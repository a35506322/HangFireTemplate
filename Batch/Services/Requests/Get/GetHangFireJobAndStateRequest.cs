namespace HangFireTemplate.Batch.Services.Requests.Get;

public class GetHangFireJobAndStateRequest
{
    /// <summary>
    /// PK 
    /// </summary>
    public int? Id { get; set; }
    /// <summary>
    /// 執行狀態 Succeeded/Failed
    /// </summary>
    public string? StateName { get; set; }
    /// <summary>
    /// 開始時間 (Start)
    /// </summary>
    public DateTime? StartCreatedAt { get; set; }
    /// <summary>
    /// 開始時間 (End)
    /// </summary>
    public DateTime? EndCreatedAt { get; set; }
    /// <summary>
    /// 排程過程 Failed/Enqueued/Processing/Succeeded
    /// <summary>
    public string? Name { get; set; }
}
