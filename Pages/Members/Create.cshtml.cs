using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Campean_Anca_Lusia_Lab2.Models;

namespace Campean_Anca_Lusia_Lab2.Pages.Members;

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
    public Member Member { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Member.Add(Member);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
