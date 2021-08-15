using Project.Core.Domain.Base;
using Project.Core.Resources.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.ViewModels.Users.Command
{
    public class UserRoleStatusToEditVM
    {
        /// <summary>
        /// ایدی کاربر
        /// </summary>
        [Display(Name = SharedResource.UserId)]
        public string UserId { get; set; }

        /// <summary>
        /// نقش
        /// </summary>
        [Display(Name = SharedResource.UserId)]
        public string RoleName { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        [Display(Name = SharedResource.UserId)]
        public TypeOfStatus Status { get; set; }
    }
}
