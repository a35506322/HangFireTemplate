namespace HangFireTemplate.Batch.Repositories.Entities;

public class HangFireStateEntity
{
    /// <summary>
    /// PK 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 與HangFireJobEntity FK
    /// </summary>
    public int JobId { get; set; }
    /// <summary>
    /// 排程過程 Enqueued/ Processing/ Failed
    /// 
    public string Name { get; set; }
    /// <summary>
    /// An exception occurred during performance of the job.
    /// </summary>
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// 資料有錯誤訊息會在這邊
    /// </summary>
    public string Data { get; set; }
}

