using System;
namespace OrderApp
{
    public class Order
    {
        public const int MaxLines = 20;

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string Date { get; set; }
        public bool IsPaid { get; set; }
        public List<OrderLine> Lines { get; set; }

        public Order(int id, Customer customer, string date)
        {
            Id = id;
            Customer = customer;
            Date = date;
            IsPaid = false;
            Lines = new List<OrderLine>();
        }

        public void AddLine(Product product, int quantity)
        {
            if (IsPaid)
            {
                Console.WriteLine("ERROR: cannot change a paid order.");
                return;
            }

            if (Lines.Count >= MaxLines)
            {
                Console.WriteLine("ERROR: order has too many lines.");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("ERROR: quantity must be positive.");
                return;
            }

            if (product.Stock < quantity)
            {
                Console.WriteLine("ERROR: not enough stock for product #" + product.Id + ".");
                return;
            }

            product.Stock -= quantity;
            Lines.Add(new OrderLine(product, quantity));
        }

        public double GetTotal()
        {
            double total = 0;

            foreach (OrderLine line in Lines)
            {
                total += line.GetTotal();
            }

            if (Customer.IsVip)
            {
                total = total * 0.90;
            }

            return total;
        }

        public void MarkPaid()
        {
            if (Lines.Count == 0)
            {
                Console.WriteLine("ERROR: cannot pay an empty order.");
                return;
            }

            IsPaid = true;
        }

        public void Print()
        {
            Console.WriteLine();
            Console.WriteLine("=== ORDER #" + Id + " ===");
            Console.WriteLine("Date: " + Date);
            Console.WriteLine("Customer: " + Customer.Name + " (#" + Customer.Id + ")");
            Console.WriteLine("Paid: " + (IsPaid ? "yes" : "no"));
            Console.WriteLine("Lines:");

            foreach (OrderLine line in Lines)
            {
                Console.WriteLine("  - " + line.Product.Name
                    + "  x" + line.Quantity
                    + "  @" + line.Product.Price.ToString("F2")
                    + "  = " + line.GetTotal().ToString("F2"));
            }

            Console.WriteLine("TOTAL: " + GetTotal().ToString("F2"));
        }
    }
}
