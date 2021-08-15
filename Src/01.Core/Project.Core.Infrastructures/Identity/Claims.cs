using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Infrastructures.Identity
{
    public enum Claims : byte
    {
        [Description("System")]
        [Display(Name = "اخبار" )]
        News = 1,

        [Description("System")]
        [Display(Name = "اطلاعیه ها")]
        Notification = 2
    }
}
