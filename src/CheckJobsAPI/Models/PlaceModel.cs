using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckJobsAPI.Models
{
    public class PlaceModel
    {
        public Guid? id { get; set; }
        public Guid ID { get; set; }
        public string Title { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public bool IsActive { get; set; }
        public Guid? UserID { get; set; }
        public string UserName { get; set; }

    }
}
