using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.Models.Models
{
    public class ReviewProduct
    {
        public int ReviewProductId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int ReviewId { get; set; }
        public virtual Review Review { get; set; }
    }
}
