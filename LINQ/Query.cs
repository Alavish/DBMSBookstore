using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Globalization;
using LinqEx1; 

namespace test
{

    public class Query
    {
        public Query() // Table that contain the queries used to run the program.

        {
        }

        /*
        public int Query01()
        // On initial Start of the Program new Book <in the top 10 range> are sh.
        {
            BookstoreDB bsdb = new BookstoreDB();

            var homescreen = from Books in bsdb.Books
                             select new { Bk_ID = Books.ISBN, Bk_title = Books.Title, Author = Books.Author, Genre = Books.Genre, Year = Books.YearPublished, Publisher = Books.Publisher };

            NumberFormatInfo setPrecision = new NumberFormatInfo();

            setPrecision.NumberDecimalDigits = 2;

            Console.WriteLine("Query01:");  // Write UI manipulated code from source.

            foreach (var p in homescreen)
            {
                Console.Write("ID: " + p.Bk_ID);
                Console.Write(", Title " + p.Bk_title);
                Console.Write(", Author" + p.Author);
                Console.Write(", Genre" + p.Genre);
                Console.Write(", Year" + p.Year);
                Console.Write(", Publisher" + p.Publisher);
                Console.Write('\n');
            }

            return 0; 
        }

        */




    }

}