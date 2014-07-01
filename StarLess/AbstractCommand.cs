using System.Collections.Generic;
using System.Linq;

namespace StarLess
{
    public abstract class AbstractCommand : ICommand
    {
        public string Keyword { get; private set; }
        public string Description { get; private set; }
        public ArgumentsList Arguments { get; private set; }
        public OptionsList Options { get; private set; }

        protected internal AbstractCommand(string keyword, string description)
        {
            Keyword = keyword;
            Description = description;
            Arguments = new ArgumentsList();
            Options = new OptionsList();
        }

        public void Run(params string[] args)
        {
            ArgumentsValues arguments;
            OptionsValues options;

            CheckValidity(args, out arguments, out options);

            Action(arguments, options);
        }

        protected abstract void CheckValidity(string[] args, out ArgumentsValues arguments, out OptionsValues options);

        protected abstract void Action(ArgumentsValues arguments, OptionsValues options);

        public abstract string CompleteDescription();

        public class ArgumentsList : List<Argument>
        {
        }

        public class OptionsList : List<Option>
        {
            public Option? this[string key]
            {
                get
                {
                    if (key.Length >= 2 && key.ElementAt(0) == '-')
                    {
                        string s;
                        if (key.Length >= 3 && key.ElementAt(1) == '-')
                        {
                            s = key.Substring(2);
                            if (Exists(o => o.LongKey == s))
                                return Find(o => o.LongKey == s);
                            else
                                return null;
                        }
                        else
                        {
                            s = key.Substring(1);
                            if (Exists(o => o.ShortKey == s))
                                return Find(o => o.ShortKey == s);
                            else
                                return null;
                        }
                    }
                    else
                        return null;
                }
            }
        }

        protected class ArgumentsValues : Dictionary<string, string>
        {
        }

        protected class OptionsValues : Dictionary<string, ArgumentsValues>
        {
        }
    }
}