using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Campean_Anca_Lusia_Lab2.Models;

namespace Campean_Anca_Lusia_Lab2.Pages.BookPages
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Campean_Anca_Lusia_Lab2Context _context;

        public EditModel(Campean_Anca_Lusia_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ID == id);

            if (book == null)
            {
                return NotFound();
            }

            Book = book;

            PopulateAssignedCategoryData(_context, Book);
            PopulateDropdowns();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                b => b.Title,
                b => b.AuthorID,
                b => b.Price,
                b => b.PublishingDate,
                b => b.PublisherID))
            {
                UpdateBookCategories(
                    _context, selectedCategories, bookToUpdate);

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            UpdateBookCategories(
                _context, selectedCategories, bookToUpdate);

            Book = bookToUpdate;

            PopulateAssignedCategoryData(_context, Book);
            PopulateDropdowns();

            return Page();
        }

        private void PopulateDropdowns()
        {
            var authorList = _context.Authors.Select(a => new
            {
                a.ID,
                FullName = a.LastName + " " + a.FirstName
            });

            ViewData["AuthorID"] = new SelectList(
                authorList, "ID", "FullName", Book.AuthorID);

            ViewData["PublisherID"] = new SelectList(
                _context.Publisher,
                "ID",
                "PublisherName",
                Book.PublisherID);
        }
    }
}