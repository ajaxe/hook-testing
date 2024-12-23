﻿using ApogeeDev.WebhookTester.Abstractions;
using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.Models;
using ApogeeDev.WebhookTester.Common.ViewModels;
using Htmx;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApogeeDev.WebhookTester.Pages.Session;

public class IndexModel : PageModel
{
    private readonly IWebhookSessionService _sessionService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IWebhookSessionService sessionService, ILogger<IndexModel> logger)
    {
        _sessionService = sessionService;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public WebhookSessionView? NewSession { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        if (Id == default)
        {
            NewSession = await _sessionService.GetWebhookSession();
            return RedirectToPage("/Session/Index", new { id = NewSession?.WebhookSessionId });
        }
        else
        {
            NewSession = await _sessionService.GetWebhookSession(Id);
        }

        return Page();
    }

    public async Task<IActionResult> OnGetCallbackAsync(Guid callbackId)
    {
        if (Request.IsHtmx())
        {
            CallbackRequestModel? callback = await _sessionService.GetCallback(callbackId);

            if (callback is null)
            {
                return Partial("_Templates/_CallbackNotFound");
            }
            return Partial("_Templates/_CallbackRequestDetail", callback);
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostSaveUserSessionAsync(bool save)
    {
        IActionResult? result;
        if (Request.IsHtmx())
        {
            if (save)
            {
                await _sessionService.AssignWebhookSessionToCurrentUser(Id);
            }
            else
            {
                await _sessionService.RemoveWebhookSessionToCurrentUser(Id);
            }
            result = Partial("_Templates/_SessionSaveStatus", save);
        }
        else
        {
            result = RedirectToPage();
        }
        return result;
    }
}
