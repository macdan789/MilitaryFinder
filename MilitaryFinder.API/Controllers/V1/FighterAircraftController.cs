using Microsoft.AspNetCore.Mvc;
using MilitaryFinder.API.Contracts.V1;
using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
using MilitaryFinder.API.Domain;
using System;
using System.Collections.Generic;

namespace MilitaryFinder.API.Controllers.V1
{
    public class FighterAircraftController : Controller
    {
        private List<FighterAircraft> _aircrafts;

        public FighterAircraftController()
        {
            _aircrafts = new List<FighterAircraft>();
            for (int i = 0; i < 5; i++)
            {
                _aircrafts.Add(new FighterAircraft { Id = Guid.NewGuid().ToString() });
            }
        }


        [HttpGet(ApiRoutes.FighterAircraft.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_aircrafts);
        }


        [HttpPost(ApiRoutes.FighterAircraft.Create)]
        public IActionResult Create([FromBody] FighterAircraftRequest request)
        {
            var fighterAircraft = new FighterAircraft { Id = request.Id };

            _aircrafts.Add(fighterAircraft);

            var baseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var location = baseUri + "/" + ApiRoutes.FighterAircraft.Get.Replace("{id}", fighterAircraft.Id);
            
            var response = new FighterAircraftResponse { Id = fighterAircraft.Id };

            return Created(location, response);
        }

    }
}
