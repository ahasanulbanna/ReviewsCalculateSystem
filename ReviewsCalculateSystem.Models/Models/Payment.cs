using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.Models.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int ReviewerId { get; set; }
        public virtual Reviewer Reviewer { get; set; }
        public int AdminId { get; set; }
        public virtual Admin Admin { get; set; }

    }
}
