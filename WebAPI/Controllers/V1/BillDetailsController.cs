using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class BillDetailsController : BaseApiController
    {
        private readonly IHotelDBContext _context;

        public BillDetailsController(IHotelDBContext context)
        {
            _context = context;
        }
    //    [HttpGet("GetBillDetails/{id}")]
    }
}
