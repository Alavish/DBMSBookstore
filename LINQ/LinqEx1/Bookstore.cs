// Nana Amoah, Anthony Christopher, and Matthew Lochridge
// CS 374 Final Project - Bookstore Embedded SQL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Globalization;
using System.Data;
using utilities; // operation for more database utilization. 


    [Table]
    public class Customers
    {
        [Column(Name = "ID",IsPrimaryKey =true, IsDbGenerated =true,AutoSync =AutoSync.OnInsert, DbType ="INT NOT NULL IDENTITY")]
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
        [Column(Name = "ID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert, DbType = "INT NOT NULL IDENTITY")]
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
        [Column(Name = "ISBN", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert, DbType = "INT NOT NULL IDENTITY")]
        public int ISBN;
        [Column(Name = "Title")]
        public string Title;
        [Column(Name = "Author")]
        public string Author;
        [Column(Name = "Genre")]
        public string Genre;
        [Column(Name = "YearPublished")]
        public string YearPublished;
        [Column(Name = "Publisher")]
        public string Publisher;
    }

    [Table]
    class Inventory
    {
        [Column(Name = "ID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert, DbType = "INT NOT NULL IDENTITY")]
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
        [Column(Name = "ID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert, DbType = "INT NOT NULL IDENTITY")]
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
        [Column(Name = "ID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert, DbType = "INT NOT NULL IDENTITY")]
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
        [Column(Name = "ID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert, DbType = "INT NOT NULL IDENTITY")]
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
        [Column(Name = "ID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert, DbType = "INT NOT NULL IDENTITY")]
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


/**
 * @description This Class handles the insertion operations from the queries. 
 * @citation https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/how-to-insert-rows-into-the-database
 */
class Utility
{

    public BookstoreDB data_base; 
    public Utility() { } // Constructor.


    /**
     * @description Inserts Customers Records to the Database.
     * @param Customers type to be inserted to the table. 
     * @return true when successful. 
     */
    public bool InsertCustomer(Customers user )
    {
        data_base = new BookstoreDB(); // API to comm with the server. 

        // Add the new object to the Customers collection. 
        data_base.Customers.InsertOnSubmit(user);


        // Submit the Changes to the database. 
        try
        {
            data_base.SubmitChanges();
            return true; 
        } catch (Exception e)
        {
            Console.WriteLine(e); 
        }

        return false; // not successful. 

    }

    /**
 * @description Inserts Employees Records to the Database.
 * @param Employees type to be inserted to the table. 
 * @return true when successful. 
 */
    public bool InsertEmployees(Employees work)
    {
        data_base = new BookstoreDB(); // API to comm with the server. 

        // Add the new object to the Customers collection. 
        data_base.Employees.InsertOnSubmit(work);


        // Submit the Changes to the database. 
        try
        {
            data_base.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false; // not successful. 

    }

    /**
     * @description Inserts Transactions into purchases of the user. 
     * @param Purchase tuple of transaction
     * @return true when insertion completed.
     * 
     */
    public bool InsertPurchases(Purchases transact)
    {

        data_base = new BookstoreDB(); // APi to comm with the server

        // Add the new object to the Purchase collection
        data_base.Purchases.InsertOnSubmit(transact);


        // Submit the Changes to the database. 
        try
        {
            data_base.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false; // not successful. 

    }

    /**
  * @description Inserts Rentals into purchases of the user. 
  * @param Rentals tuple of transaction
  * @return true when insertion completed.
  * 
  */
    public bool InsertRentals(Rentals transact)
    {
        data_base = new BookstoreDB(); // APi to comm with the server

        // Add the new object to the Purchase collection
        data_base.Rentals.InsertOnSubmit(transact);


        // Submit the Changes to the database. 
        try
        {
            data_base.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false; // not successful.

    }

  
}




class Bookstore
{
    /**
     * @Description Enables the User to log in before accessing the database for security purposes | create a new user and insert the value into the server. 
     * @param takes in the API to communicate with the database. 
     * @return a Customer datatype
     * */
    static Customers Login(BookstoreDB bsdb)
    {
        string result = new string('-', 110); // for indenting purposes.
        Console.WriteLine(result);
        // Display a Welcome page to the user to log in. 
        string userID;
        Console.WriteLine("Hello Welcome to Glass BookStore... Log in to proceed/n");
        Console.Write("Enter your Unique bookstore ID...");
        userID = Console.ReadLine(); //
        bool done = false; // used to create a new customer. 

        Customers user_data;

        // Query to get Customer data from the table. 
        var data = from users in bsdb.Customers
                   select new { iD = users.ID, name = users.Name, cinfo = users.ContactInfo, yr = users.YearJoined };

        // Find the User in the table. 
        foreach (var p in data)
        {
            if (p.iD.ToString() == userID) // equality check to see if the user exists.
            { 

                // if it does obtain the info of the user. 
                user_data = new Customers
                {
                    ID = p.iD,
                    Name = p.name,
                    ContactInfo = p.cinfo,
                    YearJoined = p.yr
                };

                done = true;
                return user_data; // return the Customer found
            }
        }

        // if user data is not available. A new customer data is created.
        if (!done)
        {
            Console.Write(" Hello, it seems your data is not found in the server. We will let you create one\n");
            int iD, yr = 2021;
            string name, info;

            // iD
            Console.Write("Enter your ID: ");
            string temp = Console.ReadLine();
            iD = Convert.ToInt32(temp);

            // name
            Console.Write("Enter your name: ");
            name = Console.ReadLine();

            // Contact Info. 
            Console.Write("Enter your Phone number: ");
            info = Console.ReadLine();

            user_data = new Customers { /*ID = iD*/ Name = name, YearJoined = yr, ContactInfo = info };

            // Perform the Insertion Operation. 
            Utility send = new Utility();

            if (send.InsertCustomer(user_data)) { return user_data;  } // insert the data to the database.
        }

        Console.WriteLine(result);

        Console.Clear(); // clear the screen.

        return null; // if it fails

    }

    /**
     * @description This query displays to the user the books in the inventory of the table. 
     * @param bsdb Object that handles the communication between the program and the database. 
     * @param user gets specific information of the user to display the information to. 
     */
    static int Startup(BookstoreDB bsdb, Customers user)
    // On initial Start of the Program new Book  are shown.
    {
        Console.WriteLine(" Hello..." + user.Name + "\n");
        Console.WriteLine(" These are the books currently in the Inventory\t");

        // Query to Get the Books currently in the inventory of the bookstore. 
        var data_books = (from books in bsdb.Books
                          join items in bsdb.Inventory
                          on books.ISBN equals items.ISBN
                          join prices in bsdb.Prices on
                          new { books.ISBN, items.Condition }
                          equals new { prices.ISBN, prices.Condition }
                          select new { iSBN = books.ISBN, title = books.Title, author = books.Author, yr = books.YearPublished, genre = books.Genre, price = prices.PurchasePrice, rental=prices.RentalPrice, condition = prices.Condition });
        //

        // Precision setters. 
        NumberFormatInfo setPrecision = new NumberFormatInfo();
        setPrecision.NumberDecimalDigits = 2;
        string result = new string('-', 115); // for indenting purposes.

        { // Formatting gimmick to handle drawing a neat table to show to the user. 
            Console.WriteLine(result);
            Console.WriteLine(String.Format("|{0,5}|{1,30}|{2,25}|{3,3}|{4,20}|{5,5}|{6,5}|{7,6}|", "ISBN", "Title", "Author", "Year", "Genre", "Price","Rental", "Condition"));
            Console.WriteLine(result);

            foreach (var p in data_books)
            {
                // Display the Data from the server to the user. 
                Console.WriteLine(String.Format("|{0,5}|{1,30}|{2,25}|{3,3}|{4,20}|{5,5}|{6,5}|{7,10}|", p.iSBN, p.title, p.author, p.yr, p.genre, Math.Round(p.price, 2), Math.Round(p.rental,2), p.condition));



            }
            Console.WriteLine(result);
        }

        // Show the Options here later on. 
        Console.WriteLine("1: Buy a Book 2: Rent a Book 3: ");
        Console.Write("Press a number to know what you want to do today \n");
        int input = Convert.ToInt32((Console.ReadLine()));


        return input; 
    }


    /**
     * @description This function handles the purchase of a book by the user. 
     * @param bsdb APi to communicate with the SQL server
     * @param user Personalized Customer who's purchasing a book his info is used to generate transactions and receipts. 
     * @return true when the user is done shopping. 
     * 
     */
     static bool BuyBook(BookstoreDB bsdb, Customers user, Employees job)
    {
        // Console.WriteLine("Foo");
        // Create a loop to allow multiple buying as well as generate transactions

        // variables to be used 
        int done = 0;
        string leave; // used to leave the loop of the program. 
        Utility trxt = new Utility(); // for performing insertion operations. 
        Purchases buy = new Purchases();


        do
        {

            Console.Write("Enter the ISBN of the Book you want to buy from the Table above. <Check your input correctly> -->  ");
            string input_ISBN = Console.ReadLine(); // get the user's ISBN choice. 

            Console.Write("Enter the condition you want the book to be in. <Check your input correctly> -->");
            string input_cdtn = Console.ReadLine(); // read the conditon of the book.

            // DO: Perform a search on the database with the ISBN key

            // Query to Seach the inventory for a specific book by using the ISBN as index. 
            var data_books = (from books in bsdb.Books
                              join items in bsdb.Inventory on books.ISBN equals items.ISBN 
                              where books.ISBN == Convert.ToInt32(input_ISBN) && items.Condition==input_cdtn
                              select new { iSBN = books.ISBN, title = books.Title, author = books.Author, yr = books.YearPublished, genre = books.Genre, publisher = books.ISBN, cdtion=items.Condition }
                );

            // Display the tuple to the user. 
            foreach (var p in data_books)
            {
                Console.WriteLine(String.Format("|{0,5}|{1,30}|{2,25}|{3,3}|{4,20}|", p.iSBN, p.title, p.author, p.yr, p.genre));
            }

            Console.WriteLine("Is that the correct book? (y/n): ");
            leave = Console.ReadLine(); // take input. 
            DateTime today = DateTime.Today; //Get the current date the book's purchase was made.

            if( leave == "y")
            {
                foreach (var p in data_books) {

                    // Create a Purchase Object to insert appropriate data into it. 
                    buy = new Purchases
                    {
                        ISBN = p.iSBN,
                        CustomerID = user.ID,
                        EmployeeID = job.ID,
                        Date_Time = today,
                        Condition = p.cdtion

                    };
                 }

                /* Insert into Purchases*/
                trxt.InsertPurchases(buy);

              
                done = 1;

            }


            else { done = 0;} // compute if the user is satisfied with the results. 

        } while (done != 1);

        return true;  

    }

    /**
 * @description This function handles the rental of a book by the user. 
 * @param bsdb APi to communicate with the SQL server
 * @param user Personalized Customer who's purchasing a book his info is used to generate transactions and receipts. 
 * @param Employee who handles the rental.
 * @return true when the user is done shopping. 
 * 
 */
    static bool rentABook(BookstoreDB bsdb, Customers user, Employees job)
    {
        // Console.WriteLine("Foo");
        // Create a loop to allow multiple buying as well as generate transactions

        // variables to be used 
        int done = 0;
        string leave; // used to leave the loop of the program. 
        Utility trxt = new Utility(); // for performing insertion operations. 
        Rentals buy = new Rentals();


        do
        {

            Console.Write("Enter the ISBN of the Book you want to Rent from the Table above. <Check your input correctly> -->  ");
            string input_ISBN = Console.ReadLine(); // get the user's ISBN choice. 

            Console.Write("Enter the condition you want the book to be in. <Check your input correctly> -->");
            string input_cdtn = Console.ReadLine(); // read the conditon of the book.

            // DO: Perform a search on the database with the ISBN key

            // Query to Seach the inventory for a specific book by using the ISBN as index. 
            var data_books = (from books in bsdb.Books
                              join items in bsdb.Inventory on books.ISBN equals items.ISBN
                              join prices in bsdb.Prices on items.ISBN equals prices.ISBN
                              where books.ISBN == Convert.ToInt32(input_ISBN) && items.Condition == input_cdtn && items.Condition == prices.Condition
                              select new {iSBN = books.ISBN, ivID = items.ID, title = books.Title, author = books.Author, yr = books.YearPublished, genre = books.Genre, publisher = books.ISBN, cdtion = items.Condition, Ltfee = prices.LateFee, Rtpr = prices.RentalPrice, fee=prices.LateFee}
                );

            // Display the tuple to the user. 
            foreach (var p in data_books)
            {
                Console.WriteLine(String.Format("|{0,5}|{1,30}|{2,25}|{3,3}|{4,20}|{5,10}|{6,5}|", p.iSBN, p.title, p.author, p.yr, p.genre, p.Rtpr, p.cdtion));
            }

            Console.WriteLine("Is that the correct book? (y/n): ");
            leave = Console.ReadLine(); // take input. 
            DateTime today = DateTime.Today; //Get the current date the book's purchase was made.

            if (leave == "y")
            {
                foreach (var p in data_books)
                {

                    // Create a Purchase Object to insert appropriate data into it. 
                    buy = new Rentals
                    {
                        InventoryID = p.ivID,
                        CustomerID = user.ID,
                        EmployeeID = job.ID,
                        Date_Time = today,
                        DateDue = DateTime.Now.AddDays(10),
                        DateReturned = today,
                        Discount =false,
                        
                    };
                }

                /* Insert into Rentals*/
                trxt.InsertRentals(buy);

                done = 1;
            }

            else { done = 0; } // compute if the user is satisfied with the results. 

        } while (done != 1);

        return true; // when done. 
    }
      


    /**
     * @description Get an Employee from the user by specifying the ID. 
     * @return data of type Employee
     */
     static Employees getEmployee(BookstoreDB bsdb) 
    {

        string result = new string('-', 110); // for indenting purposes.
        Console.WriteLine(result);
        // Display a Welcome page to the user to log in. 
        string userID;
        Console.WriteLine("Hello Welcome to Glass BookStore...n");
        Console.Write("Enter your Employee ID...");
        userID = Console.ReadLine(); //
        bool done = false; // used to create a new customer. 

        Employees user_data;

        // Query to get Customer data from the table. 
        var data = from users in bsdb.Employees
                   select new { iD = users.ID, name = users.Name, cinfo = users.ContactInfo, yr = users.YearJoined };

        // Find the User in the table. 
        foreach (var p in data)
        {
            if (p.iD.ToString() == userID) // equality check to see if the employee exists.
            {

                // if it does obtain the info of the user. 
                user_data = new Employees
                {
                    ID = p.iD,
                    Name = p.name,
                    ContactInfo = p.cinfo,
                    YearJoined = p.yr
                };

                done = true;
                return user_data; // return the Employees found
            }
        }

        // if user data is not available. A new customer data is created.
        if (!done)
        {
            Console.Write(" Hello, it seems your data is not found in the server. We will let you create one\n");
            int iD, yr = 2021;
            string name, info;

            // iD
            Console.Write("Enter your ID: ");
            string temp = Console.ReadLine();
            iD = Convert.ToInt32(temp);

            // name
            Console.Write("Enter your name: ");
            name = Console.ReadLine();

            // Contact Info. 
            Console.Write("Enter your Phone number: ");
            info = Console.ReadLine();

            user_data = new Employees { /*ID = iD*/ Name = name, YearJoined = yr, ContactInfo = info };

            // Perform the Insertion Operation. 
            Utility send = new Utility();

            if (send.InsertEmployees(user_data)) { return user_data; } // insert the data to the database.
        }

        Console.WriteLine(result);

        Console.Clear(); // clear the screen.

        return null; // if it fails


    }
    static void Main(string[] args)
    {
        BookstoreDB bsdb = new BookstoreDB();

        // Get an employee
        Employees worker = getEmployee(bsdb); // get employee details.

        Utility inst_emp = new Utility(); // employees. 
        //inst_emp.InsertEmployees(worker);


        // IQueryable inventory = getBookstoreDB(bsdb);
        // TODO: Login procedure
        // Startup(bsdb);

        Customers cs = Login(bsdb); // Log the User trying to access the database into the system.

        // Case : Perform the Startup of the Program. During the Startup ask the User what he/she wants to do. 


        /////////// Loop to Run the Homescreen of the Main Program. 
        int choise = 0; 
        choise = Startup(bsdb, cs); // Home Screen sort of the BookStore. 

         switch(choise)
        {
            case 1:
                BuyBook(bsdb, cs, worker);
                break;

            case 2:
                rentABook(bsdb, cs, worker); // call it when you want to rent book
                break;
            default:
                Console.WriteLine("No input selected");
                break; 
        }

        /////////////

        /*
            string input = null;
            do
            {
            startup:
                if (input == null) // Initial loop on login
                {
                    Console.WriteLine("Enter 's' to sort by attribute. Enter 't' to make a transaction. Enter nothing to log out.");
                }
                else if (input == "s" || input == "S")
                {
                    Console.WriteLine("Enter 't' to sort by Title. Enter 'a' to sort by Author. Enter 'y' to sort by Year. Enter 'g' to sort by Genre. Enter nothing to log out.");
                sorting:
                    input = Console.ReadLine();
                    if (input == "t" || input == "T")
                    {
                        var db = from book in bsdb.Books
                                 orderby book.Title
                                 select book;
                        Console.WriteLine("Title\tAuthor\tYear\tGenre");
                        foreach (var book in db)
                        {
                            Console.WriteLine(book.Title + "\t" + book.Author + "\t" + book.YearPublished + "\t" + book.Genre);
                        }
                        input = null;
                        goto startup;
                    }
                    else if (input == "a" || input == "A")
                    {
                        var db = from book in bsdb.Books
                                 orderby book.Author
                                 select book;
                        Console.WriteLine("Title\tAuthor\tYear\tGenre");
                        foreach (var book in db)
                        {
                            Console.WriteLine(book.Title + "\t" + book.Author + "\t" + book.YearPublished + "\t" + book.Genre);
                        }
                        input = null;
                        goto startup;
                    }
                    else if (input == "y" || input == "Y")
                    {
                        var db = from book in bsdb.Books
                                 orderby book.YearPublished
                                 select book;
                        Console.WriteLine("Title\tAuthor\tYear\tGenre");
                        foreach (var book in db)
                        {
                            Console.WriteLine(book.Title + "\t" + book.Author + "\t" + book.YearPublished + "\t" + book.Genre);
                        }
                        input = null;
                        goto startup;
                    }
                    else if (input == "g" || input == "G")
                    {
                        var db = from book in bsdb.Books
                                 orderby book.Genre
                                 select book;
                        Console.WriteLine("Title\tAuthor\tYear\tGenre");
                        foreach (var book in db)
                        {
                            Console.WriteLine(book.Title + "\t" + book.Author + "\t" + book.YearPublished + "\t" + book.Genre);
                        }
                        input = null;
                        goto startup;
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
                        // TODO: Display user's collection; will probably be empty on login (should we store it clientside in main?); prompt if user collection is empty (goto transaction)
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
                    Console.WriteLine("Invalid input. Enter 's' to sort by attribute. Enter 't' to make a transaction. Enter nothing to log out.");
                }
                input = Console.ReadLine();
            } while (input != "");
        }

        */
    }
}

