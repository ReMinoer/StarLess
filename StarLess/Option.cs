using System.Collections.Generic;
using System.Linq;

namespace Diese.ConsoleInterface
{
    public struct Option
    {
        public string ShortKey { get; set; }
        public string LongKey { get; set; }
        public string Description { get; set; }
        public List<Argument> Arguments { get; set; }

        public Option(string shortKey, string longKey, string description, params Argument[] arguments)
            : this()
        {
            ShortKey = shortKey;
            LongKey = longKey;
            Description = description;
            Arguments = arguments.ToList();
        }
    }
}