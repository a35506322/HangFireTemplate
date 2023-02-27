using HangFireTemplate.Batch.Services.Requests.Get;
using HangFireTemplate.Batch.Services.Requests.Post;

namespace HangFireTemplate.Batch.Services.Interfaces
{
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

    }
}
