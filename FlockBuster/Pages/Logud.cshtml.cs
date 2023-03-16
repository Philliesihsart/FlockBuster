using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlockBuster.Pages
{
    public class Logud : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("Email");
            return RedirectToPage("/Login");
            
        }
    }
}
