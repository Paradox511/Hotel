using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Queries
{
    public class GetByIDQuery<T> : IRequest<T> where T : class
    {
        public int Id { get; set; }
    }

    public class GetByIDQueryHandler<T> : IRequestHandler<GetByIDQuery<T>, T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GetByIDQueryHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> Handle(GetByIDQuery<T> request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
