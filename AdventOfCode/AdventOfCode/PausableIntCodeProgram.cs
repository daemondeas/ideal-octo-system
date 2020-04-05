using System.Collections.Generic;

namespace AdventOfCode
{
    public class PausableIntCodeProgram
    {
        public List<int> Program { get; set; }
        public List<int> Input { get; set; }
        public List<int> Output { get; set; }
        public int ProgramCounter { get; set; }
        public int InputPointer { get; set; }
        public bool ProgramHasFinished => Program[ProgramCounter] == 99;
    }
}
