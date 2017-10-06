# Truck Router

Truck routing API that takes in a maze as JSON and outputs the solved maze. 

## Sample Request
- Use this [link](http://static.decontextualize.com/lines-to-json/) to convert multiline string maze to valid JSON string array
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