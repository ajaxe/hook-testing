using ApogeeDev.WebhookTester.Common.ViewModels;

namespace ApogeeDev.WebhookTester.Abstractions;
public interface IUserInfoProvider
{
    UserProfile GetUserProfile();
}