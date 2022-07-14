using MilitaryFinder.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryFinder.API.Services
{
    public class FighterAircraftService : IFighterAircraftService
    {
        private readonly List<FighterAircraft> _aircrafts;

        public FighterAircraftService()
        {
            _aircrafts = new List<FighterAircraft>();
            for (var i = 0; i < 5; i++)
                _aircrafts.Add(new FighterAircraft
                {
                    Id = Guid.NewGuid().ToString(),
                    Model = $"Model {i}"
                });
        }

        public void CreateAircraft(FighterAircraft aircraft)
        {
            _aircrafts.Add(aircraft);
        }

        public FighterAircraft GetAircraft(string aircraftId)
        {
            return _aircrafts.SingleOrDefault(x => x.Id == aircraftId);
        }

        public List<FighterAircraft> GetAllAircrafts()
        {
            return _aircrafts;
        }
    }
}
