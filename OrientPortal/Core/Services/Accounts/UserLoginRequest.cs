namespace HR_Assist.Core.Services.Accounts
{
    using System.ComponentModel.DataAnnotations;
    using MediatR;
    using HR_Assist.Core.Services.Common.Models;

    public class UserLoginRequest : IRequest<ResponseModel>
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
