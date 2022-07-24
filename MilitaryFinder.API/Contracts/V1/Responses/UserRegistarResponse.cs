﻿using System.Collections.Generic;

namespace MilitaryFinder.API.Contracts.V1.Responses
{
    public class UserRegistarResponse
    {
        public string Token { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
