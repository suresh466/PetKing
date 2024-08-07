using System.ComponentModel.DataAnnotations;
using PetKing.Models;

namespace PetKing.ViewModels
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string ShippingName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string ShippingAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string ShippingCity { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string ShippingPostalCode { get; set; }
    }
}