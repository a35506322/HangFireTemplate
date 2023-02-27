namespace HangFireTemplate.Batch.Repositories.Entities
{
    public class EmployeeWorkTimeRecordsEntity
    {
        public int RecordNo { get; set; }
        public int EmployeeID { get; set; }
        /// <summary>
        /// 2023/01/01
        /// </summary>
        public string WorkDay { get; set; }
        public DateTime? OnWorkTime { get; set; }
        public DateTime? OffWorkTime { get; set; }
    }
}
