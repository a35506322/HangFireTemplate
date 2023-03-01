namespace HangFireTemplate.Batch.Services.Responses;

public class HangFireJobAndStateResponse
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
    /// 給予參數DisplayName  ["\"Batch\"","\"通知未打卡人員打卡\"",null]
    /// </summary>
    public string? Arguments { get; set; }
    /// <summary>
    /// 開始時間
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 結束時間 (失敗不會有結束時間)
    /// </summary>
    public DateTime? ExpireAt { get; set; }
    /// <summary>
    /// 排程過程 Enqueued/ Processing/ Failed
    /// 
    public string? Name { get; set; }
    /// <summary>
    /// An exception occurred during performance of the job.
    /// </summary>
    public string? Reason { get; set; }
    /// <summary>
    /// 資料有錯誤訊息會在這邊
    /// </summary>
    public string? Data { get; set; }
}
