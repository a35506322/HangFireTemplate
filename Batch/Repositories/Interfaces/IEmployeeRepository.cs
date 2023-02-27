namespace HangFireTemplate.Batch.Repositories.Interfaces;

public interface IEmployeeRepository
{
    /// <summary>
    /// 取得員工們
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<EmployeeEntity>> GetEmployees();
}
