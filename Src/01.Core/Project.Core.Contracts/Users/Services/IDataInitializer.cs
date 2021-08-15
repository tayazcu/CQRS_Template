using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Contracts.Users.Services
{
    public interface IDataInitializer
    {
        int index { get; set; }
        void InitializeData();
    }
}
