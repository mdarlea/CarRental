﻿namespace CarRental.Api.Infrastructure.Services
{
	using CarRental.ReadModel;
	using System;
	using System.Collections.Generic;	
	using System.Threading.Tasks;

	public class AvailableCarService : IAvailableCarService
	{
		private readonly IAvailableCarRepository availableCarRepository;

		public AvailableCarService(IAvailableCarRepository availableCarRepository) {
			this.availableCarRepository = availableCarRepository ?? throw new ArgumentNullException(nameof(availableCarRepository));
		}

		public async Task<IList<AvailableCarReadModel>> FindAvailableCarsAsync(DateTime from, DateTime to)
		{
			return await availableCarRepository.GetAvailableCarsAsync(from, to);			
		}
	}
}
