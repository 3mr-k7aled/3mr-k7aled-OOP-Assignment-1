using System;

namespace OrderApp
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void Print()
        {
            Console.WriteLine("#" + Id + "  " + Name + "  price=" + Price.ToString("F2") + "  stock=" + Stock);
        }
    }
}
