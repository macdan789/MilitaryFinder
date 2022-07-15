using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
using MilitaryFinder.API.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilitaryFinder.API.Services
{
    public interface IFighterAircraftService
    {
        Task<FighterAircraftResponse> GetAircraftAsync(string aircraftId);
        Task<List<FighterAircraftResponse>> GetAllAircraftsAsync();
        Task<bool> CreateAircraftAsync(FighterAircraftRequest aircraft);
        Task<bool> UpdateAircraftAsync(string aircraftId, UpdateFighterAircraft aircraft);
        Task<bool> DeleteAircraftAsync(string aircraftId);
    }
}
