using System.Collections.Generic;

namespace MilitaryFinder.API.Domain
{
    public class AuthentificationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
