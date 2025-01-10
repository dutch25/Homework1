using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryCatalog catalog = new LibraryCatalog();

            // Add initial items to the catalog
            catalog.AddItem(new Book("1984", "George Orwell", "1949", "Dystopian"));
            catalog.AddItem(new Book("To Kill a Mockingbird", "Harper Lee", "1960", "Fiction"));
            catalog.AddItem(new DVD("Inception", "Christopher Nolan", "2010", "148 minutes"));
            catalog.AddItem(new DVD("The Matrix", "The Wachowskis", "1999", "136 minutes"));

            while (true)
            {
                Console.WriteLine("\nLibrary System Menu:");
                Console.WriteLine("1. Display all items");
                Console.WriteLine("2. Search for an item");
                Console.WriteLine("3. Checkout an item");
                Console.WriteLine("4. Return an item");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nLibrary Catalog:");
                        catalog.DisplayItems();
                        break;

                    case "2":
                        Console.Write("Enter keyword to search: ");
                        string keyword = Console.ReadLine();
                        catalog.FindItem(keyword);
                        break;

                    case "3":
                        Console.Write("Enter the title of the item to checkout: ");
                        string checkoutTitle = Console.ReadLine();
                        var checkoutItem = catalog.GetItems().FirstOrDefault(item => item.Title.Equals(checkoutTitle, StringComparison.OrdinalIgnoreCase));
                        if (checkoutItem != null)
                        {
                            checkoutItem.Checkout();
                        }
                        else
                        {
                            Console.WriteLine("Item not found.");
                        }
                        break;

                    case "4":
                        Console.Write("Enter the title of the item to return: ");
                        string returnTitle = Console.ReadLine();
                        var returnItem = catalog.GetItems().FirstOrDefault(item => item.Title.Equals(returnTitle, StringComparison.OrdinalIgnoreCase));
                        if (returnItem != null)
                        {
                            returnItem.ReturnItem();
                        }
                        else
                        {
                            Console.WriteLine("Item not found.");
                        }
                        break;

                    case "5":
                        Console.WriteLine("Exiting the program. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }

    abstract class LibraryItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublicationDate { get; set; }
        public bool Available { get; set; } = true;

        public LibraryItem(string title, string author, string publicationDate)
        {
            Title = title;
            Author = author;
            PublicationDate = publicationDate;
        }

        public override string ToString()
        {
            string status = Available ? "Available" : "Checked Out";
            return $"{Title} by {Author} ({PublicationDate}) - {status}";
        }

        public abstract void Checkout();
        public abstract void ReturnItem();
    }

    class Book : LibraryItem
    {
        public string Genre { get; set; }

        public Book(string title, string author, string publicationDate, string genre)
            : base(title, author, publicationDate)
        {
            Genre = genre;
        }

        public override void Checkout()
        {
            if (Available)
            {
                Available = false;
                Console.WriteLine($"You have checked out the book: {Title}");
            }
            else
            {
                Console.WriteLine($"Sorry, the book '{Title}' is already checked out.");
            }
        }

        public override void ReturnItem()
        {
            if (!Available)
            {
                Available = true;
                Console.WriteLine($"You have returned the book: {Title}");
            }
            else
            {
                Console.WriteLine($"The book '{Title}' was not checked out.");
            }
        }
    }

    class DVD : LibraryItem
    {
        public string Runtime { get; set; }

        public DVD(string title, string author, string publicationDate, string runtime)
            : base(title, author, publicationDate)
        {
            Runtime = runtime;
        }

        public override void Checkout()
        {
            if (Available)
            {
                Available = false;
                Console.WriteLine($"You have checked out the DVD: {Title}");
            }
            else
            {
                Console.WriteLine($"Sorry, the DVD '{Title}' is already checked out.");
            }
        }

        public override void ReturnItem()
        {
            if (!Available)
            {
                Available = true;
                Console.WriteLine($"You have returned the DVD: {Title}");
            }
            else
            {
                Console.WriteLine($"The DVD '{Title}' was not checked out.");
            }
        }
    }

    class LibraryCatalog
    {
        private List<LibraryItem> items = new List<LibraryItem>();

        public void AddItem(LibraryItem item)
        {
            items.Add(item);
        }

        public void FindItem(string keyword)
        {
            var results = items.Where(item => item.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                           item.Author.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (results.Any())
            {
                foreach (var item in results)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("No items found matching your search.");
            }
        }

        public void DisplayItems()
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        public List<LibraryItem> GetItems()
        {
            return items;
        }
    }
}
