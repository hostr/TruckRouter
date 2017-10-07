using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Interfaces;
using MapSolver.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MapSolver.Controllers
{
    [Route("api/[controller]")]
    public class MazeController : Controller
    {
        private readonly ISolvingService _service;

        public MazeController(ISolvingService service)
        {
            _service = service;
        }

        /// <summary>
        /// Endpoints used to solve mazes. 
        /// </summary>
        /// <param name="request">Maze as string array</param>
        /// <returns>Maze with optimal path to reach destination and amount of steps it took</returns>
        [HttpPost]
        [Route("solveMaze")]
        public IActionResult SolveMaze([FromBody] SolveMazeRequest request)
        {
            if (request == null || !request.Map.Any())
            {
                return new BadRequestResult();
            }

            // Solve maze
            var result = _service.Solve(request.Map);

            //Output solution to maze
            return new JsonResult(result); 
        }
    }
}
