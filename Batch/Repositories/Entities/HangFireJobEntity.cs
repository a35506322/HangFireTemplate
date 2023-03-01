namespace HangFireTemplate.Batch.Repositories.Entities;

public class HangFireJobEntity
{
    /// <summary>
    /// PK 
    /// </summary>
    public int Id { get; set; }
    public int StateId { get; set; }
    /// <summary>
    /// 執行狀態 Succeeded/Failed
    /// </summary>
    public string StateName { get; set; }
    public string InvocationData { get; set; }
    /// <summary>
    /// 給予參數DisplayName  ["\"Batch\"","\"通知未打卡人員打卡\"",null]
    /// </summary>
    public string Arguments { get; set; }
    /// <summary>
    /// 開始時間
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// 結束時間 (失敗不會有結束時間)
    /// </summary>
    public DateTime ExpireAt { get; set; }
}

