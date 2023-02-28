namespace HangFireTemplate.Batch.Services.Requests.Get;

public class GetNotifyUncheckedEmployeesRequest:IValidatableObject
{
    /// <summary>
    /// 請輸入正確工作日 (格式應為1991/01/01)
    /// </summary>
    [DisplayName("工作日")]
    [Required(ErrorMessage = "{0} 為必輸")]
    [RegularExpression(@"^\d{4}/\d{2}/\d{2}$", ErrorMessage = "{0} 格式應為1991/01/01")]
    public string WorkDay { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateTime.TryParse(this.WorkDay, out var workDay) == false)
        {
            yield return new ValidationResult($"{this.WorkDay} 轉換日期失敗", new string[] { "WorkDay" });
        }
    }
}
