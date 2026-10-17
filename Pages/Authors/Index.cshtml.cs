using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Campean_Anca_Lusia_Lab2.Models;

namespace Campean_Anca_Lusia_Lab2.Pages.AuthorsPages;

public class IndexModel : PageModel
{
    private readonly Campean_Anca_Lusia_Lab2Context _context;

    public IndexModel(Campean_Anca_Lusia_Lab2Context context)
    {
        _context = context;
    }

    public IList<global::Campean_Anca_Lusia_Lab2.Models.Authors> Authors { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Authors = await _context.Authors.ToListAsync();
    }
}
