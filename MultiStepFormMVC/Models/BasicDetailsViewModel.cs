using System.ComponentModel.DataAnnotations;

namespace MultiStepFormMVC.Models
{
    public class BasicDetailsViewModel
    {
        [Required]
        public string CustomerID { get; set; }
        [Required]
        [StringLength(30)]
        public string CompanyName { get; set; }
    }
}
