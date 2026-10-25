using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Campean_Anca_Lusia_Lab2.Models;

namespace Campean_Anca_Lusia_Lab2.Pages.BookPages
{
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Campean_Anca_Lusia_Lab2Context _context;

        public CreateModel(Campean_Anca_Lusia_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = new Book();

        public IActionResult OnGet()
        {
            Book.BookCategories = new List<BookCategory>();

            PopulateAssignedCategoryData(_context, Book);
            PopulateDropdowns();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            string[]? selectedCategories)
        {
            Book.BookCategories = new List<BookCategory>();

            if (selectedCategories != null)
            {
                foreach (var categoryID in selectedCategories)
                {
                    Book.BookCategories.Add(new BookCategory
                    {
                        CategoryID = int.Parse(categoryID)
                    });
                }
            }

            // Relațiile sunt stabilite prin AuthorID și PublisherID.
            ModelState.Remove("Book.Author");
            ModelState.Remove("Book.Publisher");
            ModelState.Remove("Book.BookCategories");

            if (!ModelState.IsValid)
            {
                PopulateAssignedCategoryData(_context, Book);
                PopulateDropdowns();

                return Page();
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private void PopulateDropdowns()
        {
            var authorList = _context.Authors.Select(a => new
            {
                a.ID,
                FullName = a.LastName + " " + a.FirstName
            });

            ViewData["AuthorID"] = new SelectList(
                authorList,
                "ID",
                "FullName",
                Book.AuthorID);

            ViewData["PublisherID"] = new SelectList(
                _context.Publisher,
                "ID",
                "PublisherName",
                Book.PublisherID);
        }
    }
}