using System;
using Microsoft.AspNetCore.Mvc;

namespace TelephoneDirectory.Contracts.Exceptions;


	public abstract class CommonExceptionBase : Exception
	{
		public string ErrorType { get; set; }

		protected CommonExceptionBase()
		{
		}

		protected CommonExceptionBase(string message)
			: base(message)
		{
		}

		protected CommonExceptionBase(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public abstract ProblemDetails GetProblemDetails();
	}
