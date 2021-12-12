// Nana Amoah, Anthony Christopher, and Matthew Lochridge
// CS 374 Final Project - Bookstore Embedded SQL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Globalization;

namespace LinqEx1
{
    [Table]
    class Customers
    {
        [Column(Name = "ID")]
        public uint ID;
        [Column(Name = "Name")]
        public string ProductName;
        [Column(Name = "ContactInfo")]
        public string ContactInfo;
        [Column(Name = "YearJoined")]
        public uint YearJoined;
    }

    [Table]
    class Employees
    {
        [Column(Name = "ID")]
        public uint ID;
        [Column(Name = "Name")]
        public string ProductName;
        [Column(Name = "ContactInfo")]
        public string ContactInfo;
        [Column(Name = "YearJoined")]
        public uint YearJoined;
    }

    [Table]
    class Books
    {
        [Column(Name = "ISBN")]
        public ulong ISBN;
        [Column(Name = "Title")]
        public string Title;
        [Column(Name = "Author")]
        public string Author;
        [Column(Name = "Genre")]
        public string Genre;
        [Column(Name = "YearPublished")]
        public uint YearPublished;
        [Column(Name = "Publisher")]
        public string Publisher;
    }

    [Table]
    class Inventory
    {
        [Column(Name = "ID")]
        public uint ID;
        [Column(Name = "ISBN")]
        public ulong ISBN;
        [Column(Name = "CopyNumber")]
        public uint CopyNumber;
        [Column(Name = "Condition")]
        public string Condition;
    }

    [Table]
    class Prices
    {
        [Column(Name = "ID")]
        public uint ID;
        [Column(Name = "ISBN")]
        public ulong ISBN;
        [Column(Name = "Condition")]
        public string Condition;
        [Column(Name = "PurchasePrice")]
        public decimal PurchasePrice;
        [Column(Name = "RentalPrice")]
        public decimal RentalPrice;
    }

    [Table]
    class Sales
    {
        [Column(Name = "ID")]
        public uint ID;
        [Column(Name = "CustomerID")]
        public uint CustomerID;
        [Column(Name = "EmployeeID")]
        public uint EmployeeID;
        [Column(Name = "Date_Time")]
        public DateTime Date_Time;
        [Column(Name = "InventoryID")]
        public uint InventoryID;
        [Column(Name = "Discount")]
        public bool Discount;
    }

    [Table]
    class Rentals
    {
        [Column(Name = "ID")]
        public uint ID;
        [Column(Name = "CustomerID")]
        public uint CustomerID;
        [Column(Name = "EmployeeID")]
        public uint EmployeeID;
        [Column(Name = "Date_Time")]
        public DateTime Date_Time;
        [Column(Name = "InventoryID")]
        public uint InventoryID;
        [Column(Name = "DateDue")]
        public DateTime DateDue;
        [Column(Name = "DateReturned")]
        public DateTime DateReturned;
        [Column(Name = "Discount")]
        public bool Discount;
    }

    [Table]
    class Purchases
    {
        [Column(Name = "ID")]
        public uint ID;
        [Column(Name = "CustomerID")]
        public uint CustomerID;
        [Column(Name = "EmployeeID")]
        public uint EmployeeID;
        [Column(Name = "Date_Time")]
        public DateTime Date_Time;
        [Column(Name = "ISBN")]
        public ulong ISBN;
        [Column(Name = "Condition")]
        public string Condition;
    }

    class BookstoreDB : DataContext
    {
        public Table<Customers> Customers;
        public Table<Employees> Employees;
        public Table<Books> Books;
        public Table<Inventory> Inventory;
        public Table<Prices> Prices;
        public Table<Sales> Sales;
        public Table<Rentals> Rentals;
        public Table<Purchases> Purchases;

        public BookstoreDB() : base(@"Data Source=CS1;Initial Catalog=bookstore;Integrated Security=True")  { }
    }

