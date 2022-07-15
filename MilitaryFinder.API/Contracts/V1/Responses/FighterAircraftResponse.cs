using System;

namespace MilitaryFinder.API.Contracts.V1.Responses
{
    public class FighterAircraftResponse
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
    }
}
