using System.Collections.Generic;

namespace AdventOfCode
{
    public class Chemical
    {
        public string Name { get; set; }
        public long Amount { get; set; }
        public List<Chemical> Cost { get; set; }
    }
}
