using Hangfire.Console;
using HangFireTemplate.Batch.Services.Requests.Get;

namespace HangFireTemplate.Batch;

public class SchTaskWorker
{
    private readonly IServiceProvider _services;
    private readonly IConfiguration _configuration;

    // 取得 IServiceProvider 稍後建立 Scoped 範圍的  DbContext
    // https://blog.darkthread.net/blog/aspnetcore-use-scoped-in-singleton/
    public SchTaskWorker(IServiceProvider services, IConfiguration configuration)
    {
        _services = services;
        _configuration = configuration;
    }
    // 設定定期排程工作
    public void SetSchTasks()
    {
        
        //利用 appSetting設定循環時間 _configuration["BatchCofig:Corn:NotifyUncheckedEmployees"]         
        SetSchTask("排程作業: Batch - 通知未打卡人員打卡", () => NotifyUncheckedEmployees("Batch", "通知未打卡人員打卡",null), _configuration["BatchCofig:Corn:NotifyUncheckedEmployees"]);
    }

    // 先刪再設，避免錯過時間排程在伺服器啟動時執行
    // https://blog.darkthread.net/blog/missed-recurring-job-in-hangfire/
    void SetSchTask(string id, Expression<Action> job, string cron)
    {
        RecurringJob.RemoveIfExists(id);
        RecurringJob.AddOrUpdate(id, job, cron, TimeZoneInfo.Local);
    }

    [DisplayName("排程作業: {0} - {1}")]
    [AutomaticRetry(Attempts = 0)]
    public async Task NotifyUncheckedEmployees(string creattor, string jobName, PerformContext context)
    {
        /* 
         * 1.打法切記要使用async awiat 才行，不然內部執行不會等待
         * 2.如果要用 PerformContext context 必須要包在內部的 Expression<Action> job 才行唷，所以有發現我從最外層傳到最裡層
         * 3.HangFire 老規矩會幫你擷取錯誤並且不會讓伺服器當機
         */
        await Task.Run(async () =>
        {
            // 不創造這個結界範圍，會因為我排程註冊為Single與Scope會有衝突
            using (var scope = _services.CreateScope())
            {
                var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();
                GetNotifyUncheckedEmployeesRequest getNotifyUncheckedEmployeesRequest = new GetNotifyUncheckedEmployeesRequest();
                getNotifyUncheckedEmployeesRequest.WorkDay = DateTime.Now.ToString("yyyy/MM/dd");
                await employeeService.BatchNotifyUncheckedEmployees(creattor, jobName, getNotifyUncheckedEmployeesRequest, context);
            }
        });
    }
}
// 擴充方法，註冊排程工作元件以及設定排程
public static class SchTaskWorkerExtensions
{
    public static WebApplicationBuilder AddSchTaskWorker(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<SchTaskWorker>();
        return builder;
    }

    public static void SetSchTasks(this WebApplication app)
    {
        var worker = app.Services.GetRequiredService<SchTaskWorker>();
        worker.SetSchTasks();
    }
}

