using Microsoft.EntityFrameworkCore;
using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
using MilitaryFinder.API.Data;
using MilitaryFinder.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilitaryFinder.API.Services
{
    public class FighterAircraftService : IFighterAircraftService
    {
        private readonly DataContext _dbContext;

        public FighterAircraftService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAircraftAsync(FighterAircraft aircraft)
        {
            await _dbContext.FighterAircraft.AddAsync(aircraft);
            var created = await _dbContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> DeleteAircraftAsync(Guid aircraftId)
        {
            var domainAircraft = await _dbContext.FighterAircraft.SingleOrDefaultAsync(x => x.Id == aircraftId);

            if (domainAircraft is not null)
            {
                _dbContext.FighterAircraft.Remove(domainAircraft);
                var deleted = await _dbContext.SaveChangesAsync();

                return deleted > 0;
            }

            return false;
        }

        public async Task<FighterAircraftResponse> GetAircraftAsync(Guid aircraftId)
        {
            var domainAircraft = await _dbContext.FighterAircraft.SingleOrDefaultAsync(x => x.Id == aircraftId);

            if(domainAircraft is not null)
            {
                var response = new FighterAircraftResponse { Id = domainAircraft.Id, Model = domainAircraft.Model };

                return response;
            }

            return null;
        }

        public async Task<List<FighterAircraftResponse>> GetAllAircraftsAsync()
        {
            var aircrafts = await _dbContext.FighterAircraft.ToListAsync();

            var response = aircrafts.Select(x => new FighterAircraftResponse { Id = x.Id, Model = x.Model }).ToList();   

            return response;
        }

        public async Task<bool> UpdateAircraftAsync(Guid aircraftId, UpdateFighterAircraft aircraft)
        {
            var domainAircraft = new FighterAircraft { Id = aircraftId, Model = aircraft.Model };

            _dbContext.FighterAircraft.Update(domainAircraft);
            var updated = await _dbContext.SaveChangesAsync();
            
            return updated > 0;
        }
    }
}
