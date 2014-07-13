using System.Collections.Generic;
using System.Linq;
using StarLess.Interfaces;

namespace StarLess
{
    public struct Option
    {
        public string ShortKey { get; set; }
        public string LongKey { get; set; }
        public string Description { get; set; }
        public List<IArgument> Arguments { get; set; }

        public Option(string shortKey, string longKey, string description, params IArgument[] arguments)
            : this()
        {
            ShortKey = shortKey;
            LongKey = longKey;
            Description = description;
            Arguments = arguments.ToList();
        }
    }
}