using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IHoaDonRepository : IGenericRepository<HoaDon>
    {
        Task<IEnumerable<CTHoaDon>> GetAllById(int id);
    }
    public class HoaDonRepository:  GenericRepository<HoaDon>, IHoaDonRepository
    {
        public HoaDonRepository(DbContext context) : base(context) { }

        public async Task<IEnumerable<CTHoaDon>> GetAllById(int id)
        {
            var hoaDon = await DbSet.FirstOrDefaultAsync(h => h.MaHoaDon == id);
            if (hoaDon != null)
            {
                return hoaDon.CTHoaDon; // Assuming a navigation property 'CTHoaDon'
            }
            return Enumerable.Empty<CTHoaDon>();
        }
    }
}
