using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class PausableLongCodeProgram
    {
        public List<long> Program { get; set; }
        public List<long> Input { get; set; }
        public List<long> Output { get; set; }
        public int ProgramCounter { get; set; }
        public int RelativeBase { get; set; }
        public int InputPointer { get; set; }
        public bool ProgramHasFinished => Program[ProgramCounter] == 99;
    }
}
