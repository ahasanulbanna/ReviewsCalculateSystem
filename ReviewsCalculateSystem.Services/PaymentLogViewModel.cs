using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.Services
{
    public class PaymentLogViewModel
    {
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public double TotalPaymentAmount { get; set; }
        public int AdminId { get; set; }
    }
}
