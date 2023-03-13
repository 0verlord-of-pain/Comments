namespace Comments.Core.Exceptions;

public class UserDeleteException : Exception
{
    public UserDeleteException(string message) : base(message)
    {
    }
}