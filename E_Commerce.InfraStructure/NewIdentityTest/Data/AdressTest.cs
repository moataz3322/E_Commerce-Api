using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.InfraStructure.Identity.Entities;

namespace E_Commerce.InfraStructure.NewIdentityTest.Data
{
    class AdressTest
    {

        public int Id { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;


        public ApplicationUserTest User { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}
