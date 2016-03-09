using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobApp.Models
{
    public class Job
    {

        [Key]
        public int JobId { get; set; }
        
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ApplicationUserId { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "PositionRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "Long50")]
        public string JobPosition { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "CompanyNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "Long50")]
        public string JobCompanyName { get; set; }

        [Display(Name = "CompanyDescription", ResourceType = typeof(Resources.Resources))]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "Long500")]
        public string JobCompany { get; set; }

        [Display(Name = "JobDescription", ResourceType = typeof(Resources.Resources))]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "Long500")]
        public string JobDescription { get; set; }


        [Display(Name = "SkillsRequirements", ResourceType = typeof(Resources.Resources))]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "Long500")]
        public string JobRequirements { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
        ErrorMessageResourceName = "CityRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
                      ErrorMessageResourceName = "Long50")]
        public string JobCity { get; set; }

        [Display(Name = "Salary", ResourceType = typeof(Resources.Resources))]
        public decimal JobSalary { get; set; }

        [Display(Name = "Category", ResourceType = typeof(Resources.Resources))]

        public JobCategory Category { get; set; }

        public virtual ICollection<Submit> Submit { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
    public enum JobCategory
    {
        [Display(Name = "Sales", ResourceType = typeof(Resources.Resources))]
        Sales = 1,
        [Display(Name = "InformationTechnology", ResourceType = typeof(Resources.Resources))]
        IT = 2,
        [Display(Name = "Banking", ResourceType = typeof(Resources.Resources))]
        Banking = 3,
        [Display(Name = "Construction", ResourceType = typeof(Resources.Resources))]
        Construction = 4,
        [Display(Name = "Retail", ResourceType = typeof(Resources.Resources))]
        Retail = 5,
        [Display(Name = "Marketing", ResourceType = typeof(Resources.Resources))]
        Marketing = 6
    }

}