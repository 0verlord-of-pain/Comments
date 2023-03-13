using System.Net;
using Comments.Core.Exceptions;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Comments.API.Infrastructure.Helpers;
internal static class ErrorHelper
{
    public static ErrorResponse CreateErrorResponse(Exception exception, bool includeStackTrace = false)
    {
        if (exception == null) return null;

        ErrorResponse errorResponse;

        switch (exception)
        {
            case ValidationException ex:
                errorResponse = new ErrorResponse(HttpStatusCode.BadRequest, "Validation Exception", ex.Message);
                break;
            case BadRequestException ex:
                errorResponse = new ErrorResponse(HttpStatusCode.BadRequest, "Bad Request Exception", ex.Message);
                break;
            case ForbidException ex:
                errorResponse = new ErrorResponse(HttpStatusCode.Forbidden, "ForbidException", ex.Message);
                break;
            case IdentityUserException ex:
                errorResponse = new ErrorResponse(HttpStatusCode.UnprocessableEntity, "Invalid User Exception",
                    ex.Message);
                break;
            case NotFoundException ex:
                errorResponse = new ErrorResponse(HttpStatusCode.NotFound, "Not Found Exception", ex.Message);
                break;
            case UserDeleteException ex:
                errorResponse = new ErrorResponse(HttpStatusCode.Gone, "User Delete Exception", ex.Message);
                break;

            default:
                return null;
        }

        if (includeStackTrace) errorResponse.SetStackTrace(exception.StackTrace);

        return errorResponse;
    }
}