using Microsoft.AspNetCore.Mvc;
using MilitaryFinder.API.Contracts.V1;
using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
using MilitaryFinder.API.Domain;
using MilitaryFinder.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IActionResult GetAll()
        {
            var aircrafts = _service.GetAllAircrafts();

            return Ok(aircrafts);
        }


        [HttpGet(ApiRoutes.FighterAircraft.Get)]
        public IActionResult Get([FromRoute] string aircraftId)
        {
            var aircraft = _service.GetAircraft(aircraftId);

            if (aircraft == null)
                return NotFound();

            return Ok(aircraft);
        }


        [HttpPost(ApiRoutes.FighterAircraft.Create)]
        public IActionResult Create([FromBody] FighterAircraftRequest request)
        {
            _service.CreateAircraft(request);

            var baseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var location = baseUri + "/" + ApiRoutes.FighterAircraft.Get.Replace("{id}", request.Id);
            
            var response = new FighterAircraftResponse { Id = request.Id, Model = request.Model };

            return Created(location, response);
        }

    }
}
