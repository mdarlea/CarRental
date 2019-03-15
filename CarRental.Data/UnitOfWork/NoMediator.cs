namespace CarRental.Data.UnitOfWork
{
	using MediatR;	
	using System.Threading;
	using System.Threading.Tasks;

	public class NoMediator : IMediator
	{
		public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
		{
			return Task.CompletedTask;
		}

		public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Task.CompletedTask;
		}

		public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Task.FromResult<TResponse>(default(TResponse));
		}

		public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Task.CompletedTask;
		}
	}
}
