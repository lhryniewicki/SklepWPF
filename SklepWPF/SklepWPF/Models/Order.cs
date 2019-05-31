using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<OrderProduct> OrderedProducts { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
