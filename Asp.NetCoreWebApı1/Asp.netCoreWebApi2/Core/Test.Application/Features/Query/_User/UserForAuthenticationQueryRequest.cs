using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Application.Features.Query._User
{
    public class UserForAuthenticationQueryRequest:IRequest<UserForAuthenticationQueryRespons>
    {
        [Required(ErrorMessage ="user name is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "password is required")]
        public string? Password { get; init; }
    }
}
