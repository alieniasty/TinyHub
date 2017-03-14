using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyHub.Models
{
    public enum LicenseType
    {
        MitLicense = 0,
        Gnugpl = 1,
        MozillaPublicLicense2 = 2,
        ApacheLicense = 3,
        Unlicense = 4
    }
}
