using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.InfraStructure.NewIdentityTest.Data
{
    class ApplicationUserTest:IdentityUser
    {

        public string DisplayName { get; set; } = default!;
        public AdressTest? AdressTest { get; set; } = default!;
    }
}
