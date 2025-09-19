using EF_Core_13_9.Data;
using EF_Core_13_9.Models;

namespace EF_Core_13_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext db = new();

            // 1- List all customers' first and last names along with their email addresses
            var Customers = db.Customers
                .Select(c => new { c.FirstName, c.LastName, c.Email })
                .ToList();
            foreach (var c in Customers)
                Console.WriteLine($"{c.FirstName} {c.LastName} - {c.Email}");
            Console.WriteLine("--------------------");


            // 2- Retrieve all orders processed by a specific staff member (staff_id = 3)
            var staffOrders = db.Orders
                .Where(o => o.StaffId == 3)
                .ToList();
            foreach (var o in staffOrders)
                Console.WriteLine($"OrderID: {o.OrderId}, CustomerID: {o.CustomerId}");
            Console.WriteLine("--------------------");

            // 3- Get all products that belong to a category named "Mountain Bikes"
            var mountainBikes = db.Products
                .Where(p => p.Category.CategoryName == "Mountain Bikes")
                .ToList();
            foreach (var p in mountainBikes)
                Console.WriteLine($"{p.ProductName}");
            Console.WriteLine("--------------------");

            // 4- Count the total number of orders per store
            var OrderPerStore = db.Orders
                .GroupBy(o => o.StoreId)
                .Select(g => new { StoreId = g.Key, Count = g.Count() })
                .ToList();
            foreach (var s in OrderPerStore)
                Console.WriteLine($"Store {s.StoreId} => {s.Count} Orders");
            Console.WriteLine("--------------------");

            // 5- List all orders that have not been shipped yet (shipped_date is null)
            var OrdersNotShipped = db.Orders
                .Where(o => o.ShippedDate == null)
                .ToList();
            foreach (var o in OrdersNotShipped)
                Console.WriteLine($"OrderID: {o.OrderId}");
            Console.WriteLine("--------------------");

            // 6- Display each customer’s full name and the number of orders they have placed
            var customerOrders = db.Customers
                .Select(c => new
                {
                    FullName = c.FirstName + " " + c.LastName,
                    Count = c.Orders.Count()
                }).ToList();
            foreach (var c in customerOrders)
                Console.WriteLine($"{c.FullName} => {c.Count} Orders");
            Console.WriteLine("--------------------");

            // 7- List all products that have never been ordered
            var neverOrderedProducts = db.Products
                .Where(p => !p.OrderItems.Any())
                .ToList();
            foreach (var p in neverOrderedProducts)
                Console.WriteLine(p.ProductName);
            Console.WriteLine("--------------------");

            // 8- Display products that have a quantity of less than 5 in any store stock
            var lowStockProducts = db.Stocks
                .Where(s => s.Quantity < 5)
                .Select(s => s.Product)
                .Distinct()
                .ToList();
            foreach (var p in lowStockProducts)
                Console.WriteLine(p.ProductName);
            Console.WriteLine("--------------------");

            // 9- Retrieve the first product from the products table
            var FirstProduct = db.Products.FirstOrDefault();
            if (FirstProduct != null) Console.WriteLine(FirstProduct.ProductName);
            Console.WriteLine("--------------------");

            // 10- Retrieve all products from the products table with a certain model year
            var products2019 = db.Products
                .Where(p => p.ModelYear == 2019)
                .ToList();
            foreach (var p in products2019)
                Console.WriteLine(p.ProductName);
            Console.WriteLine("--------------------");

            // 11- Display each product with the number of times it was ordered
            var productOrdersCount = db.Products
                .Select(p => new { p.ProductName, Count = p.OrderItems.Count() })
                .ToList();
            foreach (var p in productOrdersCount)
                Console.WriteLine($"{p.ProductName} => {p.Count} Times");
            Console.WriteLine("--------------------");

            // 12- Count the number of products in a specific category
            var productCount = db.Products.Count(p => p.CategoryId == 2);
            Console.WriteLine(productCount);
            Console.WriteLine("--------------------");

            // 13- Calculate the average list price of products
            var AVGPrice = db.Products.Average(p => p.ListPrice);
            Console.WriteLine(AVGPrice);
            Console.WriteLine("--------------------");

            // 14- Retrieve a specific product from the products table by ID
            var product = db.Products.FirstOrDefault(p => p.ProductId == 5);
            if (product != null) Console.WriteLine(product.ProductName);
            Console.WriteLine("--------------------");

            // 15- List all products that were ordered with a quantity greater than 3 in any order
            var productsOrderedGT3 = db.OrderItems
                .Where(oi => oi.Quantity > 3)
                .Select(oi => oi.Product)
                .Distinct()
                .ToList();
            foreach (var p in productsOrderedGT3)
                Console.WriteLine(p.ProductName);
            Console.WriteLine("--------------------");

            // 16- Display each staff member’s name and how many orders they processed
            var staffOrdersCount = db.Staffs
                .Select(s => new
                {
                    FullName = s.FirstName + " " + s.LastName,
                    Count = s.Orders.Count()
                }).ToList();
            foreach (var s in staffOrdersCount)
                Console.WriteLine($"{s.FullName} => {s.Count} Orders");
            Console.WriteLine("--------------------");

            // 17- List active staff members only (active = true) along with their phone numbers
            var activeStaff = db.Staffs
                .Where(s => s.Active == 1)
                .Select(s => new { s.FirstName, s.LastName, s.Phone })
                .ToList();
            foreach (var s in activeStaff)
                Console.WriteLine($"{s.FirstName} {s.LastName} - {s.Phone}");
            Console.WriteLine("--------------------");


            // 18- List all products with their brand name and category name
            var productsWithDetails = db.Products
                .Select(p => new
                {
                    p.ProductName,
                    Brand = p.Brand.BrandName,
                    Category = p.Category.CategoryName
                })
                .ToList();
            foreach (var p in productsWithDetails)
                Console.WriteLine($"{p.ProductName} - {p.Brand} - {p.Category}");
            Console.WriteLine("--------------------");

            // 19- Retrieve orders that are completed
            var completedOrders = db.Orders
                .Where(o => o.OrderStatus == 3)
                .ToList();
            foreach (var o in completedOrders)
                Console.WriteLine($"OrderID: {o.OrderId}");
            Console.WriteLine("--------------------");


            // 20- List each product with the total quantity sold
            var totalSoldPerProduct = db.Products
                .Select(p => new
                {
                    p.ProductName,
                    TotalSold = p.OrderItems.Sum(oi => (int?)oi.Quantity) ?? 0
                }).ToList();
            foreach (var p in totalSoldPerProduct)
                Console.WriteLine($"{p.ProductName} => {p.TotalSold}");
        }
    }
}
