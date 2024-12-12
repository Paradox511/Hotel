using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Interfaces
{
	public interface IRoomRepository : IGenericRepository<Phong>
	{
		Task<IEnumerable<Phong>> GetPhongsByLoaiPhongIdAsync(int loaiPhongId);
	}
	
	public class RoomRepository: GenericRepository<Phong>, IRoomRepository
	{
		public RoomRepository(DbContext context) : base(context) { }
		// Lấy danh sách phòng dựa trên MaLoaiPhong
		public async Task<IEnumerable<Phong>> GetPhongsByLoaiPhongIdAsync(int loaiPhongId)
		{
			return await DbSet
				.Include(p => p.LoaiPhong) // Điều hướng tới LoaiPhong nếu cần
				.Where(p => p.MaLoaiPhong == loaiPhongId)
				.ToListAsync();
		}
	}
}
