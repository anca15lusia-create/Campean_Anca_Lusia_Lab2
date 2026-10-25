using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Campean_Anca_Lusia_Lab2.Models;

namespace Campean_Anca_Lusia_Lab2.Pages.BookPages
{
    public class IndexModel : PageModel
    {
        private readonly Campean_Anca_Lusia_Lab2Context _context;

        public IndexModel(Campean_Anca_Lusia_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = new List<Book>();

        public BookData BookD { get; set; } = new BookData();

        public int BookID { get; set; }

        public int CategoryID { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? AuthorID { get; set; }

        public SelectList AuthorOptions { get; set; } = default!;

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            var authorList = await _context.Authors
                .OrderBy(a => a.LastName)
                .Select(a => new
                {
                    a.ID,
                    FullName = a.LastName + " " + a.FirstName
                })
                .ToListAsync();

            AuthorOptions = new SelectList(
                authorList, "ID", "FullName", AuthorID);

            var booksQuery = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .AsNoTracking()
                .AsQueryable();

            if (AuthorID.HasValue)
            {
                booksQuery = booksQuery.Where(
                    b => b.AuthorID == AuthorID.Value);
            }

            Book = await booksQuery
                .OrderBy(b => b.Title)
                .ToListAsync();

            BookD = new BookData
            {
                Books = Book
            };

            if (id.HasValue)
            {
                BookID = id.Value;

                var selectedBook = BookD.Books
                    .FirstOrDefault(b => b.ID == id.Value);

                if (selectedBook != null)
                {
                    BookD.Categories = selectedBook.BookCategories
                        .Select(bc => bc.Category)
                        .ToList();
                }
            }

            if (categoryID.HasValue)
            {
                CategoryID = categoryID.Value;
            }
        }
    }
}