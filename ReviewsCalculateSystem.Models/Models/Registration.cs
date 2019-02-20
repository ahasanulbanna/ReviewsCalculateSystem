using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.Models.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ActivationKey { get; set; }
        public bool IsActive { get; set; }
    }
}
