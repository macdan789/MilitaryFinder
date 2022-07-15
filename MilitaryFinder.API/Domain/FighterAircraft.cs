using System.ComponentModel.DataAnnotations;

namespace MilitaryFinder.API.Domain
{
    public class FighterAircraft
    {
        [Key]
        public string Id { get; set; }
        public string Model { get; set; }
    }
}
