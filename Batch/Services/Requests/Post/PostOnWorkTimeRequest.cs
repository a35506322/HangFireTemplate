using Microsoft.AspNetCore.Mvc.Formatters;

namespace HangFireTemplate.Batch.Services.Requests.Post;

public class PostOnWorkTimeRequest/*:IValidatableObject*/
{
    [DisplayName("員工編號")]
    [Required(ErrorMessage = "{0} 為必輸")]
    public int EmployeeID { get; set; }
    //[DisplayName("工作日")]
    //[Required(ErrorMessage = "{0} 為必輸")]
    //[RegularExpression(@"^\d{4}/\d{2}/\d{2}$",ErrorMessage = "{0} 格式應為1991/01/01")]
    //public string WorkDay { get; set; }

    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //    if (DateTime.TryParse(this.WorkDay,out var workDay)== false)
    //    {
    //        yield return new ValidationResult($"{this.WorkDay} 轉換日期失敗",new string[] { "WorkDay" });
    //    }
    //}
}
