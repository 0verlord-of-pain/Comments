using Comments.API.Infrastructure.Helpers;
using Microsoft.AspNetCore.Diagnostics;
namespace Comments.API.Infrastructure.Extensions;

public static class ApplicationBuilderExtension
{
    public static void UseErrorHandler(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var errorResponse =
                        ErrorHelper.CreateErrorResponse(contextFeature.Error, environment.IsDevelopment());

                    context.Response.StatusCode = (int)errorResponse.StatusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(errorResponse.ToString());
                }
            });
        });
    }
}