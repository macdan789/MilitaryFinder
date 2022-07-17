using Cosmonaut;
using Cosmonaut.Extensions;
using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
using MilitaryFinder.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilitaryFinder.API.Services
{
    public class FighterAircraftCosmosDBService : IFighterAircraftService
    {
        private readonly ICosmosStore<CosmosFighterAircraftDto> _service;

        public FighterAircraftCosmosDBService(ICosmosStore<CosmosFighterAircraftDto> service)
        {
            _service = service;
        }


        public async Task<bool> CreateAircraftAsync(FighterAircraft aircraft)
        {
            //Generate new GUID for each item to create 'cause CosmosDB doesn't generate these values automatically
            var entity = new CosmosFighterAircraftDto { Id = Guid.NewGuid().ToString(), Model = aircraft.Model };

            var response = await _service.AddAsync(entity);
            aircraft.Id = Guid.Parse(response.Entity.Id);

            return response.IsSuccess;
        }

        public async Task<bool> DeleteAircraftAsync(Guid aircraftId)
        {
            var response = await _service.RemoveByIdAsync(aircraftId.ToString(), aircraftId.ToString());

            return response.IsSuccess;
        }

        public async Task<FighterAircraftResponse> GetAircraftAsync(Guid aircraftId)
        {
            var aircraft = await _service.FindAsync(aircraftId.ToString(), aircraftId.ToString());

            if(aircraft != null)
            {
                var response = new FighterAircraftResponse { Id = aircraftId, Model = aircraft.Model };

                return response;
            }

            return null;
        }

        public async Task<List<FighterAircraftResponse>> GetAllAircraftsAsync()
        {
            var aircrafts = await _service.Query().ToListAsync();

            var response = aircrafts.Select(x => new FighterAircraftResponse { Id = Guid.Parse(x.Id), Model = x.Model }).ToList();

            return response;
        }

        public async Task<bool> UpdateAircraftAsync(Guid aircraftId, UpdateFighterAircraft aircraft)
        {
            var entity = new CosmosFighterAircraftDto { Id = aircraftId.ToString(), Model = aircraft.Model };

            var response = await _service.UpdateAsync(entity);

            return response.IsSuccess;
        }
    }
}
