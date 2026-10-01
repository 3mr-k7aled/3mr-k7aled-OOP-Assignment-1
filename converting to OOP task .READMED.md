The C++ program has no classes and no structs Everything is stored in arrays and the arrays
are connected only by the index number and i asked to change it to OOP to connected every 
class and functions works on that data moves inside the class

customer arrays => class Customer
product arrays  => class Product
order arrays  => class Order
line tables  => class OrderLine
Global functions  => class OrderSystem

Left side is what the C++ code had. Right side is what the C# project has ...
global functions donot disappear They move into OrderSystem which is the manager of the program
====================================================================================================
Five customer arrays turn into one small class:
C++
int customerIds[50];
string customerNames[50];
string customerEmails[50];
string customerCities[50];
bool customerIsVip[50];

C#
public class Customer
{
public int Id { get; set; }
public string Name { get; set; }
public string Email { get; set; }
public string City { get; set; }
public bool IsVip { get; set; }
};
===================================================================================================

functions move into the class they belong to :
If a function works on one order, it should live inside Order. So the function calls change like this 

calculateOrderTotal(orderIndex) ==> order.GetTotal()
markOrderPaid(orderId) ==> order.MarkPaid()

image =><img width="906" height="1280" alt="photo_2026-10-01_11-58-18" src="https://github.com/user-attachments/assets/7c8911d0-2559-4a60-b973-9e33db7eb66d" />

























