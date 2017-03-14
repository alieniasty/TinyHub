using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyHub.Models
{
    public class Bug
    {
        public int Id { get; set; }

        [Required]
        public PriorityType Priority { get; set; }

        [Required]
        public string FromProject { get; set; }

        public string UserCalling { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
