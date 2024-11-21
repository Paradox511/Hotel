using Application.Interfaces;
using MediatR;

public class DeleteCommand<T> : IRequest<Unit> where T : class
{
    public int Id { get; set; }
}

public class DeleteCommandHandler<T> : IRequestHandler<DeleteCommand<T>, Unit> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public DeleteCommandHandler(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteCommand<T> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity != null)
        {
            await _repository.DeleteCommandAsync(entity); 
        }

        return Unit.Value;
    }
}