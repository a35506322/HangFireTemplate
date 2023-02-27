namespace HangFireTemplate.Batch.Repositories.Helpers;

public class DataBaseHelper
{
    private readonly string _localHostConnectionString;

    public DataBaseHelper(IConfiguration configuration)
    {
        _localHostConnectionString = configuration.GetConnectionString("LocalHost");
    }

    public IDbConnection CreateLocalHostConnection() => new SqlConnection(_localHostConnectionString);
}
