using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Application.Features.Commends._User.CreateUser
{
    public class CreateUserCommendRequest : IRequest<CreateUserCommendRespons>
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required(ErrorMessage = "User name ıs required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password ıs required")]
        public string? Password { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }

        public ICollection<string>? Roles { get; init; }

    }
}
