using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain.Entites
{
    public class User:IdentityUser
    {
        //IdentityUser da olmayan kendı projemzıde ıstedıgımız prop ları buraya yazabılırız
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTie { get; set; }


    }
}
