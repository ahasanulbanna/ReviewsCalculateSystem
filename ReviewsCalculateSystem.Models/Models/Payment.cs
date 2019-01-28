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
        public double PayAmount { get; set; }
        public double DueAmount { get; set; }
        public int ReviewerId { get; set; }
        public virtual Reviewer Reviewer { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int AdminId { get; set; }
        public virtual Admin Admin { get; set; }

    }
}
