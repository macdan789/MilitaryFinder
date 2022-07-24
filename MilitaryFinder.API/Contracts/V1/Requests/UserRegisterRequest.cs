using System.ComponentModel.DataAnnotations;

namespace MilitaryFinder.API.Contracts.V1.Requests
{
    public class UserRegisterRequest
    {
        [EmailAddress(ErrorMessage = "Email Address is invalid.")]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
