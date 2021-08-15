using Project.Core.Resources.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.ViewModels.Users.Command
{
    public class RemoveClaimFromUserVM
    {
        /// <summary>
        /// ایدی کاربر
        /// </summary>
        [Display(Name = SharedResource.UserId)]
        public string UserId { get; set; }

        /// <summary>
        /// ادعا 
        /// </summary>
        [Display(Name = SharedResource.Claim)]
        public string Claim { get; set; }
    }
}
