using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PublisherEntity = Campean_Anca_Lusia_Lab2.Models.Publisher;

namespace Campean_Anca_Lusia_Lab2.Pages.Publisher;

public class DeleteModel : PageModel
{
    private readonly Campean_Anca_Lusia_Lab2Context _context;

    public DeleteModel(Campean_Anca_Lusia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public PublisherEntity Publisher { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var publisher = await _context.Set<PublisherEntity>()
            .FirstOrDefaultAsync(p => p.ID == id);

        if (publisher is null)
        {
            return NotFound();
        }

        Publisher = publisher;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var publisher = await _context.Set<PublisherEntity>()
            .FindAsync(id.Value);

        if (publisher != null)
        {
            _context.Set<PublisherEntity>().Remove(publisher);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}