using Blazored.LocalStorage;
using Hotel_App.Data;
using Hotel_App.Service;

namespace Hotel_App
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddHttpClient<HotelService<HoaDon>>();
			services.AddHttpClient<HotelService<NhanVien>>();
			services.AddHttpClient<HotelService<PhuongThucThanhToan>>();
			services.AddHttpClient<HotelService<DatPhong>>();
			services.AddHttpClient<RoomService<LoaiPhong>>();
			services.AddHttpClient<RoomService<Phong>>();

			services.AddHttpClient<HoaDonService>();
			services.AddSingleton<WeatherForecastService>();



			var appSettingSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingSection);

			services.AddBlazoredLocalStorage();

			services.AddSingleton<HttpClient>();

			services.AddAuthorization(options =>
			{
				options.AddPolicy("SeniorEmployee", policy =>
					policy.RequireClaim("IsUserEmployedBefore1990", "true"));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{

				endpoints.MapFallbackToPage("/_Host");
				endpoints.MapBlazorHub();
			});
		}
	}
}
