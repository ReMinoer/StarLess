﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace StarLess
{
    // TODO : check if option key already exist (add methods)
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

        protected abstract void CheckValidity(string[] args, out ArgumentsValues arguments, out OptionsValues options);

        protected abstract void Action(ArgumentsValues args, OptionsValues options);

        protected class ArgumentsDictionary : Dictionary<string, string> {}

        public class ArgumentsList : List<IArgument> {}

        protected class ArgumentsValues : ReadOnlyDictionary<string, string>
        {
            public ArgumentsValues(IDictionary<string, string> dictionary)
                : base(dictionary) {}

            public T Value<T>(string parameterName)
            {
                string stringValue;
                if (!TryGetValue(parameterName, out stringValue))
                    throw new KeyNotFoundException();

                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(stringValue);
            }
        }

        protected class OptionsDictionary : Dictionary<Option.OptionKeys, ArgumentsValues> {}

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
                        if (Exists(o => o.Key.Long == s))
                            return Find(o => o.Key.Long == s);
                        return null;
                    }
                    s = key.Substring(1);
                    if (Exists(o => o.Key.Short == s))
                        return Find(o => o.Key.Short == s);

                    return null;
                }
            }
        }

        protected class OptionsValues : ReadOnlyDictionary<Option.OptionKeys, ArgumentsValues>
        {
            public ArgumentsValues this[string key]
            {
                get
                {
                    if (ContainsKey(key))
                        return this.First(o => o.Key.IsShortOrLong(key)).Value;
                    throw new KeyNotFoundException();
                }
            }

            public OptionsValues(IDictionary<Option.OptionKeys, ArgumentsValues> dictionary)
                : base(dictionary) {}

            public bool ContainsKey(string key)
            {
                return this.Any(o => o.Key.IsShortOrLong(key));
            }
        }
    }
}