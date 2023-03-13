using Ganss.Xss;

namespace Comments.API.Infrastructure.Helpers;
public static class XCCSanitizeHelper
{
    public static string SanitizeText(string text)
    {
        var sanitizer = new HtmlSanitizer();
        sanitizer.AllowedTags.Clear();
        sanitizer.AllowedAttributes.Clear();
        sanitizer.AllowedTags.Add("a");
        sanitizer.AllowedTags.Add("code");
        sanitizer.AllowedTags.Add("i");
        sanitizer.AllowedTags.Add("strong");
        sanitizer.AllowedAttributes.Add("href");
        sanitizer.AllowedAttributes.Add("title");
        var sanitized = sanitizer.Sanitize(text);
        return sanitized;
    }
}