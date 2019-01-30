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
        public string ProductAsin { get; set; }
        public string ProductLink { get; set; }
        public bool CurrentStatus { get; set; }
        public int NumberOfReviewNeed { get; set; }
        public int? NumberOfReviewCollect { get; set; }
        public DateTime? ReviewStartDate { get; set; }
        public DateTime? ReviewEndDate { get; set; }
        public int AdminId { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
