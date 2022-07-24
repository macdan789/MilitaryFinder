namespace MilitaryFinder.API.Contracts.V1.Requests
{
    public class UserLoginRequest
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
