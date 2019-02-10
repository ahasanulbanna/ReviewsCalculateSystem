using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.Models.Models
{
    public class Reviewer
    {
        public int ReviewerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? NumberOfAmazonAccount { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
