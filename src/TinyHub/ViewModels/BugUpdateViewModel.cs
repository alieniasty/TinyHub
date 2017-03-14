using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyHub.Models;

namespace TinyHub.ViewModels
{
    public class BugUpdateViewModel
    {
        [Required]
        public string FromProject { get; set; }

        [Required]
        public string UserCalling { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string NewDescription { get; set; }
    }
}
