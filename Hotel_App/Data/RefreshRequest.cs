using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_App.Data
{
    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
