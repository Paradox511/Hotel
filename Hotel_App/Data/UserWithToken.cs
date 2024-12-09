using Microsoft.AspNetCore.Identity;

namespace Hotel_App.Data
{
    public class UserWithToken
    {
        public int MaTaiKhoan { get; set; } // Hoặc thuộc tính tương đương
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

       
    }
}
