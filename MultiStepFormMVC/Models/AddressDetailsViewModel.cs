using System.ComponentModel.DataAnnotations;

namespace MultiStepFormMVC.Models
{
    public class AddressDetailsViewModel
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PostCode { get; set; }
    }
}
