using BooksStore.Helpers;
using BooksStore.Models;
using BooksStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore
{
    public partial class Program
    {
        static async Task ReviewAuthors()
        {
            var allAuthors = await _authors.GetAllAuthorsAsync();
            var authors = allAuthors.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
            int result = ItemsHelper.MultipleChoice(true, authors, true);
            if (result != 0)
            {
                var currentAuthors = await _authors.GetAuthorAsync(result);
                await AuthorInfo(currentAuthors);
            }
        }
        static async Task AuthorInfo(Author CurrentAuthor)
        {
            int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Browes Books" },
                new ItemView { Id = 2, Value = "Edit Author" },
                new ItemView { Id = 3, Value = "Delete Author" },
            },
            IsMenu: true, message: String.Format("{0}\n", CurrentAuthor), startY: 5, optionsPerLine: 1);

            switch (result)
            {
                case 1: 
                    {
                        break;
                    }
                case 2:
                    {
                        await EditAuthor(CurrentAuthor);
                        Console.ReadLine();
                        break;
                    }
                case 3:
                    {
                        await RemoveAuthor(CurrentAuthor);
                        Console.ReadLine();
                        break;
                    }
            }
        }
        static async Task EditAuthor(Author currentAuthor)
        {
            Console.WriteLine("Changing: {0}", currentAuthor.Name);
            currentAuthor.Name = InputHelper.GetString("author 'Name' with 'SurName'");
            await _authors.EditAuthorAsync(currentAuthor);
            Console.WriteLine("Author Successfully changed");
        }
        static async Task RemoveAuthor(Author currentAuthor)
        {
            int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "yes" },
                new ItemView { Id = 2, Value = "no" }
            },
            message: string.Format("[Are you sure delete the author {0} ?]\n", currentAuthor.Name), startY: 2);
            if (result == 1)
            {
                await _authors.DeleteAuthorAsync(currentAuthor);
                Console.WriteLine("Author has Deleted");
            }
            else
            {
                Console.WriteLine("press key to continue");
            }
        }
        static async Task AddAuthor()
        {
            string authortName = InputHelper.GetString("author 'Name' with 'SurName'");
            await _authors.AddAuthorAsync(new Author
            {
                Name = authortName,
            });
            Console.WriteLine("Author added");
        }
        static async Task SearchAuthors()
        {
            string authorName = InputHelper.GetString("author name or surname");
            var currentAuthor = await _authors.GetAuthorsByNameAsync(authorName);
            if (currentAuthor.Count() > 0)
            {
                var author = currentAuthor.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
                int result = ItemsHelper.MultipleChoice(true, author, true);
                if (result != 0)
                {
                    var currentauthor = await _authors.GetAuthorAsync(result);
                    await AuthorInfo(currentauthor);
                }
            }
            else
            {
                Console.WriteLine("no authors were not found");
            }
        }
    }
}

