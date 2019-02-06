using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.Models.Models
{
    public class ReviewerTaskAsign
    {
        public int ReviewerTaskAsignId { get; set; }
        public int? NumberOfReviewCollect { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int ReviewerId { get; set; }
        public virtual Reviewer Reviewer { get; set; }
        public double PerReviewCost { get; set; }
        public int AdminId { get; set; }
        public virtual Admin Admin { get; set; }
        public bool isComplete { get; set; }
    }
}
