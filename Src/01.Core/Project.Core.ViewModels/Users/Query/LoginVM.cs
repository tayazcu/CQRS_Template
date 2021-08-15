using Project.Core.Resources.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.ViewModels.Users.Query
{
    public class LoginVM
    {
        [Display(Name = SharedResource.GrantType)]
        public string grant_type { get; set; }

        [Display(Name = SharedResource.UserName)]
        public string username { get; set; }

        [Display(Name = SharedResource.Password)]
        public string password { get; set; }

        public string refresh_token { get; set; }
        public string scope { get; set; }

        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}
