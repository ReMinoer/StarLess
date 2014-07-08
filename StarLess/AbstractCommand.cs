﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StarLess
{
    public abstract class AbstractCommand : ICommand
    {
        protected internal AbstractCommand(string keyword, string description)
        {
            Keyword = keyword;
            Description = description;
            Options = new OptionsList();
        }

        public string Keyword { get; private set; }
        public string Description { get; private set; }
        public abstract ArgumentsList Arguments { get; }
        public OptionsList Options { get; private set; }

        public void Run(params string[] args)
        {
            ArgumentsValues arguments;
            OptionsValues options;

            CheckValidity(args, out arguments, out options);

            Action(arguments, options);
        }

        public abstract string CompleteDescription();

        protected abstract void CheckValidity(string[] args, out ArgumentsValues arguments,
                                              out OptionsValues options);

        protected abstract void Action(ArgumentsValues arguments, OptionsValues options);

        public class ArgumentsList : List<Argument> {}

        protected class ArgumentsValues : Dictionary<string, string>
        {
            new public object this[string key]
            {
                get
                {
                    string stringValue;
                    TryGetValue(key, out stringValue);
                    return
                        TypeDescriptor.GetConverter(_arguments.First(a => a.Name == key).Type).
                                       ConvertFromString(stringValue);
                }
            }
            private readonly List<Argument> _arguments;

            public ArgumentsValues(List<Argument> arguments)
            {
                _arguments = arguments;
            }
        }

        public class OptionsList : List<Option>
        {
            public Option? this[string key]
            {
                get
                {
                    if (key.Length < 2 || key.ElementAt(0) != '-')
                        return null;

                    string s;
                    if (key.Length >= 3 && key.ElementAt(1) == '-')
                    {
                        s = key.Substring(2);
                        if (Exists(o => o.LongKey == s))
                            return Find(o => o.LongKey == s);
                        return null;
                    }
                    s = key.Substring(1);
                    if (Exists(o => o.ShortKey == s))
                        return Find(o => o.ShortKey == s);
                    return null;
                }
            }
        }

        protected class OptionsValues : Dictionary<string, ArgumentsValues> {}
    }
}