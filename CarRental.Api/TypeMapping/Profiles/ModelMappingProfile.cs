namespace CarRental.Api.TypeMapping.Profiles
{
	using AutoMapper;
	using CarRental.Api.Models;
	using CarRental.Identity.Data.Entities;
	using CarRental.Identity.Data.ValueObjects;

	public class ModelMappingProfile : Profile
	{
		public ModelMappingProfile() {
			CreateMap<UserModel, ApplicationUser>()
					.ForMember(au => au.Name, opt => opt.MapFrom<ApplicationUserNameResolver>())
					.ForMember(au => au.UserName, opt => opt.MapFrom(u => u.Email));
			CreateMap<AddressModel, Address>();
			CreateMap<CreditCardModel, CreditCard>()
				.ForMember(cc => cc.BillingAddressId, opt => opt.Ignore())
				.ForMember(cc => cc.BillingAddress, opt => opt.Ignore());
			CreateMap<DriverLicenseModel, DriverLicense>();
		}
	}

	public class ApplicationUserNameResolver : IValueResolver<UserModel, ApplicationUser, Name>
	{
		public Name Resolve(UserModel source, ApplicationUser destination, Name destMember, ResolutionContext context)
		{
			return new Name(source.FirstName, source.LastName);
		}
	}
}
