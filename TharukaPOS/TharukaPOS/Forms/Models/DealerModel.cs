using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TharukaPOS.Forms.Models
{
    public class DealerModel
    {
        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public string DealerAddress { get; set; }
        public string DealerPhone { get; set; } // Consider changing to string if phone numbers can contain non-numeric chars

    }
}
