// Matthew Lochridge
// CS 374 EX05 10/18/2021
// Based on examples from Pete Tucker

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
    class Products
    {
        [Column(Name = "ProductID")]
        public int ID;
        [Column(Name = "ProductName")]
        public string ProductName;
        [Column(Name = "SupplierID")]
        public int SupplierID;
        [Column(Name = "CategoryID")]
        public int CategoryID;
        [Column(Name = "QuantityPerUnit")]
        public string QuantityPerUnit;
        [Column(Name = "UnitPrice")]
        public decimal UnitPrice;
        [Column(Name = "UnitsInStock")]
        public int UnitsInStock;
        [Column(Name = "UnitsOnOrder")]
        public int UnitsOnOrder;
        [Column(Name = "ReorderLevel")]
        public short ReorderLevel;
        [Column(Name = "Discontinued")]
        public bool Discontinued;
    }

    [Table]
    class Categories
    {
        [Column(Name = "CategoryID")]
        public int CategoryID;
        [Column(Name = "CategoryName")]
        public string CategoryName;
        [Column(Name = "Description")]
        public string Description;
    }
    [Table]
    class Orders
    {
        [Column(Name = "OrderID")]
        public int OrderID;
        [Column(Name = "EmployeeID")]
        public int EmployeeID;
        [Column(Name = "ShipVia")]
        public int ShipVia;
        [Column(Name = "ShipName")]
        public string ShipName;
        [Column(Name = "ShippedDate")]
        public DateTime ShippedDate;
        [Column(Name = "Freight")]
        public decimal Freight;
    }

    [Table]
    class Shippers
    {
        [Column(Name = "ShipperID")]
        public int ShipperID;
        [Column(Name = "CompanyName")]
        public string CompanyName;
        [Column(Name = "Phone")]
        public string Phone;
    }
    [Table]
    class Employees
    {
        [Column(Name = "EmployeeID")]
        public int EmployeeID;
        [Column(Name = "Company")]
        public string Company;
        [Column(Name = "LastName")]
        public string LastName;
        [Column(Name = "FirstName")]
        public string FirstName;
        [Column(Name = "Title")]
        public string Title;
    }

    class NWindDatabase : DataContext
    {
        public Table<Products> Products;
        public Table<Categories> Categories;
        public Table<Employees> Employees;
        public Table<Orders> Orders;
        public Table<Shippers> Shippers;

        public NWindDatabase() : base(@"Data Source=CS1;Initial Catalog=Northwind;Integrated Security=True")  { }
    }

    class EX05
    {
        static void EX05_01()
        // List the results of giving a 10% increase to the list price of all products.
        {
            NWindDatabase nwnddb = new NWindDatabase();

            var prices = from product in nwnddb.Products
                         select new { ProductName = product.ProductName, OldUnitPrice = product.UnitPrice, NewUnitPrice = product.UnitPrice * 1.1m };

            NumberFormatInfo setPrecision = new NumberFormatInfo();

            setPrecision.NumberDecimalDigits = 2;

            Console.WriteLine("EX05_01:");

            foreach (var p in prices)
            {
                Console.Write("Product: " + p.ProductName);
                Console.Write(", Old Unit Price: $" + p.OldUnitPrice.ToString("N", setPrecision));
                Console.Write(", New Unit Price: $" + p.NewUnitPrice.ToString("N", setPrecision));
                Console.Write('\n');
            }
        }

        static void EX05_02()
        // List the maximum, minimum, and average target level of the products.
        // ***NOTE: I'm working in SQL Server, in which the Product table does not have the Target Level attribute.
        //          Instead, I'll use the Reorder Level.
        {
            NWindDatabase nwnddb = new NWindDatabase();

            var maxReorderLevel = nwnddb.Products.Max(p => p.ReorderLevel);

            var minReorderLevel = nwnddb.Products.Min(p => p.ReorderLevel);

            var avgReorderLevel = nwnddb.Products.Average(p => p.ReorderLevel);

            Console.WriteLine("EX05_02:");

            Console.WriteLine("Max. Reorder Level: " + maxReorderLevel);

            Console.WriteLine("Min. Reorder Level: " + minReorderLevel);

            Console.WriteLine("Avg. Reorder Level: " + avgReorderLevel);
        }

        static void EX05_03()
        // How many products are currently discontinued?
        {
            NWindDatabase nwnddb = new NWindDatabase();

            var discontinuedProducts = from product in nwnddb.Products
                                       where product.Discontinued == true
                                       select new { ProductName = product.ProductName };

            var DPCount = discontinuedProducts.Count();

            Console.WriteLine("EX05_03:");
            Console.WriteLine(DPCount + " products are currently discontinued.");
        }

        static void EX05_04()
        // List all Products whose product name contains the word ‘dried’.
        {
            NWindDatabase nwnddb = new NWindDatabase();

            var driedProducts = from product in nwnddb.Products
                                where product.ProductName.Contains("dried")
                                select new { ProductName = product.ProductName };

            Console.WriteLine("EX05_04:");

            foreach (var p in driedProducts)
                Console.WriteLine("Product: " + p.ProductName);
        }

        static void EX05_05()
        // List all products that are beverages and are not discontinued. 
        {
            NWindDatabase nwnddb = new NWindDatabase();

            var availableBeverages = from product in nwnddb.Products
                                     from category in nwnddb.Categories
                                     where product.CategoryID == category.CategoryID && category.CategoryName == "Beverages" && product.Discontinued == false
                                     select new { ProductName = product.ProductName };

            Console.WriteLine("EX05_05:");

            foreach (var b in availableBeverages)
                Console.WriteLine("Available Beverage: " + b.ProductName);
        }

        static void EX05_06()
        // List the names of shippers who shipped orders where the shipping fee > 100.
        {
            NWindDatabase nwnddb = new NWindDatabase();

            var shipInfo = (from order in nwnddb.Orders
                            from shipper in nwnddb.Shippers
                            where order.ShipVia == shipper.ShipperID && order.Freight > 100 && order.ShippedDate != null
                            select new { Company = shipper.CompanyName }).Distinct();

            Console.WriteLine("EX05_06:");

            foreach (var s in shipInfo)
                Console.WriteLine("Company: " + s.Company);
        }

        static void EX05_07()
        // List the names of all employees and their job title.
        {
            NWindDatabase nwnddb = new NWindDatabase();

            var employeePositions = from employee in nwnddb.Employees
                                    select new { FirstName = employee.FirstName, LastName = employee.LastName, Title = employee.Title };

            Console.WriteLine("EX05_07:");

            foreach (var e in employeePositions)
            {
                Console.Write("Name: " + e.FirstName + ' ' + e.LastName);
                Console.Write(", Title: " + e.Title);
                Console.Write('\n');
            }
        }

        static void EX05_08()
        // List the ship date for all Orders, and if there is an employee assigned to the order, list the employee’s name.
        {
            NWindDatabase nwnddb = new NWindDatabase();

            var orderInfo = from order in nwnddb.Orders
                            from employee in nwnddb.Employees
                            where order.ShippedDate != null && order.EmployeeID == employee.EmployeeID
                            select new { OrderID = order.OrderID, ShippedDate = (DateTime?)order.ShippedDate, FirstName = employee.FirstName, LastName = employee.LastName };

            Console.WriteLine("EX05_08:");

            foreach (var o in orderInfo)
            {
                Console.Write("Order ID: " + o.OrderID);
                Console.Write(", Ship Date: " + o.ShippedDate);
                if (o.FirstName != null && o.LastName != null)
                    Console.Write(", Employee: " + o.FirstName + ' ' + o.LastName);
                Console.Write('\n');
            }
        }

        static void EX05_09()
        // List the maximum, minimum, and average prices for all products.
        {
            NWindDatabase nwnddb = new NWindDatabase();

            var maxPrice = nwnddb.Products.Max(p => p.UnitPrice);

            var minPrice = nwnddb.Products.Min(p => p.UnitPrice);

            var avgPrice = nwnddb.Products.Average(p => p.UnitPrice);

            NumberFormatInfo setPrecision = new NumberFormatInfo();

            setPrecision.NumberDecimalDigits = 2;

            Console.WriteLine("EX05_09:");

            Console.WriteLine("Max. Price: $" + maxPrice.ToString("N", setPrecision));

            Console.WriteLine("Min. Price: $" + minPrice.ToString("N", setPrecision));

            Console.WriteLine("Avg. Price: $" + avgPrice.ToString("N", setPrecision));
        }

        static void EX05_10()
        // List each product category, and for each category, list the maximum, minimum, and average prices for products in that category.
        {
            NWindDatabase nwnddb = new NWindDatabase();

            var catPrices = from product in nwnddb.Products
                            from category in nwnddb.Categories
                            where product.CategoryID == category.CategoryID
                            select new { category = category.CategoryName, unitPrice = product.UnitPrice };

            var catStats = from item in catPrices
                           group item by item.category into cats
                           select new { category = cats.Key, maxPrice = cats.Max(p => p.unitPrice), minPrice = cats.Min(p => p.unitPrice), avgPrice = cats.Average(p => p.unitPrice) };

            NumberFormatInfo setPrecision = new NumberFormatInfo();

            setPrecision.NumberDecimalDigits = 2;

            Console.WriteLine("EX05_10:");

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
                    EX05_01();
                    break;
                case 2:
                    EX05_02();
                    break;
                case 3:
                    EX05_03();
                    break;
                case 4:
                    EX05_04();
                    break;
                case 5:
                    EX05_05();
                    break;
                case 6:
                    EX05_06();
                    break;
                case 7:
                    EX05_07();
                    break;
                case 8:
                    EX05_08();
                    break;
                case 9:
                    EX05_09();
                    break;
                case 10:
                    EX05_10();
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

