using Microsoft.EntityFrameworkCore;
using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
using MilitaryFinder.API.Data;
using MilitaryFinder.API.Domain;
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

        public async Task<bool> DeleteAircraftAsync(string aircraftId)
        {
            var domainAircraft = await _dbContext.FighterAircraft.SingleOrDefaultAsync(x => x.Id == aircraftId);

            _dbContext.FighterAircraft.Remove(domainAircraft);
            var deleted = await _dbContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<FighterAircraftResponse> GetAircraftAsync(string aircraftId)
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
            var response = new List<FighterAircraftResponse>();

            var aircrafts = await _dbContext.FighterAircraft.ToListAsync();

            foreach (var aircraft in aircrafts)
            {
                response.Add(new FighterAircraftResponse { Id = aircraft.Id, Model = aircraft.Model });
            }

            return response;
        }

        public async Task<bool> UpdateAircraftAsync(string aircraftId, UpdateFighterAircraft aircraft)
        {
            var domainAircraft = new FighterAircraft { Id = aircraftId, Model = aircraft.Model };

            _dbContext.FighterAircraft.Update(domainAircraft);
            var updated = await _dbContext.SaveChangesAsync();
            
            return updated > 0;
        }
    }
}
