using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkCollectionSystem.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
namespace MilkCollectionSystem.Pages
{
    
    
 [Authorize(Roles = "Admin,Clerk")]
public class AddFarmerModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddFarmerModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [BindProperty]
        [RegularExpression(@"^(07\d{8}|01\d{8})$",
        ErrorMessage = "Phone number must be 10 digits and start with 07 or 01")]
        public string Phone { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var farmer = new Farmer
            {
                Name = Name,
                Phone = Phone
            };

            _context.Farmers.Add(farmer);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Farmer"); // ✅ FIXED
        }
    }
}