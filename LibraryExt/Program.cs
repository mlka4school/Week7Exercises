using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibraryExt.Program;

namespace LibraryExt
{
	internal class Program
	{
		// i already did this so i'm not really going to put too much effort into it. i will do the bare minimum though :)
		static void Main(string[] args)
		{
			// make a new library
			Library Lib = new Library();

			// populate with default entries
			Lib.AddBook("AAA-1234","Cool Guy","Return of Shades");
			Lib.AddBook("AAA-5678","Clockmaster","Looking to the Future");

			TopMenu: Console.Clear();
			Console.WriteLine("There are "+Lib.Catalog.Count.ToString()+" books. What would you like to do?");
			Console.WriteLine("1. List books");
			Console.WriteLine("2. Add a book from record");
			Console.WriteLine("3. Remove a book from record");
			Console.WriteLine("4. Check in/out book");
			Console.WriteLine("5. Exit");

			var entry = -1;
			int.TryParse(Console.ReadLine(),out entry);
			switch (entry){
				case 1: // list books
					var title = "Book title";
					var author = "Author";
					var checkstate = "CHK";

					while (title.Count() < 33) title += " ";
					while (author.Count() < 24) author += " ";
					Console.WriteLine(" | "+"ISBN NO."+" | "+checkstate+" | "+author+" | "+title+" | ");
					Console.WriteLine(" ---------------------------------------------------------------------------------");
					foreach (Book book in Lib.Catalog){
						title = book.Title;
						author = book.Author;
						checkstate = "IN ";
						if (book.IsAvailable == false) checkstate = "OUT";

						while (title.Count() < 33) title += " ";
						while (author.Count() < 24) author += " ";
						Console.WriteLine(" | "+book.ISBN+" | "+checkstate+" | "+author+" | "+title+" | ");
					}
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
				goto TopMenu;
				case 2: // add book
					var NewEntry = "";
					Console.WriteLine("Please fill out some information");
					ISBNEntry: Console.WriteLine("ISBN? (ABC-XXXX)");
					var NewISBN = Console.ReadLine();
					if (NewISBN.Length != 8){
						Console.WriteLine("ISBN should at least pretend to be in format");
						goto ISBNEntry;
					}
					var QueryISBN = from book in Lib.Catalog where book.ISBN == NewISBN select book;
					if (QueryISBN.ToList<Book>().Count > 0){
						Console.WriteLine("ISBN already exists");
						goto ISBNEntry;
					}
					AuthEntry: Console.WriteLine("Author?");
					var NewAuth = Console.ReadLine();
					if (NewAuth.Length == 0){
						goto AuthEntry;
					}
					TitleEntry: Console.WriteLine("Title?");
					var NewTitle = Console.ReadLine();
					if (NewTitle.Length == 0){
						goto TitleEntry;
					}
					Lib.AddBook(NewISBN,NewAuth,NewTitle);
					Console.WriteLine("Added new book! Press any key to continue...");
					Console.ReadKey();
				goto TopMenu;
				case 3: // remove book
					Console.WriteLine("ISBN?");
					var CheckRemove = Console.ReadLine();
					if (Lib.RemoveBook(CheckRemove)){
						Console.Write("Book was removed from records. ");
					}else{
						Console.Write("Book was not found. ");
					}
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
				goto TopMenu;
				case 4: // check book
					Console.WriteLine("ISBN?");
					var CheckFind = Console.ReadLine();
					var BookRes = Lib.CheckBook(CheckFind);
					switch (BookRes){
						case -1:
							Console.Write("Book was not found. ");
						break;
						case 0:
							Console.Write("Book checked out successfully. ");
						break;
						case 1:
							Console.Write("Book checked in successfully. ");
						break;
					}
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
				goto TopMenu;
				case 5:
				return;
				default:
				goto TopMenu;
			}
		}

		public class Library{
			public List<Book> Catalog = new List<Book>();

			private Book FindBookFromISBN(string ISBN){ // this is private since nobody else needs to use it.
				var Query = from book in Catalog where book.ISBN == ISBN select book;
				if (Query.ToList<Book>().Count > 0){
					return Query.ToList<Book>()[0];
				}else{
					return null;
				}
			}

			public void AddBook(string newISBN, string newAuth, string newTitle){
				Catalog.Add(new Book(newISBN,newAuth,newTitle));
			}

			public bool RemoveBook(string ISBN){ // in or out depending on what is needed.
				var find = FindBookFromISBN(ISBN);
				if (find != null){
					// cull the weak
					Catalog.Remove(find);
					return true;
				}else{
					return false; // didn't find a book
				}
			}

			public int CheckBook(string ISBN){ // in or out depending on what is needed.
				var find = FindBookFromISBN(ISBN);
				if (find != null){
					if (find.IsAvailable){ // check out
						find.IsAvailable = false;
						return 0;
					}else{ // check in
						find.IsAvailable = true;
						return 1;
					}
				}else{
					return -1; // didn't find a book
				}
			}
		}

		public class Book{
			public Book(string newISBN, string newAuth, string newTitle){
				ISBN = newISBN;
				Author = newAuth;
				Title = newTitle;

				IsAvailable = true;
			}
			public string ISBN {get;set;}
			public string Author {get;set;}
			public string Title {get;set;}
			public bool IsAvailable {get;set;}
		}
	}
}
