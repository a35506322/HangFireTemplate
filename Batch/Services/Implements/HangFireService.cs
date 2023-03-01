namespace HangFireTemplate.Batch.Services.Implements;

public class HangFireService : IHangFireService
{
    private readonly IMapper _mapper;
    private readonly IHangFireRepository _hangFireRepository;
    public HangFireService(IHangFireRepository hangFireRepository
        , IMapper mapper)
    {
        this._hangFireRepository = hangFireRepository;
        this._mapper = mapper;
    }

    [DisplayName("排程作業: {0} - {1}")]
    [AutomaticRetry(Attempts = 0)]
    public async Task BatchSentFailHangFireDetail(string creattor, string jobName, GetHangFireJobAndStateRequest getHangFireJobAndStateRequest, PerformContext context)
    {
        var hangFireJobAndStateEntities = await this._hangFireRepository.GetHangFireJobAndStates(getHangFireJobAndStateRequest);
        var hangFireDetails = this._mapper.Map<IEnumerable<HangFireJobAndStateResponse>>(hangFireJobAndStateEntities);

        foreach (var detail in hangFireDetails)
        {
            string[] arguments = JsonConvert.DeserializeObject<string[]>(detail.Arguments);
            string template = $"{arguments[0]} / {arguments[1]}  建立於 {detail.CreatedAt} ";
            context.WriteLine(template);
            HangFireErrorEntity error = JsonConvert.DeserializeObject<HangFireErrorEntity>(detail.Data);
            context.WriteLine(error.ExceptionDetails);
            context.WriteLine();
        }
    }

    public async Task<ResultResponse> GetHangFireJobAndStates(GetHangFireJobAndStateRequest getHangFireJobAndStateRequest)
    {
        var hangFireJobAndStateEntities  = await this._hangFireRepository.GetHangFireJobAndStates(getHangFireJobAndStateRequest);
        var response = this._mapper.Map<IEnumerable<HangFireJobAndStateResponse>>(hangFireJobAndStateEntities);
        return new ResultResponse() { Data = response };
    }
}
