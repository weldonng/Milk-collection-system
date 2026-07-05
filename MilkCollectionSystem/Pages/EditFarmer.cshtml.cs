using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MilkCollectionSystem.Data;

namespace MilkCollectionSystem.Pages
{
    [Authorize(Roles = "Admin")]
    public class EditFarmerModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditFarmerModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Farmer Farmer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Farmer = await _context.Farmers.FindAsync(id);

            if (Farmer == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var farmerInDb = await _context.Farmers.FindAsync(Farmer.Id);

            if (farmerInDb == null)
                return NotFound();

            farmerInDb.Name = Farmer.Name;
            farmerInDb.Phone = Farmer.Phone;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Farmers");
        }
    }
}