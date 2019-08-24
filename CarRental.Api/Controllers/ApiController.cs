namespace CarRental.Api
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Swaksoft.Core.Dto;
	using System;
	using System.Linq;

	public abstract class ApiController : ControllerBase
	{
		protected IActionResult GetErrorResult(IdentityResult result)
		{
			//test
			if (result == null)
			{
				throw new ArgumentNullException(nameof(result));
			}

			if (result.Succeeded) return null;
			if (result.Errors != null)
			{
				foreach (var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
			}

			if (ModelState.IsValid)
			{
				// No ModelState errors are available to send, so just return an empty BadRequest.
				return BadRequest();
			}

			return new BadRequestObjectResult(ModelState);
		}

		protected IActionResult GetErrorResult(Swaksoft.Core.Dto.ActionResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException(nameof(result));
			}

			if (result.Status == ActionResultCode.Success) return null;
			if (result.Errors != null)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error);
				}
			}

			if (ModelState.IsValid)
			{
				// No ModelState errors are available to send, so just return an empty BadRequest.
				return BadRequest();
			}

			return new BadRequestObjectResult(ModelState);
		}

		public string GetUserId() {
			return User.Claims.Where(c => c.Type == "id").Select(c => c.Value).SingleOrDefault();
		}
	}
}
