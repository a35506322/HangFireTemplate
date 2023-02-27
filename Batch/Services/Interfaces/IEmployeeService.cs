using HangFireTemplate.Batch.Services.Requests;

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
        public Task<ResultResponse> InsertOnWorkTime(InsertOnWorkTimeRequest insertOnWorkTimeRequest);

    }
}
