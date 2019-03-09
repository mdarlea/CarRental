namespace CarRental.Api.Authentication
{
	using Newtonsoft.Json;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;

	public static class Tokens
	{
		public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
		{
			var response = new
			{
				id = identity.Claims.Single(c => c.Type == "id").Value,
				userName,
				token = await jwtFactory.GenerateEncodedToken(userName, identity),
				expires = (int)jwtOptions.ValidFor.TotalSeconds
			};

			return JsonConvert.SerializeObject(response, serializerSettings);
		}
	}
}
