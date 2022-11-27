using System.Text;
using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApogeeDev.WebhookTester.Pages.Callback;

[IgnoreAntiforgeryToken(Order = 1001)]
public class IndexModel : PageModel
{
    private readonly ICallbackProcessor callbackProcessor;
    public IndexModel(ICallbackProcessor callbackProcessor)
    {
        this.callbackProcessor = callbackProcessor;
    }

    [BindProperty]
    public Guid Id { get; set; }

    public IActionResult OnGet(Guid id)
    {
        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        if (Id != default)
        {
            await callbackProcessor.Handle(await CreateCommand());
        }

        return new OkObjectResult(null);
    }

    private async Task<CallbackReceived> CreateCommand()
    {
        using StreamReader reader = new StreamReader(Request.Body,
            Encoding.UTF8);
        return new CallbackReceived
        {
            WebhookSessionId = Id,
            Headers = this.Request.Headers
                .ToDictionary(h => h.Key,
                elem => elem.Value.ToList()),
            RequestBody = await reader.ReadToEndAsync()
        };
    }
}
