namespace Hotel_App.Service
{
	public class RoomService
	{
		HttpClient _httpClient;
		public RoomService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		//public async Task<IEnumerable<CTHoaDon>> GetCTHoaDonByMaHoaDon(int id)
		//{
		//    try
		//    {
		//        var baseUrl = "https://localhost:44359/api"; // Replace with your actual base URL
		//        var url = $"{baseUrl}/Bills/details/{id}";

		//        var response = await _httpClient.GetAsync(url);
		//        response.EnsureSuccessStatusCode();
		//        var data = await response.Content.ReadFromJsonAsync<IEnumerable<CTHoaDon>>();
		//        return data;
		//    }
		//    catch (Exception ex)
		//    {
		//        // Handle exceptions here, e.g., log the error, return an empty list, or throw a custom exception
		//        Console.WriteLine("Error fetching CTHoaDon data: " + ex.Message);
		//        return Enumerable.Empty<CTHoaDon>();
		//    }
		//}
	}
}
