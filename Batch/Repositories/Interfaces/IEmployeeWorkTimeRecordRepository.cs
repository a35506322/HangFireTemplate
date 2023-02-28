namespace HangFireTemplate.Batch.Repositories.Interfaces
{
    public interface IEmployeeWorkTimeRecordRepository
    {
        /// <summary>
        /// 新增上班時間
        /// </summary>
        /// <param name="employeeWorkTimeRecordsEntity"></param>
        /// <returns></returns>
        public Task<bool> InsertEmployeeWorkTimeRecord(EmployeeWorkTimeRecordsEntity employeeWorkTimeRecordsEntity);
        /// <summary>
        /// 查詢打卡時間
        /// </summary>
        /// <param name="employeeWorkTimeRecordsEntity"></param>
        /// <returns></returns>
        public Task<IEnumerable<EmployeeWorkTimeRecordsEntity>> GetEmployeeWorkTimeRecords(EmployeeWorkTimeRecordsEntity?  employeeWorkTimeRecordsEntity = null);
        /// <summary>
        /// 查詢打卡時間(錯誤)
        /// </summary>
        /// <param name="employeeWorkTimeRecordsEntity"></param>
        /// <returns></returns>
        public Task<IEnumerable<EmployeeWorkTimeRecordsEntity>> GetEmployeeWorkTimeRecordsErr(EmployeeWorkTimeRecordsEntity? employeeWorkTimeRecordsEntity = null);
    }
}
