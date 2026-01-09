using Microsoft.Extensions.Localization;

namespace Movement.Api.Resources;

public class I18nService(IStringLocalizer<Errors> errors, IStringLocalizer<Messages> messages)
{
    public string GetError(string key) => errors[key];
    public string GetMessage(string key) => messages[key];
}