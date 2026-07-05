using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MilkCollectionSystem.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MilkCollectionSystem.Pages

{

    [Authorize(Roles = "Admin,Clerk")]
    public class MilkCollectionModel : PageModel
    {

        private readonly AppDbContext _context;


        public MilkCollectionModel(AppDbContext context)

        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Please select a farmer")]
        public int FarmerId { get; set; }

        [BindProperty]
        [Range(0.1, 1000, ErrorMessage = "Enter valid litres")]
        public double Litres { get; set; }

        public List<SelectListItem> FarmersList { get; set; }

        public async Task OnGetAsync()
        {
            FarmersList = await _context.Farmers
                .Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = f.Name
                })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                FarmersList = _context.Farmers
                    .Select(f => new SelectListItem
                    {
                        Value = f.Id.ToString(),
                        Text = f.Name
                    }).ToList();

                return Page();
            }

            var farmer = await _context.Farmers.FindAsync(FarmerId);

            double pricePerLitre = 50;
            double total = Litres * pricePerLitre;

            var record = new MilkRecord
            {
                FarmerId = FarmerId,
                Litres = Litres,
                Amount = total,
                CollectionDate = DateTime.Now
            };

            _context.MilkRecords.Add(record);
            await _context.SaveChangesAsync();

            return RedirectToPage("/MilkRecords");
        }
    }
}