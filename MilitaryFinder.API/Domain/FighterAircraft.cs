using System;
using System.ComponentModel.DataAnnotations;

namespace MilitaryFinder.API.Domain
{
    public class FighterAircraft
    {
        [Key]
        public Guid Id { get; set; }
        public string Model { get; set; }
    }
}
