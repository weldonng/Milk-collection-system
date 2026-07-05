using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkCollectionSystem.Data;

namespace MilkCollectionSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public int TotalFarmers { get; set; }
        public int TotalDeliveries { get; set; }
        public double TotalLitres { get; set; }

        public void OnGet()
        {
            TotalFarmers = _context.Farmers.Count();
            TotalDeliveries = _context.MilkRecords.Count();
            TotalLitres = _context.MilkRecords.Sum(x => x.Litres);
        }
    }
}