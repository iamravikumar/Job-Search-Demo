using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jobApp.Models
{
    public class Submit
    {
        [Key]
        public int SubmitId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "FromNameRequired")]
        [Display(Name = "FullName", ResourceType = typeof(Resources.Resources))]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "Long50")]
        public string fullName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "FromEmailRequired")]
        [Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resources))]
        [EmailAddress]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "Long50")]
        public string submitEmail { get; set; }
        public string CV { get; set; }
        [ForeignKey("Job")]
        public int jobid { get; set; }
        [ForeignKey("JobId")]
        public Job Job { get; set; }
    }
}