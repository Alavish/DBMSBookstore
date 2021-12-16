// Nana Amoah, Anthony Christopher, and Matthew Lochridge
// CS 374 Final Project - Bookstore Embedded SQL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Globalization;

[Table]
class Customers
{
    [Column(Name = "ID")]
    public int ID;
    [Column(Name = "Name")]
    public string Name;
    [Column(Name = "ContactInfo")]
    public string ContactInfo;
    [Column(Name = "YearJoined")]
    public int YearJoined;
}

[Table]
class Employees
{
    [Column(Name = "ID")]
    public int ID;
    [Column(Name = "Name")]
    public string Name;
    [Column(Name = "ContactInfo")]
    public string ContactInfo;
    [Column(Name = "YearJoined")]
    public int YearJoined;
}

[Table]
class Books
{
    [Column(Name = "ISBN")]
    public int ISBN;
    [Column(Name = "Title")]
    public string Title;
    [Column(Name = "Author")]
    public string Author;
    [Column(Name = "Genre")]
    public string Genre;
    [Column(Name = "YearPublished")]
    public int YearPublished;
    [Column(Name = "Publisher")]
    public string Publisher;
}

[Table]
class Inventory
{
    [Column(Name = "ID")]
    public int ID;
    [Column(Name = "ISBN")]
    public int ISBN;
    [Column(Name = "CopyNumber")]
    public int CopyNumber;
    [Column(Name = "Condition")]
    public string Condition;
}

[Table]
class Prices
{
    [Column(Name = "ID")]
    public int ID;
    [Column(Name = "ISBN")]
    public int ISBN;
    [Column(Name = "Condition")]
    public string Condition;
    [Column(Name = "PurchasePrice")]
    public decimal PurchasePrice;
    [Column(Name = "RentalPrice")]
    public decimal RentalPrice;
    [Column(Name = "LateFee")]
    public decimal LateFee;
}

[Table]
class Sales
{
    [Column(Name = "ID")]
    public int ID;
    [Column(Name = "CustomerID")]
    public int CustomerID;
    [Column(Name = "EmployeeID")]
    public int EmployeeID;
    [Column(Name = "Date_Time")]
    public DateTime Date_Time;
    [Column(Name = "InventoryID")]
    public int InventoryID;
    [Column(Name = "Discount")]
    public bool Discount;
}

[Table]
class Rentals
{
    [Column(Name = "ID")]
    public int ID;
    [Column(Name = "CustomerID")]
    public int CustomerID;
    [Column(Name = "EmployeeID")]
    public int EmployeeID;
    [Column(Name = "Date_Time")]
    public DateTime Date_Time;
    [Column(Name = "InventoryID")]
    public int InventoryID;
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
    public int ID;
    [Column(Name = "CustomerID")]
    public int CustomerID;
    [Column(Name = "EmployeeID")]
    public int EmployeeID;
    [Column(Name = "Date_Time")]
    public DateTime Date_Time;
    [Column(Name = "ISBN")]
    public int ISBN;
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

    public BookstoreDB() : base(@"Data Source=CS1;Initial Catalog=bookstore;Integrated Security=True") { }
}

class Bookstore
{
    static void Startup(BookstoreDB bsdb)
    // On initial Start of the Program new Book  are sh.
    {
        var selection = from book in bsdb.Books
                        select book;

        NumberFormatInfo setPrecision = new NumberFormatInfo();

        setPrecision.NumberDecimalDigits = 2;

        Console.WriteLine("Startup:");  // Write UI manipulated code from source.
        Console.WriteLine("Title\tAuthor\tYear\tGenre");
        foreach (var book in selection)
        {
            Console.WriteLine(book.Title + "\t" + book.Author + "\t" + book.YearPublished + "\t" + book.Genre);
        }
    }
    /*
    static void Query02(var bsdb)
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

    static void Query03(var bsdb) ///
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

    static void Query04(var bsdb)
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

    static void Query05(var bsdb)
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

    static void Query06(var bsdb)
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

    static void Query07(var bsdb)
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

    static void Query08(var bsdb)
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

    static void Query09(var bsdb)
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

    static void Query10(var bsdb)
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

    static void selection(var bsdb)
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
    */
    static void Main(string[] args)
    {
        BookstoreDB bsdb = new BookstoreDB();
        // TODO: Login procedure
        Startup(bsdb);
        string input = null;
        do {
            if (input == null) // Initial loop on login
            {
                Console.WriteLine("Enter 's' to search by attribute(s). Enter 't' to make a transaction. Enter nothing to log out.");
            }
            else if (input == "s" || input == "S")
            {
                Console.WriteLine("Enter 't' to sort by Title. Enter 'a' to sort by Author. Enter 'y' to sort by Year. Enter 'g' to sort by Genre. Enter nothing to log out.");
                sorting:
                input = Console.ReadLine();
                    if (input == "t" || input == "T")
                    {
                        // TODO: sort Inventory by Title; prompt user input; break;
                    }
                    else if (input == "a" || input == "A")
                    {
                        // TODO: sort Inventory by Author; prompt user input; break;
                    }
                    else if (input == "y" || input == "Y")
                    {
                        // TODO: sort Inventory by YearPublished; prompt user input; break;
                    }
                    else if (input == "g" || input == "G")
                    {
                        // TODO: sort Inventory by Genre; prompt user input; break;
                    }
                    else if (input == "")
                    {
                        break; // exit if-cluster and outer do-while
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter 't' to sort by Title. Enter 'a' to sort by Author. Enter 'y' to sort by Year. Enter 'g' to sort by Genre. Enter nothing to log out.");
                        goto sorting;
                    }

            }
            else if (input == "t" || input == "T")
            {
                Console.WriteLine("Enter 'p' to reserve a purchase. Enter 'r' to reserve a rental. Enter 's' to offer a sale. Enter nothing to log out.");
                transaction:
                    input = Console.ReadLine();
                    if (input == "p" || input == "P")
                    {
                        Console.WriteLine("Enter your selected titles separated by commas:");

                        // TODO: ^
                    }
                    else if (input == "r" || input == "R")
                    {
                        Console.WriteLine("Enter your selected titles separated by commas:");

                        // TODO: ^
                    }
                    else if (input == "s" || input == "S")
                    {
                        // TODO: Display user's collection; will probably be empty on login (should we store it clientside in this repo?); prompt if user collection is empty (goto transaction)
                    }
                    else if (input == "")
                    {
                        break; // exit if-cluster and outer do-while
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter 'p' to reserve a purchase. Enter 'r' to reserve a rental. Enter 's' to offer a sale. Enter nothing to log out.");
                        goto transaction;
                    }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter 's' to sort by attribute(s). Enter 't' to make a transaction. Enter nothing to log out.");
            }
            input = Console.ReadLine();
        } while (input != "");
    }
}

