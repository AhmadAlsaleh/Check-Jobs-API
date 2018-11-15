using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheckJobsAPI.Data
{
    public class Place
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid? UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [InverseProperty("Place")]
        public ICollection<Spot> Spots { get; set; }
    }
}
