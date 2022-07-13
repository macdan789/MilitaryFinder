using Microsoft.AspNetCore.Mvc;
using MilitaryFinder.API.Contracts.V1;
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
                _aircrafts.Add(new FighterAircraft { Id = Guid.NewGuid() });
            }
        }

        [HttpGet(ApiRoutes.FighterAircraft.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_aircrafts);
        }
    }
}
