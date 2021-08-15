using Project.Core.Resources.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.ViewModels.Users.Command
{
    public class UserClaimToAddVM
    {
        /// <summary>
        /// ایدی کاربر
        /// </summary>
        [Display(Name = SharedResource.UserId)]
        public string UserId { get; set; }

        /// <summary>
        /// مقدار ادعا
        /// </summary>
        [Display(Name = SharedResource.ClaimValue)]
        public string ClaimValue { get; set; }
    }
}
