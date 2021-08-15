using System.ComponentModel.DataAnnotations;

namespace Project.Core.Domain.Base
{
    public enum TypeOfUser
    {
        [Display(Name = "مشتری")]
        Customer = 1,

        [Display(Name = "اپراتور")]
        Operator = 2,

        [Display(Name = "کانتر همکار")]
        CounterColleague = 3,

        [Display(Name = "مدیر")]
        Admin = 4,
    }
}
