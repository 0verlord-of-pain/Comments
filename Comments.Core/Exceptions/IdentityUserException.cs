﻿namespace Comments.Core.Exceptions;

public class IdentityUserException : Exception
{
    public IdentityUserException(string message) : base(message)
    {
    }
}