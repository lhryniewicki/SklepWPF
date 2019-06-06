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

        public string Name { get; set; }
        public string Surname { get; set; }
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }

        public Order() { }
        public Order(OrderStatus OrderStatus, User User, string Name,
                     string Surname, string StreetName, string PostalCode, string City, string PhoneNumber, PaymentMethod PaymentMethod, DeliveryMethod DeliveryMethod)
        {
            this.OrderedProducts = OrderedProducts;
            this.Created = DateTime.Now;
            this.OrderStatus = OrderStatus;
            this.User = User;
            this.Name = Name;
            this.Surname = Surname;
            this.StreetName = StreetName;
            this.PostalCode = PostalCode;
            this.City = City;
            this.PhoneNumber = PhoneNumber;
            this.PaymentMethod = PaymentMethod;
            this.DeliveryMethod = DeliveryMethod;
        }
    }
}
