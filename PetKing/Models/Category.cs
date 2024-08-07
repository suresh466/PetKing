using System.Collections.Generic;

namespace PetKing.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}