using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckJobsAPI.Models
{
    public class SpotModel
    {
        public Guid? ID { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public int? Minutes { get; set; }
        public bool IsPM { get; set; }
        public int? Days { get; set; }
        public double? LowSal { get; set; }
        public double? HighSal { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string PlaceTitle { get; set; }
        public string UserName { get; set; }
        public bool IsStared { get; set; }
        public bool IsDone { get; set; }
        public string ImagePath { get; set; }

    }
}
