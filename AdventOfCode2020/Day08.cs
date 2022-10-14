using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    internal class Day08 : IDay
    {
        public string FirstStar(string[] inputLines)
        {
            var instructions = ParseInstructions(inputLines);

            var (acc, _) = Execute(instructions);
            return acc.ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            var instructions = ParseInstructions(inputLines);
            foreach (var tweakedInstructions in GenerateFixedInstructions(instructions))
            {
                var (acc, instructionIndex) = Execute(tweakedInstructions);
                if (instructionIndex == tweakedInstructions.Count)
                {
                    return acc.ToString();
                }
            }
            throw new Exception("Could not find the correct solution.");
        }

        enum Instruction
        {
            NOP,
            ACC,
            JMP
        }

        private IEnumerable<List<(Instruction, int)>> GenerateFixedInstructions(List<(Instruction, int)> originalInstructions)
        {
            int instructionIndex = 0;
            while (instructionIndex < originalInstructions.Count)
            {
                if (originalInstructions[instructionIndex].Item1 != Instruction.ACC)
                {
                    var patchedInstruction = originalInstructions[instructionIndex].Item1 == Instruction.JMP ? Instruction.NOP : Instruction.JMP;
                    var insrtuctionsCopy = new List<(Instruction, int)>(originalInstructions);
                    insrtuctionsCopy[instructionIndex] = (patchedInstruction, originalInstructions[instructionIndex].Item2);
                    yield return insrtuctionsCopy;
                }
                instructionIndex++;
            }
        }

        private (int, int) Execute(List<(Instruction, int)> instructions)
        {
            var instructionIndex = 0;
            var visitedLines = new HashSet<int>();
            var acc = 0;

            while (true)
            {
                if (instructionIndex == instructions.Count)
                {
                    return (acc, instructionIndex);
                }

                if (visitedLines.Contains(instructionIndex))
                {
                    return (acc, instructionIndex);
                }

                var (instruction, argument) = instructions[instructionIndex];
                visitedLines.Add(instructionIndex);
                switch (instruction)
                {
                    case Instruction.JMP:
                        instructionIndex += argument;
                        break;
                    case Instruction.ACC:
                        acc += argument;
                        instructionIndex += 1;
                        break;
                    case Instruction.NOP:
                        instructionIndex += 1;
                        break;
                }

            }
        }

        private List<(Instruction, int)> ParseInstructions(string[] inputLines)
        {
            var instructions = new List<(Instruction, int)>();
            foreach (var line in inputLines)
            {
                var splits = line.Split(" ");
                var instruction = Instruction.NOP;
                if (splits[0] == "acc")
                {
                    instruction = Instruction.ACC;
                }
                else if (splits[0] == "jmp")
                {
                    instruction = Instruction.JMP;
                }
                instructions.Add((instruction, int.Parse(splits[1])));
            }
            return instructions;
        }
    }
}
