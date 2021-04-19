using System.ComponentModel.DataAnnotations;

namespace MultiStepFormMVC.Models
{
    public class ContactDetailsViewModel
    {
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Fax { get; set; }
    }
}
