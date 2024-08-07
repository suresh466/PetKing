using System.ComponentModel.DataAnnotations;

namespace PetKing.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}