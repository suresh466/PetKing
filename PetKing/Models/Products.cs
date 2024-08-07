using System.ComponentModel.DataAnnotations;

namespace PetKing.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public string ImageURL { get; set; }
    }
}