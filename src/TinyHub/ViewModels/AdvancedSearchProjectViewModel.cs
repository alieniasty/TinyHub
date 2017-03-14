using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyHub.Models;

namespace TinyHub.ViewModels
{
    public class AdvancedSearchProjectViewModel 
    {
        public string ProjectName { get; set; }

        public string FromUser { get; set; }

        public DateTime? DateCreatedFrom { get; set; }

        public DateTime? DateCreatedTo { get; set; }

        public LicenseType? License { get; set; }
    }
}
