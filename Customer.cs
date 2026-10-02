using System;
namespace OrderApp
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public bool IsVip { get; set; }

        public Customer(int id, string name, string email, string city, bool isVip)
        {
            Id = id;
            Name = name;
            Email = email;
            City = city;
            IsVip = isVip;
        }

        public void Print()
        {
            Console.WriteLine("#" + Id + "  " + Name + "  <" + Email + ">  " + City + "  vip=" + (IsVip ? "yes" : "no"));
        }
    }
}
