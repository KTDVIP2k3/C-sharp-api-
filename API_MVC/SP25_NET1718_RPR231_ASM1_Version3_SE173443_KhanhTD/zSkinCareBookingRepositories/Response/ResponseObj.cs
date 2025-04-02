using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingRepositories_.Response
{
    public class ResponseObj
    {
        public string Message { get; set; }

        public List<Schedule> Data { get; set; }

        public string Status { get; set; }
    }
}
