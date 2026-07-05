using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MilkCollectionSystem.Pages
{
    [Authorize(Roles = "Admin,Clerk")]
    public class EditMilkModel : PageModel
    {
        [BindProperty]
        public MilkRecord Record { get; set; }

        public int Id { get; set; }

        public void OnGet(int id)
        {
            Id = id;
            Record = DataStore.Records[id];
        }

        public IActionResult OnPost(int id)
        {
            DataStore.Records[id] = Record;
            return RedirectToPage("/MilkRecords");
        }
    }
}