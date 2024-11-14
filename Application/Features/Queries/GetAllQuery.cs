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
    public class GetAllQuery<T>:IRequest <IEnumerable<T>> where T: class
    {
        public class GetAllQueryHandler<T>: IRequestHandler<GetAllQuery<T>,IEnumerable<T>> where T: class
        {
            private readonly IGenericRepository<T> _repository;

            public GetAllQueryHandler(IGenericRepository<T> repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<T>> Handle(GetAllQuery<T> query, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();
            }
        }
    }
}
