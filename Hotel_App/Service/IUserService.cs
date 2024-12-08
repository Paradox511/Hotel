using Hotel_App.Data;

namespace Hotel_App.Service
{
    public interface IUserService
    {
        Task<TaiKhoan> LoginAsync(TaiKhoan user);
        Task<TaiKhoan> RegisterUserAsync(TaiKhoan user);
        //public Task<TaiKhoan> RegisterUserAsync(TaiKhoan user);
        //public Task<TaiKhoan> GetUserByAccessTokenAsync(string accessToken);
        //public Task<TaiKhoan> RefreshTokenAsync(RefreshRequest refreshRequest);
    }
}