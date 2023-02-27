namespace HangFireTemplate.Batch.Repositories.Implements;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DataBaseHelper _dataBaseHelper;
    public EmployeeRepository(DataBaseHelper dataBaseHelper)
    { 
        this._dataBaseHelper = dataBaseHelper;
    }

    public async Task<IEnumerable<EmployeeEntity>> GetEmployees(EmployeeEntity? employeeEntity = null)
    {
        var sql = @"SELECT * FROM [Northwind].[dbo].[Employees] Where 1=1 ";

        using (IDbConnection conn = _dataBaseHelper.CreateLocalHostConnection())
        {
            if (employeeEntity != null)
            {
                if (employeeEntity.EmployeeID > 0)
                {
                    sql += @"And EmployeeID = @EmployeeID ";
                }
            }

            var result = await conn.QueryAsync<EmployeeEntity>(sql,employeeEntity);
            return result;
        }
    }
}
