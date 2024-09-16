using BooksStore.Data;
using BooksStore.Helpers;
using BooksStore.Interfaces;
using BooksStore.Repository;

namespace BooksStore
{
     public partial class Program
    {
        public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();
        private static IBook _books;
        private static IAuthor _authors;
        enum ShopMenu
        {
            Books, Authors, Categories, Orders, SearchAuthors, SearchBooks, SearchCategories, SearchOrders, AddBook, AddAuthor, AddCategory, AddOrder, Exit
        }

        static async Task Main()
        {
            Initialize();
            int input = new int();
            do
            {
                input = ConsoleHelper.MultipleChoice(true, new ShopMenu());
                switch ((ShopMenu)input)
                {


                    case ShopMenu.Books:
                        break;
                    case ShopMenu.Authors:
                        await ReviewAuthors();
                        break;
                    case ShopMenu.Categories:
                        break;
                    case ShopMenu.Orders:
                        break;
                    case ShopMenu.SearchAuthors:
                        await SearchAuthors();
                        break;
                    case ShopMenu.SearchBooks:
                        break;
                    case ShopMenu.SearchCategories:
                        break;
                    case ShopMenu.SearchOrders:
                        break;
                    case ShopMenu.AddBook:
                        break;
                    case ShopMenu.AddAuthor:
                        await AddAuthor();
                        break;
                    case ShopMenu.AddCategory:
                        break;
                    case ShopMenu.AddOrder:
                        break;
                    case ShopMenu.Exit:
                        break;
                    default:
                        break;
                };
                Console.WriteLine("\nPress key to continue");
                Console.ReadLine();
                
            } while (ShopMenu.Exit != (ShopMenu)input);



            //var allBooks = await _books.GetAllBooksWithAuthorsAsync();


        }
        static void Initialize()
        {
            new DbInit().Init(DbContext());
            _books = new BookRepository();
            _authors = new AuthorRepository();
        }

    }
}
