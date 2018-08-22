using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class UserWeight
    {
        public int UserId { get; set; }
        public DateTime DateOfEntry { get; set; }
        public int TodaysWeight { get; set; }
    }
}
