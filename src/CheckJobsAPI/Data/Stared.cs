using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheckJobsAPI.Data
{
    public class Stared
    {
        public Guid ID { get; set; }
        public Guid? UserID { get; set; }
        public Guid? SpotID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("SpotID")]
        public Spot Spot { get; set; }
    }
}
