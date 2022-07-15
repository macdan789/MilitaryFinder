using Microsoft.AspNetCore.Mvc;
using MilitaryFinder.API.Contracts.V1;
using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
using MilitaryFinder.API.Domain;
using MilitaryFinder.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilitaryFinder.API.Controllers.V1
{
    public class FighterAircraftController : Controller
    {
        private readonly IFighterAircraftService _service;

        public FighterAircraftController(IFighterAircraftService service)
        {
            _service = service;
        }


        [HttpGet(ApiRoutes.FighterAircraft.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var aircrafts = await _service.GetAllAircraftsAsync();

            return Ok(aircrafts);
        }


        [HttpGet(ApiRoutes.FighterAircraft.Get)]
        public async Task<IActionResult> Get([FromRoute] string aircraftId)
        {
            var aircraft = await _service.GetAircraftAsync(aircraftId);

            if (aircraft == null)
                return NotFound();

            return Ok(aircraft);
        }


        [HttpPost(ApiRoutes.FighterAircraft.Create)]
        public async Task<IActionResult> Create([FromBody] FighterAircraftRequest request)
        {
            var domainAircraft = new FighterAircraft { Model = request.Model };

            var created = await _service.CreateAircraftAsync(domainAircraft);

            if (created)
            {
                var baseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                var location = baseUri + "/" + ApiRoutes.FighterAircraft.Get.Replace("{id}", domainAircraft.Id);

                return Created(location, domainAircraft);
            }

            return BadRequest();
        }


        [HttpPut(ApiRoutes.FighterAircraft.Update)]
        public async Task<IActionResult> Update([FromRoute] string aircraftId, [FromBody] UpdateFighterAircraft aircraft)
        {
            var isUpdated = await _service.UpdateAircraftAsync(aircraftId, aircraft);

            return isUpdated ? NoContent() : NotFound();
        }


        [HttpDelete(ApiRoutes.FighterAircraft.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string aircraftId)
        {
            var isDeleted = await _service.DeleteAircraftAsync(aircraftId);

            return isDeleted ? NoContent() : NotFound();
        }
    }
}
