﻿using AdventOfCode2020;

string[] inputLines = System.IO.File.ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/InputDay14.txt");

var day = new Day14();

var firstStarSolution = "not implemented";
var secondStarSolution = "not implemented";

try
{
    firstStarSolution = day.FirstStar(inputLines);
}
catch (NotImplementedException) { }

try
{
    secondStarSolution = day.SecondStar(inputLines);
}
catch(NotImplementedException) { }

Console.WriteLine($"First star solution: {firstStarSolution}, second star solution: {secondStarSolution}.");
