using System.ComponentModel.DataAnnotations;

namespace PetKing.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between $0.01 and $10,000")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public Category Category { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [Display(Name = "Image")]
        public string ImageURL { get; set; }
    }
}