using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
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

        public void CreateAircraft(FighterAircraftRequest aircraft)
        {
            var domainAircraft = new FighterAircraft
            {
                Id = aircraft.Id,
                Model = aircraft.Model
            };

            _aircrafts.Add(domainAircraft);
        }

        public bool DeleteAircraft(string aircraftId)
        {
            var aircraftIndex = _aircrafts.FindIndex(x => x.Id == aircraftId);

            if(aircraftIndex >= 0)
            {
                _aircrafts.RemoveAt(aircraftIndex);

                return true;
            }

            return false;
        }

        public FighterAircraftResponse GetAircraft(string aircraftId)
        {
            var domainAircraft = _aircrafts.SingleOrDefault(x => x.Id == aircraftId);

            if(domainAircraft is not null)
            {
                var response = new FighterAircraftResponse
                {
                    Id = domainAircraft.Id,
                    Model = domainAircraft.Model
                };

                return response;
            }

            return null;
        }

        public List<FighterAircraftResponse> GetAllAircrafts()
        {
            var response = new List<FighterAircraftResponse>();

            foreach (var aircraft in _aircrafts)
            {
                response.Add(new FighterAircraftResponse
                {
                    Id = aircraft.Id,
                    Model = aircraft.Model
                });
            }

            return response;
        }

        public bool UpdateAircraft(string aircraftId, UpdateFighterAircraft aircraft)
        {
            var aircraftIndex = _aircrafts.FindIndex(x => x.Id == aircraftId);

            if(aircraftIndex >= 0)
            {
                _aircrafts[aircraftIndex].Model = aircraft.Model;
                return true;
            }

            return false;
        }
    }
}
