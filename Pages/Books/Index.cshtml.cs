using Campean_Anca_Lusia_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Campean_Anca_Lusia_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Campean_Anca_Lusia_Lab2Context _context;

    public IndexModel(Campean_Anca_Lusia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = new List<Book>();

    public SelectList AuthorOptions { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public async Task OnGetAsync()
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
            authors, "ID", "FullName", AuthorID);

        var query = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .AsQueryable();

        if (AuthorID.HasValue)
        {
            query = query.Where(b => b.AuthorID == AuthorID.Value);
        }

        Book = await query.ToListAsync();
    }
}