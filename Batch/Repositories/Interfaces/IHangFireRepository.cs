namespace HangFireTemplate.Batch.Repositories.Interfaces;

public interface IHangFireRepository
{
    public Task<IEnumerable<HangFireJobAndStateEntity>> GetHangFireJobAndStates(GetHangFireJobAndStateRequest? getHangFireJobAndStateRequest = null);
}