    class Bookstore
    {
        static void Query01()
        // List the results of giving a 10% increase to the list price of all products.
        {
            BookstoreDB bsdb = new BookstoreDB();

            var prices = from product in bsdb.Products
                         select new { ProductName = product.ProductName, OldUnitPrice = product.UnitPrice, NewUnitPrice = product.UnitPrice * 1.1m };

            NumberFormatInfo setPrecision = new NumberFormatInfo();

            setPrecision.NumberDecimalDigits = 2;

            Console.WriteLine("Query01:");

            foreach (var p in prices)
            {
                Console.Write("Product: " + p.ProductName);
                Console.Write(", Old Unit Price: $" + p.OldUnitPrice.ToString("N", setPrecision));
                Console.Write(", New Unit Price: $" + p.NewUnitPrice.ToString("N", setPrecision));
                Console.Write('\n');
            }
        }

        static void Query02()
        // List the maximum, minimum, and average target level of the products.
        // ***NOTE: I'm working in SQL Server, in which the Product table does not have the Target Level attribute.
        //          Instead, I'll use the Reorder Level.
        {
            BookstoreDB bsdb = new BookstoreDB();

            var maxReorderLevel = bsdb.Products.Max(p => p.ReorderLevel);

            var minReorderLevel = bsdb.Products.Min(p => p.ReorderLevel);

            var avgReorderLevel = bsdb.Products.Average(p => p.ReorderLevel);

            Console.WriteLine("Query02:");

            Console.WriteLine("Max. Reorder Level: " + maxReorderLevel);

            Console.WriteLine("Min. Reorder Level: " + minReorderLevel);

            Console.WriteLine("Avg. Reorder Level: " + avgReorderLevel);
        }

        static void Query03()
        // How many products are currently discontinued?
        {
            BookstoreDB bsdb = new BookstoreDB();

            var discontinuedProducts = from product in bsdb.Products
                                       where product.Discontinued == true
                                       select new { ProductName = product.ProductName };

            var DPCount = discontinuedProducts.Count();

            Console.WriteLine("Query03:");
            Console.WriteLine(DPCount + " products are currently discontinued."); 
        }

        static void Query04()
        // List all Products whose product name contains the word ‘dried’.
        {
            BookstoreDB bsdb = new BookstoreDB();

            var driedProducts = from product in bsdb.Products
                                where product.ProductName.Contains("dried")
                                select new { ProductName = product.ProductName };

            Console.WriteLine("Query04:");

            foreach (var p in driedProducts)
                Console.WriteLine("Product: " + p.ProductName);
        }

        static void Query05()
        // List all products that are beverages and are not discontinued. 
        {
            BookstoreDB bsdb = new BookstoreDB();

            var availableBeverages = from product in bsdb.Products
                                     from category in bsdb.Categories
                                     where product.CategoryID == category.CategoryID && category.CategoryName == "Beverages" && product.Discontinued == false
                                     select new { ProductName = product.ProductName };

            Console.WriteLine("Query05:");

            foreach (var b in availableBeverages)
                Console.WriteLine("Available Beverage: " + b.ProductName);
        }

        static void Query06()
        // List the names of shippers who shipped orders where the shipping fee > 100.
        {
            BookstoreDB bsdb = new BookstoreDB();

            var shipInfo = (from order in bsdb.Orders
                            from shipper in bsdb.Shippers
                            where order.ShipVia == shipper.ShipperID && order.Freight > 100 && order.ShippedDate != null
                            select new { Company = shipper.CompanyName }).Distinct();

            Console.WriteLine("Query06:");

            foreach (var s in shipInfo)
                Console.WriteLine("Company: " + s.Company);
        }

        static void Query07()
        // List the names of all employees and their job title.
        {
            BookstoreDB bsdb = new BookstoreDB();

            var employeePositions = from employee in bsdb.Employees
                                    select new { FirstName = employee.FirstName, LastName = employee.LastName, Title = employee.Title };

            Console.WriteLine("Query07:");

            foreach (var e in employeePositions)
            {
                Console.Write("Name: " + e.FirstName + ' ' + e.LastName);
                Console.Write(", Title: " + e.Title);
                Console.Write('\n');
            }
        }

