using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Domain.Base
{
    public enum TypeOfStatus
    {
        [Display(Name = "فعال")]
        Active = 1,

        [Display(Name = "غیر فعال")]
        Inactive = 2,

        [Display(Name = "در حال بررسی")]
        Pending = 3
    }
}
