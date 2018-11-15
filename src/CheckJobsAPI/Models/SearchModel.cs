using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckJobsAPI.Models
{
    public class SearchModel
    {
        public Guid? UserID { get; set; }
        public Guid? PlaceID { get; set; }
        public bool isAll { get; set; }
        public bool isStarred { get; set; }
    }
}
