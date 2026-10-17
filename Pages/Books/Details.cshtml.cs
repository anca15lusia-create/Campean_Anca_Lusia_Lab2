using Campean_Anca_Lusia_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Campean_Anca_Lusia_Lab2.Pages.BookPages;

public class DetailsModel : PageModel
{
    private readonly Campean_Anca_Lusia_Lab2Context _context;

    public DetailsModel(Campean_Anca_Lusia_Lab2Context context)
    {
        _context = context;
    }

    public Book Book { get; set; } = new Book();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (book is null)
        {
            return NotFound();
        }

        Book = book;
        return Page();
    }
}