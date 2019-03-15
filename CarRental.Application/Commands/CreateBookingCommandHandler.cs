namespace CarRental.Application.Commands
{
	using CarRental.Domain.Aggregates.AvailableCarAgg;
	using CarRental.Domain.Aggregates.BookingAgg;
	using CarRental.Domain.Aggregates.UserAgg;
	using MediatR;
	using Swaksoft.Core.Dto;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Threading;
	using System.Threading.Tasks;

	public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, ActionResult>
	{
		private readonly IUserRepository userRepository;
		private readonly IAvailableCarRepository availableCarRepository;
		private readonly IBookingRepository bookingRepository;

		public CreateBookingCommandHandler(
			IUserRepository userRepository, 
			IAvailableCarRepository availableCarRepository, 
			IBookingRepository bookingRepository) {
			this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
			this.availableCarRepository = availableCarRepository ?? throw new ArgumentNullException(nameof(availableCarRepository));
			this.bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
		}

		public async Task<ActionResult> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			// gets the available car
			var availableCar = availableCarRepository.Get(request.AvailableCarId);
			if (availableCar == null) {
				var result = new ActionResult
				{
					Status = ActionResultCode.Failed,
					Message = $"Cannot find an available car with the {request.AvailableCarId} id",
					Errors = new List<string> { $"Cannot find an available car with the {request.AvailableCarId} id" }
				};
				return await Task.FromResult(result);
			}

			// gets the user
			var user = userRepository.GetUser(request.UserId);
			if (user == null)
			{
				var result = new ActionResult
				{
					Status = ActionResultCode.Failed,
					Message = $"Cannot find an user with the {request.UserId} id",
					Errors = new List<string> { $"Cannot find an user with the {request.UserId} id" }
				};
				return await Task.FromResult(result);
			}

			// creates the booking
			var booking = BookingFactory.CreateBooking(availableCar, user, request.From, request.To);

			// validates the booking
			var validationContext = new ValidationContext(booking);
			Validator.ValidateObject(booking, validationContext, validateAllProperties: true);

			bookingRepository.Add(booking);

			// saves changes
			await bookingRepository.UnitOfWork.SaveChangesAsync();

			var res = new ActionResult
			{
				Status = ActionResultCode.Success
			};

			return await Task.FromResult(res);
		}
	}
}
