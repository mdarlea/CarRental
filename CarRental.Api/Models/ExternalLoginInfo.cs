using FluentValidation;
using System;

namespace CarRental.Api.Models
{
	public class ExternalLoginInfo
	{
		public string Provider { get; set; }
		public string AccessToken { get; set; }
		public int ExpiresIn { get; set; }
		public string ProviderKey { get; set; }
	}

	public class ExternalLoginInfoValidator: ExternalLoginInfoValidator<ExternalLoginInfo>
	{
	}

	public class ExternalLoginInfoValidator<T> : AbstractValidator<T> where T : ExternalLoginInfo {
		public ExternalLoginInfoValidator() {
			RuleFor(e => e.Provider).NotNull().NotEmpty();
			RuleFor(e => e.ProviderKey).NotNull().NotEmpty();
		}
	}
}
