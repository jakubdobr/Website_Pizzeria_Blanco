using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public bool VerifiedAddress { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }

    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; } 

        [Required]
        public int Quantity { get; set; } 

        [Required]
        public decimal Price { get; set; } 

        
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public Order Order { get; set; } 
    }
}
