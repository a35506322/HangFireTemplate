using HangFireTemplate.Batch.Services.Requests.Get;

namespace HangFireTemplate.Batch.Repositories.Implements;

public class HangFireRepository : IHangFireRepository
{
    private readonly DataBaseHelper _dataBaseHelper;
    public HangFireRepository(DataBaseHelper dataBaseHelper)
    {
        this._dataBaseHelper = dataBaseHelper;
    }
    public async Task<IEnumerable<HangFireJobAndStateEntity>> GetHangFireJobAndStates(GetHangFireJobAndStateRequest? getHangFireJobAndStateRequest)
    {
        var sql = @"SELECT A.[Id]
                          ,A.[StateId]
                          ,A.[StateName]
                          ,A.[InvocationData]
                          ,A.[Arguments]
                          ,A.[CreatedAt]
                          ,A.[ExpireAt]
	                      ,B.[Id] as State_Id
                          ,B.[JobId]
                          ,B.[Name]
                          ,B.[Reason]
                          ,B.[CreatedAt] as State_CreatedAt
                          ,B.[Data]
                      FROM [Northwind].[HangFire].[Job] A
                      JOIN [Northwind].[HangFire].[State] B ON A.Id = B.JobId
                  WHERE 1=1 ";

        if (getHangFireJobAndStateRequest != null)
        {
            if (getHangFireJobAndStateRequest.Id != null)
            {
                sql += @"AND A.Id = @Id ";
            }

            if (getHangFireJobAndStateRequest.StartCreatedAt != null && getHangFireJobAndStateRequest.EndCreatedAt != null)
            {
                sql += @"AND A.CreatedAt >= @StartCreatedAt  AND  A.CreatedAt <= @EndCreatedAt ";
            }

            if (!String.IsNullOrEmpty(getHangFireJobAndStateRequest.StateName))
            {
                sql += @"AND A.StateName = @StateName ";
            }

            if (!String.IsNullOrEmpty(getHangFireJobAndStateRequest.Name))
            {
                sql += @"AND B.Name = @Name ";
            }
        }

        using (IDbConnection conn = _dataBaseHelper.CreateLocalHostConnection())
        {
            var result = await conn.QueryAsync<HangFireJobAndStateEntity>(sql, getHangFireJobAndStateRequest);
            return result;
        }
    }
}
