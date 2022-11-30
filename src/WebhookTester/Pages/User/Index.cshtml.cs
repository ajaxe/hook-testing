using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApogeeDev.WebhookTester.Pages.User;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }
}