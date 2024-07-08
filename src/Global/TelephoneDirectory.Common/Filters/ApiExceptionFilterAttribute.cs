using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TelephoneDirectory.Contracts.Exceptions;

namespace TelephoneDirectory.Common.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
	private readonly ILogger<ApiExceptionFilterAttribute> _logger;

	public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
	{
		_logger = logger;
	}

	public override Task OnExceptionAsync(ExceptionContext context)
	{
		if (context.Exception is CommonExceptionBase commonException)
		{
			_logger.LogWarning(commonException, commonException.Message);

			var problemDetails = commonException.GetProblemDetails();

			context.Result = problemDetails.Status switch
			{
				(int)HttpStatusCode.NotFound => new NotFoundObjectResult(problemDetails),
				(int)HttpStatusCode.Conflict => new ConflictObjectResult(problemDetails),
				_ => new BadRequestObjectResult(problemDetails)
			};

			context.ExceptionHandled = true;

			return Task.CompletedTask;
		}

		HandleUnknownException(context);

		_logger.LogError(context.Exception, context.Exception.Message);

		return Task.CompletedTask;
	}

	private void HandleUnknownException(ExceptionContext context)
	{
		var details = new ProblemDetails
		{
			Type = "UnknownError",
			Title = nameof(HttpStatusCode.InternalServerError),
			Status = (int)HttpStatusCode.InternalServerError,
			Detail = context.Exception.Message
		};

		context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status500InternalServerError };

		context.ExceptionHandled = true;
	}
}