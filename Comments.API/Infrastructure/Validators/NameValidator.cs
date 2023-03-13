using System.Text.RegularExpressions;

namespace Comments.API.Infrastructure.Validators
{
    public struct NameValidator
    {
        public static bool IsValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            var regex = new Regex(@"^([a-zA-Z]+[ '-]?)+$");
            return regex.IsMatch(name);
        }
    }
}