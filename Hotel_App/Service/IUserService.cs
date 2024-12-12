using Hotel_App.Data;

namespace Hotel_App.Service
{
    public interface IUserService
    {
        Task<TaiKhoan> LoginAsync(String users, String pass);
        
    }
}