using System;

namespace OrderApp
{
    public class Program
    {
        static OrderSystem system = new OrderSystem();

        static void SeedSampleData()
        {
            system.AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
            system.AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
            system.AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

            system.AddProduct(101, "USB Cable", 50.0, 100);
            system.AddProduct(102, "Wireless Mouse", 250.0, 40);
            system.AddProduct(103, "Mechanical Keyboard", 1200.0, 15);
            system.AddProduct(104, "Laptop Stand", 400.0, 25);
        }

        static void RunDemoScenario()
        {
            system.CreateOrder(1001, 1, "2026-09-15");
            system.AddLineToOrder(1001, 101, 2);
            system.AddLineToOrder(1001, 102, 1);
            system.MarkOrderPaid(1001);

            system.CreateOrder(1002, 2, "2026-09-15");
            system.AddLineToOrder(1002, 103, 1);
            system.AddLineToOrder(1002, 104, 1);

            system.CreateOrder(1003, 3, "2026-09-16");
            system.AddLineToOrder(1003, 101, 5);
            system.MarkOrderPaid(1003);
        }

        static int ReadInt(string message)
        {
            Console.Write(message);
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Invalid number, try again: ");
            }
            return value;
        }

        static string ReadText(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("---------- MENU ----------");
            Console.WriteLine("1) Print customers");
            Console.WriteLine("2) Print products");
            Console.WriteLine("3) Print all orders");
            Console.WriteLine("4) Print one order by id");
            Console.WriteLine("5) Create order");
            Console.WriteLine("6) Add line to order");
            Console.WriteLine("7) Mark order paid");
            Console.WriteLine("8) Show paid sales total");
            Console.WriteLine("0) Exit");
        }

        static void RunInteractiveMenu()
        {
            int choice = -1;

            while (choice != 0)
            {
                PrintMenu();
                choice = ReadInt("Choice: ");

                if (choice == 1)
                {
                    system.PrintCustomers();
                }
                else if (choice == 2)
                {
                    system.PrintProducts();
                }
                else if (choice == 3)
                {
                    system.PrintAllOrders();
                }
                else if (choice == 4)
                {
                    int orderId = ReadInt("Order id: ");
                    system.PrintOrder(orderId);
                }
                else if (choice == 5)
                {
                    int orderId = ReadInt("Order id: ");
                    int customerId = ReadInt("Customer id: ");
                    string date = ReadText("Date (YYYY-MM-DD): ");
                    system.CreateOrder(orderId, customerId, date);
                }
                else if (choice == 6)
                {
                    int orderId = ReadInt("Order id: ");
                    int productId = ReadInt("Product id: ");
                    int quantity = ReadInt("Quantity: ");
                    system.AddLineToOrder(orderId, productId, quantity);
                }
                else if (choice == 7)
                {
                    int orderId = ReadInt("Order id: ");
                    system.MarkOrderPaid(orderId);
                }
                else if (choice == 8)
                {
                    Console.WriteLine("Paid sales total: " + system.TotalSalesPaidOnly().ToString("F2"));
                }
                else if (choice == 0)
                {
                    Console.WriteLine("Bye.");
                }
                else
                {
                    Console.WriteLine("Unknown choice.");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Object Oriented Order System");
            Console.WriteLine("Seed sample data, show a demo, then open the menu.");

            SeedSampleData();
            RunDemoScenario();

            system.PrintCustomers();
            system.PrintProducts();
            system.PrintAllOrders();

            Console.WriteLine();
            Console.WriteLine("Paid sales total after demo: " + system.TotalSalesPaidOnly().ToString("F2"));

            RunInteractiveMenu();
        }
    }
}
