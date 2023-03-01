namespace HangFireTemplate.Batch.Repositories.Entities;

public class HangFireErrorEntity
{
    /// <summary>
    /// 錯誤時間
    /// </summary>
    public string FailedAt { get; set; }
    /// <summary>
    /// 錯誤訊息類別
    /// </summary>
    public string ExceptionType { get; set; }
    /// <summary>
    /// 錯誤訊息
    /// </summary>
    public string ExceptionMessage { get; set; }
    /// <summary>
    /// 錯誤詳細資料
    /// </summary>
    public string ExceptionDetails { get; set; }
}

