using FluentValidation;
using System;

namespace CarRental.Api.Models
{
	public class ExternalLoginInfoModel
	{
		public string Provider { get; set; }
		public string AccessToken { get; set; }
		public int ExpiresIn { get; set; }
		public string ProviderKey { get; set; }
	}

	public class ExternalLoginInfoValidator: ExternalLoginInfoValidator<ExternalLoginInfoModel>
	{
	}

	public class ExternalLoginInfoValidator<T> : AbstractValidator<T> where T : ExternalLoginInfoModel {
		public ExternalLoginInfoValidator() {
			RuleFor(e => e.Provider).NotNull().NotEmpty();
			RuleFor(e => e.ProviderKey).NotNull().NotEmpty();
		}
	}
}
