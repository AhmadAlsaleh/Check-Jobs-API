using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckJobsAPI.Models
{
    public class EditableUserInfo
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
    }
}
