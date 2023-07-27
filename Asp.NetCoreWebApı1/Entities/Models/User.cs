using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User: 
    {
        //IdentityUser da olmayan kendı projemzıde ıstedıgımız prop ları buraya yazabılırız
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
}
