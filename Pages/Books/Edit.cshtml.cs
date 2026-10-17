using Campean_Anca_Lusia_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Campean_Anca_Lusia_Lab2.Pages.BookPages;

public class EditModel : PageModel
{
    private readonly Campean_Anca_Lusia_Lab2Context _context;

    public EditModel(Campean_Anca_Lusia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = new Book();

    public SelectList AuthorOptions { get; set; } = default!;
    public SelectList PublisherOptions { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .FirstOrDefaultAsync(b => b.ID == id);

        if (book is null)
        {
            return NotFound();
        }

        Book = book;
        await LoadOptionsAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!Book.AuthorID.HasValue)
        {
            ModelState.AddModelError(
                "Book.AuthorID", "Alege un autor.");
        }
        else if (!await _context.Authors
            .AnyAsync(a => a.ID == Book.AuthorID))
        {
            ModelState.AddModelError(
                "Book.AuthorID", "Autorul nu există.");
        }

        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync();
            return Page();
        }

        var bookToUpdate = await _context.Book.FindAsync(Book.ID);

        if (bookToUpdate is null)
        {
            return NotFound();
        }

        bookToUpdate.Title = Book.Title;
        bookToUpdate.AuthorID = Book.AuthorID;
        bookToUpdate.Price = Book.Price;
        bookToUpdate.PublishingDate = Book.PublishingDate;
        bookToUpdate.PublisherID = Book.PublisherID;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private async Task LoadOptionsAsync()
    {
        var authors = await _context.Authors
            .OrderBy(a => a.LastName)
            .Select(a => new
            {
                a.ID,
                FullName = a.FirstName + " " + a.LastName
            })
            .ToListAsync();

        AuthorOptions = new SelectList(
            authors, "ID", "FullName", Book.AuthorID);

        var publishers = await _context.Publisher
            .OrderBy(p => p.PublisherName)
            .ToListAsync();

        PublisherOptions = new SelectList(
            publishers, "ID", "PublisherName", Book.PublisherID);
    }
}