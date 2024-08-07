using PetKing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetKing.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string ShippingName { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingPostalCode { get; set; }
    }
}