using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MapSolver.Interfaces;
using MapSolver.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MapSolver.Controllers
{
    [Route("api/[controller]")]
    public class MazeController : Controller
    {
        private readonly ISolvingService _service;
        private readonly IMemoryCache _cache;

        public MazeController(ISolvingService service, IMemoryCache memoryCache)
        {
            _service = service;
            _cache = memoryCache;
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
            // If request doesn't exist then return a 400 error
            if (request == null || !request.Map.Any())
            {
                return new BadRequestResult();
            }

            // Convert maze to a single line so we can use it as a caching key
            var singleLineMaze = string.Join("", request.Map);

            // If the route has been cached already then use that as the solution
            if (_cache.TryGetValue(singleLineMaze, out SolveMazeResponse solution))
            {
                return new JsonResult(solution);
            }

            // Solve maze
            try
            {
                var result = _service.Solve(request.Map);

                if (result.Steps != 0)
                {
                    // If map get's queried again it will reset the sliding expiration, 
                    // but eventually the absolute expiration will catch it and expire it
                    _cache.Set(singleLineMaze, result, new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(2),
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5)
                    });
                }

                //Output solution to maze
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                // Could use external logging service here
                Debug.WriteLine(ex.Message);

                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }       
        }
    }
}
