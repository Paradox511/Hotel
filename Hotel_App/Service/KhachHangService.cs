using Hotel_App.Data;
using System.Net.Http;
namespace Hotel_App.Service
{
    public class KhachHangService 
    {
        HttpClient _httpClient;
        public KhachHangService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

    }
}