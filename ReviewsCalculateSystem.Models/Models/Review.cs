using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.Models.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string SwapmeetFbProfileLink { get; set; }
        public string SwapmeetProductLink { get; set; }
        public string SwapmeetReviewLink { get; set; }
        public string OwnReviewLink { get; set; }
        public bool ReviewStatus { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int ReviewerId { get; set; }
        public virtual Reviewer Reviewer { get; set; }
    }
}
