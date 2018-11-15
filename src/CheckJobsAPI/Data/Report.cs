using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheckJobsAPI.Data
{
    public class Report
    {
        public Guid ID { get; set; }
        public string Body { get; set; }
        public Guid? UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
