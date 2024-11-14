using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BillsFeatures.Queries
{
    public class GetAllBillsQuery : IRequest<IEnumerable<HoaDon>> {
        public class GetAllBillsQueryHandler : IRequestHandler<GetAllBillsQuery, IEnumerable<HoaDon>>
    {
            private readonly IHotelDBContext _context;
            public GetAllBillsQueryHandler(IHotelDBContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<HoaDon>> Handle(GetAllBillsQuery query, CancellationToken cancellationToken)
            {
                var productList = await _context.hoadon.ToListAsync();
                if (productList == null)
                {
                    return null;
                }
                return productList.AsReadOnly();
            }
        }
    }
}