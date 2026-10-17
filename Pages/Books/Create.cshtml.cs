using Campean_Anca_Lusia_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Campean_Anca_Lusia_Lab2.Pages.BookPages;

public class CreateModel : PageModel
{
    private readonly Campean_Anca_Lusia_Lab2Context _context;

    public CreateModel(Campean_Anca_Lusia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = new Book();

    public SelectList AuthorOptions { get; set; } = default!;
    public SelectList PublisherOptions { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        await LoadOptionsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!Book.AuthorID.HasValue)
        {
            ModelState.AddModelError("Book.AuthorID", "Alege un autor.");
        }
        else if (!await _context.Authors.AnyAsync(a => a.ID == Book.AuthorID))
        {
            ModelState.AddModelError("Book.AuthorID", "Autorul nu există.");
        }

        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync();
            return Page();
        }

        _context.Book.Add(Book);
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