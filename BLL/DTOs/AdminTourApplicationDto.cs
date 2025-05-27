using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AdminTourApplicationDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string UserFullName { get; set; } = null!; 
        public int TourId { get; set; }
        public string TourName { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = null!;
        public bool HasVoucher { get; set; } 
    }
}
