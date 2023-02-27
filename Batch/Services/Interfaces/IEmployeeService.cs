namespace HangFireTemplate.Batch.Services.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// 取得員工們
        /// </summary>
        /// <returns></returns>
        public Task<ResultResponse> GetEmployees();
    }
}
