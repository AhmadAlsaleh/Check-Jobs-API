using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheckJobsAPI.Data
{
    public class Spot
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public int? Minutes { get; set; }
        public bool IsPM { get; set; }
        public int? Days { get; set; }
        public double? LowSal { get; set; }
        public double? HighSal { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public bool IsDone { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Description { get; set; }
        public Guid? PlaceID { get; set; }
        [ForeignKey("PlaceID")]
        public Place Place { get; set; }

    }
}
