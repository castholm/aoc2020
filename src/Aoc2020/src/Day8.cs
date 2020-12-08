using System;
using System.Collections;
using System.Linq;

namespace Aoc2020
{
    public static class Day8
    {
        public static string Part1(string input)
            => ExecuteProgram(ParseProgram(input)).Accumulator.ToString();

        public static string Part2(string input)
        {
            var program = ParseProgram(input);

            var accumulator = 0;
            for (int i = 0; i < program.Length; i++)
            {
                ref var instruction = ref program[i];

                if (instruction.Operation is not ("nop" or "jmp"))
                    continue;

                var instructionCopy = instruction;

                instruction = instruction.Operation switch
                {
                    "jmp" => new Instruction("nop", instruction.Argument),
                    "nop" => new Instruction("jmp", instruction.Argument),
                    _ => throw new InvalidOperationException(),
                };

                int programCounter;
                (accumulator, programCounter) = ExecuteProgram(program);

                if (programCounter >= program.Length)
                    break;

                instruction = instructionCopy;
            }

            return accumulator.ToString();
        }

        private static Instruction[] ParseProgram(string input)
            => input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(i => i.Split(' ', 2))
            .Select(i => new Instruction(i[0], int.Parse(i[1])))
            .ToArray();

        private static (int Accumulator, int ProgramCounter) ExecuteProgram(ReadOnlySpan<Instruction> program)
        {
            var accumulator = 0;
            var programCounter = 0;
            var visited = new BitArray(program.Length);

            while (programCounter < program.Length && !visited[programCounter])
            {
                ref readonly var instruction = ref program[programCounter];

                visited[programCounter] = true;

                (accumulator, programCounter) = instruction.Operation switch
                {
                    "acc" => (accumulator + instruction.Argument, programCounter + 1),
                    "jmp" => (accumulator, programCounter + instruction.Argument),
                    "nop" => (accumulator, programCounter + 1),
                    _ => throw new InvalidOperationException(),
                };
            }

            return (accumulator, programCounter);
        }

        private readonly struct Instruction
        {
            public Instruction(string operation, int argument)
            {
                Operation = operation;
                Argument = argument;
            }

            public readonly string Operation;
            public readonly int Argument;
        }
    }
}
