namespace CarRental.Api.TypeMapping.Profiles
{
	using AutoMapper;
	using CarRental.Api.Models;
	using CarRental.Domain.Aggregates.AvailableCarAgg;
	using CarRental.Domain.Aggregates.CarAgg;
	using CarRental.Identity.Data.Entities;
	using CarRental.Identity.Data.ValueObjects;
	using Swaksoft.Application.Seedwork.TypeMapping;
	using Dto = ReadModel;

	public class ModelMappingProfile : AutoMapperProfile
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
			CreateMap<Car, Dto.Car>()
				.ForMember(c => c.Type, opt => opt.MapFrom(c => c.Type.Type));
			CreateActionResultMap<AvailableCar, Dto.AvailableCarResult>();
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
