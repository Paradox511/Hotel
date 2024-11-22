using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Features.Commands
{
    public class UpdateCommand<T>: IRequestHandler<ICommand<T>,Unit> where T : class
    {
        public T Entity { get; }
        private readonly IGenericRepository<T> _repository;

        public UpdateCommand(IGenericRepository<T> repository, T entity)
        {
            _repository = repository;
            Entity = entity;
        }

        public async Task<Unit> Handle(ICommand<T> request, CancellationToken cancellationToken)
        {
            _repository.UpdateCommandAsync(request.Entity);
            return Unit.Value;
        }
    }
}
