using System;
using System.Collections.Generic;
using Marketplace.Models;

namespace yyy.Services
{
    public class OrderWithStatus
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<int> Counts { get; set; } = new();
        public DateTime? StatusDate { get; set; }
        public List<Product> Products { get; set; } = new();
        public List<int> Quantities { get; set; } = new();
        public List<OrderItem> OrderItems { get; set; } = new();
        public List<StatusHistoryOrder> StatusHistory { get; set; } = new();
    }
}