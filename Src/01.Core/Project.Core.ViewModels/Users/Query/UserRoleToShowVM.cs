using Project.Core.Domain.Base;
using Project.Core.Resources.Resources;
using System.ComponentModel.DataAnnotations;

namespace Project.Core.ViewModels.Users.Query
{
    public class UserRoleToShowVM
    {
        /// <summary>
        /// نام نقش
        /// </summary>
        [Display(Name = SharedResource.Role)]
        public string Role { get; set; }

        /// <summary>
        /// وضعیت نقش
        /// </summary>
        [Display(Name = SharedResource.Status)]
        public TypeOfStatus Status { get; set; }
    }
}
