using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MilkCollectionSystem.Data;
using Microsoft.AspNetCore.Authorization;
namespace MilkCollectionSystem.Pages
{
    [Authorize(Roles = "Admin,Clerk")]
    public class MilkRecordsModel : PageModel
    {
        private readonly AppDbContext _context;

        public MilkRecordsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<MilkRecord> Records { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? Date { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.MilkRecords
                .Include(r => r.Farmer)
                .AsQueryable();

            // SEARCH
            if (!string.IsNullOrEmpty(Search))
            {
                query = query.Where(r =>
                    r.Farmer.Name.Contains(Search));
            }

            // DATE FILTER
            if (Date.HasValue)
            {
                query = query.Where(r =>
                    r.CollectionDate.Date == Date.Value.Date);
            }

            Records = await query.ToListAsync();
        }

        // DELETE
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var record = await _context.MilkRecords.FindAsync(id);

            if (record != null)
            {
                _context.MilkRecords.Remove(record);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}