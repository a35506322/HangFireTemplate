namespace HangFireTemplate.Batch.Enums;
public enum ReturnCodeEnum
{
    /// <summary>回應成功</summary>
    [Description("執行成功")]
    Ok = 2000,

    /// <summary>驗證失敗</summary>
    [Description("執行成功")]
    VerificationFailed = 4000,
}
