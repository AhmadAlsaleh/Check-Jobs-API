using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckJobsAPI.Data
{
    public class User
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public string Details { get; set; }
        public string FaceBookID { get; set; }
        public string Email { get; set; }
        public bool IsPro { get; set; }
        [InverseProperty("User")]
        public ICollection<Place> Places { get; set; }
    }
}
