using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MilkCollectionSystem.Data;
using Microsoft.AspNetCore.Authorization;
namespace MilkCollectionSystem.Pages
{

    [Authorize(Roles = "Admin,Clerk")]
    public class FarmersModel : PageModel
    {
        private readonly AppDbContext _context;

        public FarmersModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Farmer> Farmers { get; set; }

        public async Task OnGetAsync()
        {
            Farmers = await _context.Farmers.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var farmer = await _context.Farmers.FindAsync(id);

            if (farmer != null)
            {
                _context.Farmers.Remove(farmer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}