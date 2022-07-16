using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FighterAircraftController> _logger;
        private readonly IFighterAircraftService _service;

        public FighterAircraftController(ILogger<FighterAircraftController> logger, IFighterAircraftService service)
        {
            _logger = logger;
            _service = service;
        }


        [HttpGet(ApiRoutes.FighterAircraft.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var aircrafts = await _service.GetAllAircraftsAsync();

            _logger.LogInformation($"[Request: GET {ApiRoutes.FighterAircraft.GetAll}] [Status: 200] [Count of items: {aircrafts.Count}]");

            return Ok(aircrafts);
        }


        [HttpGet(ApiRoutes.FighterAircraft.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid aircraftId)
        {
            var aircraft = await _service.GetAircraftAsync(aircraftId);

            return aircraft != null ? Ok(aircraft) : NotFound();
        }


        [HttpPost(ApiRoutes.FighterAircraft.Create)]
        public async Task<IActionResult> Create([FromBody] FighterAircraftRequest request)
        {
            var domainAircraft = new FighterAircraft { Model = request.Model };

            var created = await _service.CreateAircraftAsync(domainAircraft);

            if (created)
            {
                var baseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                var location = baseUri + "/" + ApiRoutes.FighterAircraft.Get.Replace("{aircraftId}", domainAircraft.Id.ToString());

                return Created(location, domainAircraft);
            }

            return BadRequest();
        }


        [HttpPut(ApiRoutes.FighterAircraft.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid aircraftId, [FromBody] UpdateFighterAircraft aircraft)
        {
            var isUpdated = await _service.UpdateAircraftAsync(aircraftId, aircraft);

            return isUpdated ? NoContent() : NotFound();
        }


        [HttpDelete(ApiRoutes.FighterAircraft.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid aircraftId)
        {
            var isDeleted = await _service.DeleteAircraftAsync(aircraftId);

            return isDeleted ? NoContent() : NotFound();
        }
    }
}
