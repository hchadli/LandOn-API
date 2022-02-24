using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using LandonApi.Models;
using LandonAPI.Models;

namespace LandonAPI.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.1")]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                Rooms = Link.ToCollection(nameof(RoomsController.GetAllRooms)),
                Info = Link.To(nameof(InfoController.GetInfo))
            };

            return Ok(response);
        }
    }
}
