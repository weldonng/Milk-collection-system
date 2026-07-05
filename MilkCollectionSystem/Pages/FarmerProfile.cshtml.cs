using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MilkCollectionSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace MilkCollectionSystem.Pages
{
    [Authorize(Roles = "Admin")]
    public class FarmerProfileModel : PageModel
    {
        private readonly AppDbContext _context;

        public FarmerProfileModel(AppDbContext context)
        {
            _context = context;
        }

        public Farmer Farmer { get; set; }

        public List<MilkRecord> Records { get; set; }

        public double TotalLitres { get; set; }

        public double TotalEarnings { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Farmer = await _context.Farmers
                .FirstOrDefaultAsync(f => f.Id == id);

            if (Farmer == null)
                return NotFound();

            Records = await _context.MilkRecords
                .Where(r => r.FarmerId == id)
                .OrderByDescending(r => r.CollectionDate)
                .ToListAsync();

            TotalLitres = Records.Sum(r => r.Litres);
            TotalEarnings = Records.Sum(r => r.Amount);

            return Page();
        }
    }
}