using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
using MilitaryFinder.API.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilitaryFinder.API.Services
{
    public interface IFighterAircraftService
    {
        Task<FighterAircraftResponse> GetAircraftAsync(Guid aircraftId);
        Task<List<FighterAircraftResponse>> GetAllAircraftsAsync();
        Task<bool> CreateAircraftAsync(FighterAircraft aircraft);
        Task<bool> UpdateAircraftAsync(Guid aircraftId, UpdateFighterAircraft aircraft);
        Task<bool> DeleteAircraftAsync(Guid aircraftId);
    }
}
