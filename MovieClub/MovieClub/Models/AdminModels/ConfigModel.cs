using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class ConfigModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Organization name should be of less than 50 charactors")]
        public string OrganizationName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Organization name should be of less than 50 charactors")]
        public string ClubName { get; set; }
        [Required]
        [Url(ErrorMessage="API Url is invalid")]
        public string ImdbAPIUrl { get; set; }
        [Required]
        [Range(0,30,ErrorMessage="Up to 30 movies can be displayed in a Page")]
        public int MovieCollectionPageSize { get; set; }
        [Required]
        public int DefaultRentingDuration { get; set; }
        [Required]
        public float DefaultCharge { get; set; }
        [Required]
        public float FinePerDay { get; set; }
        [Required]
        [Range(0, 20, ErrorMessage = "Up to 20 movies can be displayed in Featured Movies")]
        public int MaxFeaturedsCount { get; set; }
        [Required]
        public int SidebarCategoryCount { get; set; }
        [Required]
        public bool ReviewEnabled { get; set; }
        [Required]
        public bool ModerationEnabled { get; set; }
    }
}