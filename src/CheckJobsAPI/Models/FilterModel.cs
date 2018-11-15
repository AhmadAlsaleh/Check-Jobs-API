using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckJobsAPI.Models
{
    public class FilterModel
    {
        public string Title { get; set; }
        public double? LowSal { get; set; }
        public double? HighSal { get; set; }
        public string Gender { get; set; }
        public int? Minutes { get; set; }
        public int? days { get; set; }
        public bool? IsPM { get; set; }
    }
}
