namespace CarRental.Application.Services
{
	using CarRental.Domain.Aggregates.AvailableCarAgg;
	using Dto = ReadModel;
	using Swaksoft.Application.Seedwork.Services;
	using System;	
	using System.Threading.Tasks;
	using Swaksoft.Core.Dto;
	using System.Collections.Generic;
	using Swaksoft.Infrastructure.Crosscutting.Extensions;

	public class AvailableCarAppService : AppServiceBase<AvailableCarAppService>, IAvailableCarAppService
	{
		private readonly IAvailableCarRepository availableCarRepository;

		public AvailableCarAppService(IAvailableCarRepository availableCarRepository) {
			this.availableCarRepository = availableCarRepository ?? throw new ArgumentNullException(nameof(availableCarRepository));
		}

		public async Task<Dto.AvailableCarResult> FindAvailableCarById(int id)
		{
			if (id <= 0) throw new ArgumentException("Available car id must be greater than 0", nameof(id));
			var result = await availableCarRepository.GetAvailableCar(id);
			if (result == null)
			{
				return new Dto.AvailableCarResult
				{
					Status = ActionResultCode.Failed,
					Message = $"Cannot find an available car with the {id} id",
					Errors = new List<string> { $"Cannot find an available car with the {id} id" }
				};
			}
			return result.ProjectedAs<Dto.AvailableCarResult>();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!disposing) return;

			availableCarRepository.Dispose();
		}
	}
}
