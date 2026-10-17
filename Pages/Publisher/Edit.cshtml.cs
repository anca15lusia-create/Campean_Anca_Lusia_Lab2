using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PublisherEntity = Campean_Anca_Lusia_Lab2.Models.Publisher;

namespace Campean_Anca_Lusia_Lab2.Pages.Publisher;

public class EditModel : PageModel
{
    private readonly Campean_Anca_Lusia_Lab2Context _context;

    public EditModel(Campean_Anca_Lusia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public PublisherEntity Publisher { get; set; } = new PublisherEntity();

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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var publisher = await _context.Set<PublisherEntity>()
            .FindAsync(Publisher.ID);

        if (publisher is null)
        {
            return NotFound();
        }

        publisher.PublisherName = Publisher.PublisherName;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Set<PublisherEntity>()
                .AnyAsync(p => p.ID == Publisher.ID))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }
}