        static void Query08()
        // List the ship date for all Orders, and if there is an employee assigned to the order, list the employee’s name.
        {
            BookstoreDB bsdb = new BookstoreDB();

            var orderInfo = from order in bsdb.Orders
                            from employee in bsdb.Employees
                            where order.ShippedDate != null && order.EmployeeID == employee.EmployeeID
                            select new { OrderID = order.OrderID, ShippedDate = (DateTime?)order.ShippedDate, FirstName = employee.FirstName, LastName = employee.LastName };

            Console.WriteLine("Query08:");

            foreach (var o in orderInfo)
            {
                Console.Write("Order ID: " + o.OrderID);
                Console.Write(", Ship Date: " + o.ShippedDate);
                if (o.FirstName != null && o.LastName != null)
                    Console.Write(", Employee: " + o.FirstName + ' ' + o.LastName);
                Console.Write('\n');
            }
        }

        static void Query09()
        // List the maximum, minimum, and average prices for all products.
        {
            BookstoreDB bsdb = new BookstoreDB();

            var maxPrice = bsdb.Products.Max(p => p.UnitPrice);

            var minPrice = bsdb.Products.Min(p => p.UnitPrice);

            var avgPrice = bsdb.Products.Average(p => p.UnitPrice);

            NumberFormatInfo setPrecision = new NumberFormatInfo();

            setPrecision.NumberDecimalDigits = 2;

            Console.WriteLine("Query09:");

            Console.WriteLine("Max. Price: $" + maxPrice.ToString("N", setPrecision));

            Console.WriteLine("Min. Price: $" + minPrice.ToString("N", setPrecision));

            Console.WriteLine("Avg. Price: $" + avgPrice.ToString("N", setPrecision));
        }

        static void Query10()
        // List each product category, and for each category, list the maximum, minimum, and average prices for products in that category.
        {
            BookstoreDB bsdb = new BookstoreDB();

            var catPrices = from product in bsdb.Products
                            from category in bsdb.Categories
                            where product.CategoryID == category.CategoryID
                            select new { category = category.CategoryName, unitPrice = product.UnitPrice };

            var catStats = from item in catPrices
                           group item by item.category into cats
                           select new { category = cats.Key, maxPrice = cats.Max(p => p.unitPrice), minPrice = cats.Min(p => p.unitPrice), avgPrice = cats.Average(p => p.unitPrice) };

            NumberFormatInfo setPrecision = new NumberFormatInfo();

            setPrecision.NumberDecimalDigits = 2;

            Console.WriteLine("Query10:");

            foreach (var c in catStats)
            {
                Console.Write("Category: " + c.category);
                Console.Write(", Max. Price: $" + c.maxPrice.ToString("N", setPrecision));
                Console.Write(", Min. Price: $" + c.minPrice.ToString("N", setPrecision));
                Console.Write(", Avg. Price: $" + c.avgPrice.ToString("N", setPrecision));
                Console.Write('\n');
            }
        }

        static void selection()
        {
            Console.WriteLine("Enter a number between 1 and 10 to view the solution:");
            int in1 = Convert.ToInt32(Console.ReadLine());
            switch (in1)
            {
                case 1:
                    Query01();
                    break;
                case 2:
                    Query02();
                    break;
                case 3:
                    Query03();
                    break;
                case 4:
                    Query04();
                    break;
                case 5:
                    Query05();
                    break;
                case 6:
                    Query06();
                    break;
                case 7:
                    Query07();
                    break;
                case 8:
                    Query08();
                    break;
                case 9:
                    Query09();
                    break;
                case 10:
                    Query10();
                    break;
                default:
                    selection();
                    break;
            }
        }

        static void Main(string[] args)
        {
            string cont;
            do {
                selection();
                Console.WriteLine("View another solution? (Y/N)");
                cont = Console.ReadLine();
            } while (cont == "y" || cont == "Y");
        }
    }
}

