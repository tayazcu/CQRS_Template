using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Infrastructures.Identity
{
    public enum Roles : byte
    {
        [Display(Name = "مدیر")]
        Admin = 1,

        [Display(Name = "کاربر")]
        User = 2
    }
}
