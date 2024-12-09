using Hotel_App.Data;

namespace Hotel_App.Service
{
    public interface IDichVuService
    {
        Task<IEnumerable<CTHoaDon>> GetCTHoaDonByMaDichVu(int id);
    }
}
