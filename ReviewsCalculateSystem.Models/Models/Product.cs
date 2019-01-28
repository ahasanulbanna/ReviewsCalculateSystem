using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.Models.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public bool CurrentStatus { get; set; }
        public int NumberOfReviewNeed { get; set; }
        public int? NumberOfReviewCollect { get; set; }
        public DateTime? ReviewStartDate { get; set; }
        public DateTime? ReviewEndDate { get; set; }
        public virtual ICollection<ReviewProduct> ReviewProducts { get; set; }

    }
}
