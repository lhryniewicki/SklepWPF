using SklepWPF.Enums;
using System;
using System.Collections.Generic;

namespace SklepWPF.Models
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<OrderProduct> OrderedProducts { get; set; }
        public DateTime Created { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
