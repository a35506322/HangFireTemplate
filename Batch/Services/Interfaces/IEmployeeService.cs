namespace HangFireTemplate.Batch.Services.Interfaces;

public interface IEmployeeService
{
    /// <summary>
    /// 取得員工們
    /// </summary>
    /// <returns></returns>
    public Task<ResultResponse> GetEmployees();
    /// <summary>
    /// 打卡上班
    /// </summary>
    /// <param name="insertOnWorkTimeRequest"></param>
    /// <returns></returns>
    public Task<ResultResponse> InsertOnWorkTime(PostOnWorkTimeRequest insertOnWorkTimeRequest);
    /// <summary>
    /// 查詢打卡時間
    /// </summary>
    /// <param name="getWorkTimeRecordRequest"></param>
    /// <returns></returns>
    public Task<ResultResponse> GetWorkTimeRecord(GetWorkTimeRecordRequest getWorkTimeRecordRequest);
    /// <summary>
    /// [排程]通知未打卡人員打卡
    /// </summary>
    /// <param name="creattor">{userId} 建立於 {DateTime.Now:MM-dd HH:mm:ss}</param>
    /// <param name="jobName">排程名稱</param>
    /// <param name="getNotifyUncheckedEmployeesRequest"></param>
    /// <param name="context">HangFire 中的 Console</param>
    /// <returns></returns>
    public Task BatchNotifyUncheckedEmployees(string creattor, string jobName,GetNotifyUncheckedEmployeesRequest getNotifyUncheckedEmployeesRequest, PerformContext context);
    /// <summary>
    /// [排程]通知未打卡人員打卡(錯誤)
    /// </summary>
    /// <param name="creattor">{userId} 建立於 {DateTime.Now:MM-dd HH:mm:ss}</param>
    /// <param name="jobName">排程名稱</param>
    /// <param name="getNotifyUncheckedEmployeesRequest"></param>
    /// <param name="context">HangFire 中的 Console</param>
    /// <returns></returns>
    public Task BatchNotifyUncheckedEmployeesErr(string creattor, string jobName, GetNotifyUncheckedEmployeesRequest getNotifyUncheckedEmployeesRequest, PerformContext context);

}
