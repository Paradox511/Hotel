using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands
{
    public class CreateCommand<T>: IRequest<Unit> where T : class
    {

        public T Entity { get; set; }

        private readonly IGenericRepository<T> _repository;

        public CreateCommand(IGenericRepository<T> repository, T entity)
        {
            _repository = repository;
            Entity = entity;
        }
        public CreateCommand() { } // Add a parameterless constructor

        public CreateCommand(T entity)
        {
            Entity = entity;
        }
        public class CreateCommandHandler<T> : IRequestHandler<CreateCommand<T>, Unit> where T : class
        {
            private readonly IGenericRepository<T> _repository;

            public CreateCommandHandler(IGenericRepository<T> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(CreateCommand<T> request, CancellationToken cancellationToken)
            {

                await _repository.CreateCommandAsync(request.Entity);
                return Unit.Value;
            }
        }
       
    }
}
