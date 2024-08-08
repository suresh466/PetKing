using System.ComponentModel.DataAnnotations;
using PetKing.Models;

namespace PetKing.ViewModels
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string ShippingName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string ShippingCity { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Invalid Postal Code it should be like: W1T4P3")]
        public string ShippingPostalCode { get; set; }
    }
}