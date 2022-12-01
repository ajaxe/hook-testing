using System.Runtime.Serialization;
using ApogeeDev.WebhookTester.Common.ViewModels;

namespace ApogeeDev.WebhookTester.Common.Exceptions;

public class UiException : Exception
{
    public UiException()
    {
    }

    public UiException(string? message) : base(message)
    {
        SetMessage(message);
    }

    public UiException(string? message, Exception? innerException) : base(message, innerException)
    {
        SetMessage(message);
    }

    private void SetMessage(string? message)
    {
        Error = new ErrorViewModel
        {
            ErrorMessage = message ?? "Something went wrong."
        };
    }

    public ErrorViewModel Error { get; private set; }
}