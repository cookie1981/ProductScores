using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProductImmuneSystemScores.Models
{
    public class ProductScoreViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        //[Display(Name = "CTM Product")]
        public string Product { get; set; }

        public IEnumerable<SelectListItem> TeamOptions { get; set; }

        [Required]
        public string SelectedTeam { get; set; }

        public string SDET { get; set; }
        public IEnumerable<SelectListItem> AutomationScoreOptions { get; set; }

        [Required]
        public int SelectedAutomationScore { get; set; }

        public IEnumerable<SelectListItem> ReleaseScoreOptions { get; set; }

        [Required]
        public int SelectedReleaseScore { get; set; }

        public IEnumerable<SelectListItem> TechDebtScoreOptions { get; set; }

        [Required]
        public int SelectedTechDebtScore { get; set; }

        public IEnumerable<SelectListItem> PerformanceScoreOptions { get; set; }

        [Required]
        public int SelectedPerformanceScore { get; set; }

        public IEnumerable<SelectListItem> SecurityScoreOptions { get; set; }

        [Required]
        public int SelectedSecurityScore { get; set; }

        public IEnumerable<SelectListItem> CrossBrowserScoreOptions { get; set; }

        [Required]
        public int SelectedCrossBrowserScore { get; set; }

        public IEnumerable<SelectListItem> MonitoringScoreOptions { get; set; }

        [Required]
        public int SelectedMonitoringScore { get; set; }

        public bool DisplayValidationErrors { get; set; }
        public bool SaveSucceeded { get; set; }
        public bool SaveFailed => !SaveSucceeded;
    }
}