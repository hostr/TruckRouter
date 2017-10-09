# Truck Router
Truck routing API that takes in a maze as JSON and outputs the solved maze. Uses the A* Algorithm to quickly find the most optimal path. Built with .NET Core 2.0 Web API and MSTest.

## How to Run
__Visual Studio__
- Open up solution in Visual Studio
- Set MapSolver as startup project
- Run project (F5) and server will start up
- POST endpoint is at http://localhost:8080/api/maze/solveMaze

## Sample Request
- Multiline strings aren't valid JSON so use this [link](http://static.decontextualize.com/lines-to-json/) to convert to a JSON string array
- Using a string array instead of single string with newline characters so we can keep the nice multiline formatting
- \# is a wall, A is the starting point, B is the destination, and . are open points
```
{
  "maze": [
    "##########",
    "#A...#...#",
    "#.#.##.#.#",
    "#.#.##.#.#",
    "#.#....#B#",
    "#.#.##.#.#",
    "#....#...#",
    "##########"
  ]
}
```

## Sample Response
- Outputs the same maze with @ as the most optimal path
```
{
  "steps": 14,
  "solution": [
    "##########",
    "#A@@.#...#",
    "#.#@##.#.#",
    "#.#@##.#.#",
    "#.#@@@@#B#",
    "#.#.##@#@#",
    "#....#@@@#",
    "##########"
  ] 
}
```

Credit to [WichardRiezebos](https://github.com/WichardRiezebos/astar-navigator/tree/master/AStarNavigator) for implementation of the A* Algorithm.