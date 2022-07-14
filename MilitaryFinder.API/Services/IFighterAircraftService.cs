using MilitaryFinder.API.Domain;
using System.Collections.Generic;

namespace MilitaryFinder.API.Services
{
    public interface IFighterAircraftService
    {
        FighterAircraft GetAircraft(string aircraftId);
        List<FighterAircraft> GetAllAircrafts();
        void CreateAircraft(FighterAircraft aircraft);
    }
}
