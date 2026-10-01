using System;
namespace OrderApp
{
    public class OrderSystem
    {
        public const int MaxCustomers = 50;
        public const int MaxProducts = 50;
        public const int MaxOrders = 100;

        private List<Customer> customers = new List<Customer>();
        private List<Product> products = new List<Product>();
        private List<Order> orders = new List<Order>();

        public Customer FindCustomerById(int id)
        {
            foreach (Customer customer in customers)
            {
                if (customer.Id == id)
                    return customer;
            }
            return null;
        }

        public Product FindProductById(int id)
        {
            foreach (Product product in products)
            {
                if (product.Id == id)
                    return product;
            }
            return null;
        }

        public Order FindOrderById(int id)
        {
            foreach (Order order in orders)
            {
                if (order.Id == id)
                    return order;
            }
            return null;
        }

        public void AddCustomer(int id, string name, string email, string city, bool isVip)
        {
            if (customers.Count >= MaxCustomers)
            {
                Console.WriteLine("ERROR: customer list is full.");
                return;
            }

            if (FindCustomerById(id) != null)
            {
                Console.WriteLine("ERROR: customer id " + id + " already exists.");
                return;
            }

            customers.Add(new Customer(id, name, email, city, isVip));
        }

        public void PrintCustomers()
        {
            Console.WriteLine();
            Console.WriteLine("=== CUSTOMERS (" + customers.Count + ") ===");

            foreach (Customer customer in customers)
            {
                customer.Print();
            }
        }

        public void AddProduct(int id, string name, double price, int stock)
        {
            if (products.Count >= MaxProducts)
            {
                Console.WriteLine("ERROR: product list is full.");
                return;
            }

            if (FindProductById(id) != null)
            {
                Console.WriteLine("ERROR: product id " + id + " already exists.");
                return;
            }

            products.Add(new Product(id, name, price, stock));
        }

        public void PrintProducts()
        {
            Console.WriteLine();
            Console.WriteLine("=== PRODUCTS (" + products.Count + ") ===");

            foreach (Product product in products)
            {
                product.Print();
            }
        }

        public void CreateOrder(int orderId, int customerId, string date)
        {
            if (orders.Count >= MaxOrders)
            {
                Console.WriteLine("ERROR: order list is full.");
                return;
            }

            if (FindOrderById(orderId) != null)
            {
                Console.WriteLine("ERROR: order id " + orderId + " already exists.");
                return;
            }

            Customer customer = FindCustomerById(customerId);
            if (customer == null)
            {
                Console.WriteLine("ERROR: customer id " + customerId + " not found.");
                return;
            }

            orders.Add(new Order(orderId, customer, date));
        }

        public void AddLineToOrder(int orderId, int productId, int quantity)
        {
            Order order = FindOrderById(orderId);
            if (order == null)
            {
                Console.WriteLine("ERROR: order id " + orderId + " not found.");
                return;
            }

            Product product = FindProductById(productId);
            if (product == null)
            {
                Console.WriteLine("ERROR: product id " + productId + " not found.");
                return;
            }

            order.AddLine(product, quantity);
        }

        public void MarkOrderPaid(int orderId)
        {
            Order order = FindOrderById(orderId);
            if (order == null)
            {
                Console.WriteLine("ERROR: order id " + orderId + " not found.");
                return;
            }

            order.MarkPaid();
        }

        public void PrintOrder(int orderId)
        {
            Order order = FindOrderById(orderId);
            if (order == null)
            {
                Console.WriteLine("ERROR: order id " + orderId + " not found.");
                return;
            }

            order.Print();
        }

        public void PrintAllOrders()
        {
            Console.WriteLine();
            Console.WriteLine("=== ALL ORDERS (" + orders.Count + ") ===");

            foreach (Order order in orders)
            {
                order.Print();
            }
        }

        public double TotalSalesPaidOnly()
        {
            double sum = 0;

            foreach (Order order in orders)
            {
                if (order.IsPaid)
                    sum += order.GetTotal();
            }

            return sum;
        }
    }
}
