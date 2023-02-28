using Microsoft.AspNetCore.Mvc.Formatters;

namespace HangFireTemplate.Batch.Services.Requests.Post;

public class PostOnWorkTimeRequest
{
    [DisplayName("員工編號")]
    [Required(ErrorMessage = "{0} 為必輸")]
    public int EmployeeID { get; set; }
}
