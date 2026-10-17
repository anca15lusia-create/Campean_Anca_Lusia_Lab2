using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublisherEntity = Campean_Anca_Lusia_Lab2.Models.Publisher;

namespace Campean_Anca_Lusia_Lab2.Pages.Publisher;

public class CreateModel : PageModel
{
    private readonly Campean_Anca_Lusia_Lab2Context _context;

    public CreateModel(Campean_Anca_Lusia_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public PublisherEntity Publisher { get; set; } = new PublisherEntity();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Set<PublisherEntity>().Add(Publisher);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}