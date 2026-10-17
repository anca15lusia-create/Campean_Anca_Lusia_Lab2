using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PublisherEntity = Campean_Anca_Lusia_Lab2.Models.Publisher;

namespace Campean_Anca_Lusia_Lab2.Pages.Publisher;

public class IndexModel : PageModel
{
    private readonly Campean_Anca_Lusia_Lab2Context _context;

    public IndexModel(Campean_Anca_Lusia_Lab2Context context)
    {
        _context = context;
    }

    public IList<PublisherEntity> Publishers { get; set; }
        = new List<PublisherEntity>();

    public async Task OnGetAsync()
    {
        Publishers = await _context.Set<PublisherEntity>()
            .ToListAsync();
    }
}