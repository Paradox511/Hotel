using Hotel_App.Data;

namespace Hotel_App.Service
{
    public interface IHoaDonService
    {
        Task<IEnumerable<CTHoaDon>> GetCTHoaDonByMaHoaDon(int id);
        Task UpdateTotal(int id, decimal newTotal);

    }
}
