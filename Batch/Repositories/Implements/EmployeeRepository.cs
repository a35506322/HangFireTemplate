namespace HangFireTemplate.Batch.Repositories.Implements;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DataBaseHelper _dataBaseHelper;
    public EmployeeRepository(DataBaseHelper dataBaseHelper)
    { 
        this._dataBaseHelper = dataBaseHelper;
    }

    public async Task<IEnumerable<EmployeeEntity>> GetEmployees()
    {
        var sql = @"SELECT * FROM [Northwind].[dbo].[Employees]";

        using (IDbConnection conn = _dataBaseHelper.CreateLocalHostConnection())
        {
            var result = await conn.QueryAsync<EmployeeEntity>(sql);
            return result;
        }
    }
}
