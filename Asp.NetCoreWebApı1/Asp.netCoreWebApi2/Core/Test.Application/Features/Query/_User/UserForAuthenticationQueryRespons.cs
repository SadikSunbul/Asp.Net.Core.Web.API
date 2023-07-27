using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Dto;

namespace Test.Application.Features.Query._User
{
    public class UserForAuthenticationQueryRespons
    {
        public bool Success { get; set; }
        public TokenDto Token { get; set; }
    }
}
