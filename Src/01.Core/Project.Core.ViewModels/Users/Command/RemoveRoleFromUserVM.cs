using Project.Core.Resources.Resources;
using System.ComponentModel.DataAnnotations;

namespace Project.Core.ViewModels.Users.Command
{
    public class RemoveRoleFromUserVM
    {
        /// <summary>
        /// ایدی کاربر
        /// </summary>
        [Display(Name = SharedResource.UserId)]
        public string UserId { get; set; }

        /// <summary>
        ///  نقش
        /// </summary>
        [Display(Name = SharedResource.Role)]
        public string RoleName { get; set; }
    }
}
