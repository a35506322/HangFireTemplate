using HangFireTemplate.Batch.Repositories.Helpers;

namespace HangFireTemplate.Batch.Repositories.Implements
{
    public class EmployeeWorkTimeRecordRepository : IEmployeeWorkTimeRecordRepository
    {
        private readonly DataBaseHelper _dataBaseHelper;
        public EmployeeWorkTimeRecordRepository(DataBaseHelper dataBaseHelper)
        {
            this._dataBaseHelper = dataBaseHelper;
        }

        public async Task<IEnumerable<EmployeeWorkTimeRecordsEntity>> GetEmployeeWorkTimeRecords(EmployeeWorkTimeRecordsEntity? employeeWorkTimeRecordsEntity = null)
        {
            string sql = @"SELECT [RecordNo]
                                  ,[EmployeeID]
                                  ,[WorkDay]
                                  ,[OnWorkTime]
                                  ,[OffWorkTime]
                            FROM [Northwind].[dbo].[EmployeeWorkTimeRecords] 
                            Where 1=1 ";

            if (employeeWorkTimeRecordsEntity != null)
            {
                if (employeeWorkTimeRecordsEntity.EmployeeID > 0) 
                {
                    sql += @"AND EmployeeID = @EmployeeID ";
                }

                if (!String.IsNullOrEmpty(employeeWorkTimeRecordsEntity.WorkDay))
                {
                    sql += @"AND WorkDay = @WorkDay ";
                }
            }

            using (IDbConnection conn = _dataBaseHelper.CreateLocalHostConnection())
            {
                var employeeWorkTimeRecordsEntities = await conn.QueryAsync<EmployeeWorkTimeRecordsEntity>(sql, employeeWorkTimeRecordsEntity);
                return employeeWorkTimeRecordsEntities;
            }
        }

        public async Task<IEnumerable<EmployeeWorkTimeRecordsEntity>> GetEmployeeWorkTimeRecordsErr(EmployeeWorkTimeRecordsEntity? employeeWorkTimeRecordsEntity = null)
        {
            string sql = @"SELECT [RecordNo]
                                  ,[EmployeeID]
                                  ,[WorkDay]
                                  ,[OnWorkTime]
                                  ,[OffWorkTime]
                            FROM [Northwind].[dbo].[EmployeeWorkTimeRecords] 
                            Wher 1=1 ";

            if (employeeWorkTimeRecordsEntity != null)
            {
                if (employeeWorkTimeRecordsEntity.EmployeeID > 0)
                {
                    sql += @"AND EmployeeID = @EmployeeID ";
                }

                if (!String.IsNullOrEmpty(employeeWorkTimeRecordsEntity.WorkDay))
                {
                    sql += @"AND WorkDay = @WorkDay ";
                }
            }

            using (IDbConnection conn = _dataBaseHelper.CreateLocalHostConnection())
            {
                var employeeWorkTimeRecordsEntities = await conn.QueryAsync<EmployeeWorkTimeRecordsEntity>(sql, employeeWorkTimeRecordsEntity);
                return employeeWorkTimeRecordsEntities;
            }
        }

        public async Task<bool> InsertEmployeeWorkTimeRecord(EmployeeWorkTimeRecordsEntity employeeWorkTimeRecordsEntity)
        {
            var sql = @"INSERT INTO [Northwind].[dbo].[EmployeeWorkTimeRecords]
                           ([EmployeeID]
                           ,[WorkDay]
                           ,[OnWorkTime])
                     VALUES
                           (@EmployeeID
                           ,@WorkDay
                           ,@OnWorkTime)";

            using (IDbConnection conn = _dataBaseHelper.CreateLocalHostConnection())
            {
                var count = await conn.ExecuteAsync(sql, employeeWorkTimeRecordsEntity);
                return true;
            }
        }
    }
}
