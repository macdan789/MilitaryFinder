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
            var aircraft = new FighterAircraft { Id = request.Id, Model = request.Model };

            _service.CreateAircraft(aircraft);

            var baseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var location = baseUri + "/" + ApiRoutes.FighterAircraft.Get.Replace("{id}", aircraft.Id);
            
            var response = new FighterAircraftResponse { Id = aircraft.Id, Model = aircraft.Model };

            return Created(location, response);
        }

    }
}
