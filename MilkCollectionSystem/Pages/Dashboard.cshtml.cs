using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MilkCollectionSystem.Data;


namespace MilkCollectionSystem.Pages
{
    [Authorize(Roles = "Admin,Clerk")]
    public class DashboardModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardModel(
            AppDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public int TotalDeliveries { get; set; }
        public double TotalLitres { get; set; }
        public double TotalPayments { get; set; }
        public int TotalFarmers { get; set; }

        public List<string> Labels { get; set; } = new();
        public List<double> LitresData { get; set; } = new();

        // Display current user's role
        public string UserRole { get; set; } = "";

        public async Task OnGetAsync()
        {
            // Get logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UserRole = string.Join(", ", roles);
            }

            // Dashboard statistics
            var records = await _context.MilkRecords
                .Include(r => r.Farmer)
                .ToListAsync();

            TotalDeliveries = records.Count;
            TotalLitres = records.Sum(r => r.Litres);
            TotalPayments = records.Sum(r => r.Amount);
            TotalFarmers = await _context.Farmers.CountAsync();

            var farmerTotals = records
                .Where(r => r.Farmer != null)
                .GroupBy(r => r.Farmer!.Name)
                .ToList();

            Labels = farmerTotals.Select(g => g.Key).ToList();
            LitresData = farmerTotals.Select(g => g.Sum(r => r.Litres)).ToList();
        }
    }
}