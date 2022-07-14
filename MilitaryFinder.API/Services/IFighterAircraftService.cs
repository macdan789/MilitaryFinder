using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
using MilitaryFinder.API.Domain;
using System.Collections.Generic;

namespace MilitaryFinder.API.Services
{
    public interface IFighterAircraftService
    {
        FighterAircraftResponse GetAircraft(string aircraftId);
        List<FighterAircraftResponse> GetAllAircrafts();
        void CreateAircraft(FighterAircraftRequest aircraft);
        bool UpdateAircraft(string aircraftId, UpdateFighterAircraft aircraft);
    }
}
