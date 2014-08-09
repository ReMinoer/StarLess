using System.Collections.Generic;
using System.Linq;

namespace StarLess
{
    public struct Option
    {
        public OptionKeys Key { get; set; }
        public string Description { get; set; }
        public List<IArgument> Arguments { get; set; }

        public Option(string shortKey, string longKey, string description, params IArgument[] arguments)
            : this()
        {
            Key = new OptionKeys {Long = longKey, Short = shortKey};
            Description = description;
            Arguments = arguments.ToList();
        }

        public struct OptionKeys
        {
            public string Short { get; set; }
            public string Long { get; set; }

            public string ShortFormated { get { return "-" + Short; } }
            public string LongFormated { get { return "--" + Long; } }

            public OptionKeys(string shortKey, string longKey)
                : this()
            {
                Short = shortKey;
                Long = longKey;
            }

            public bool IsShortOrLong(string key)
            {
                return key == Short || key == Long;
            }
        }
    }
}