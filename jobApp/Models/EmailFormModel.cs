using System.ComponentModel.DataAnnotations;

namespace jobApp.Models
{
    public class EmailFormModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "FromNameRequired")]
        [Display(Name = "FullName", ResourceType = typeof(Resources.Resources))]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "Long50")]
        public string FromName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "FromEmailRequired")]
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resources))] 
        [EmailAddress]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "Long50")]
        public string FromEmail { get; set; }
        [Display(Name = "Message", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "MessageRequired")]
        [StringLength(500, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "Long500")]
        public string Message { get; set; }
    }
}