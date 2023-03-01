namespace HangFireTemplate.Batch.Services.Interfaces;

public interface IHangFireService
{
    /// <summary>
    /// 取得HangFire進程詳細資料
    /// </summary>
    /// <returns></returns>
    public Task<ResultResponse> GetHangFireJobAndStates(GetHangFireJobAndStateRequest getHangFireJobAndStateRequest);
    /// <summary>
    /// 批次發送排程資訊
    /// </summary>
    /// <param name="creattor">創建者 Batch 管理者</param>
    /// <param name="jobName"></param>
    /// <param name="getHangFireJobAndStateRequest"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task BatchSentFailHangFireDetail(string creattor, string jobName, 
        GetHangFireJobAndStateRequest getHangFireJobAndStateRequest, PerformContext context);
}
