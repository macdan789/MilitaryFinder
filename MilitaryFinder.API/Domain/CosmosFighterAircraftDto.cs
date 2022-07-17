using Cosmonaut.Attributes;
using Newtonsoft.Json;

namespace MilitaryFinder.API.Domain
{
    [CosmosCollection("FighterAircraft")]
    public class CosmosFighterAircraftDto
    {
        //Azure Cosmos DB doesn't work with GUID type, so Id is string
        [CosmosPartitionKey]
        [JsonProperty(PropertyName = "aircraftId")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "aircraftModel")]
        public string Model { get; set; }
    }
}
