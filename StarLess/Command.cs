using System.Collections.Generic;
using System.Linq;
using Diese.ConsoleInterface.Exceptions;

namespace Diese.ConsoleInterface
{
    // TODO : Check if an argument appears twice
    // TODO : Check if an argument is a mistake
    // TODO : Handle unlimited arguments
    public abstract class Command
    {
        public string Keyword { get; set; }
        public string Description { get; set; }

        public ArgumentsDescriptions RequiredArguments { get; set; }
        public ArgumentsDescriptions OptionalArguments { get; set; }
        public OptionsDescriptions Options { get; set; }
        public bool UnlimitedArguments { get; set; }

        protected Command(string keyword, string description)
        {
            Keyword = keyword;
            Description = description;
            RequiredArguments = new ArgumentsDescriptions();
            OptionalArguments = new ArgumentsDescriptions();
            Options = new OptionsDescriptions();
            UnlimitedArguments = false;
        }

        public void Run(string[] args)
        {
            if (!UnlimitedArguments)
            {
                int argsCount = 0;
                for (int i = 0; i < args.Length; i++)
                {
                    if (Options[args[i]].HasValue)
                    {
                        i += Options[args[i]].Value.Arguments.Count;
                        continue;
                    }

                    argsCount++;
                }

                if (argsCount < RequiredArguments.Count)
                    throw new NumberOfArgumentsException(argsCount, RequiredArguments.Count);

                if (argsCount > RequiredArguments.Count + OptionalArguments.Count)
                    throw new NumberOfArgumentsException(argsCount, RequiredArguments.Count + OptionalArguments.Count);
            }

            ArgumentsValues arguments = new ArgumentsValues();
            OptionsValues options = new OptionsValues();

            int j = 0;
            int k = 0;
            for (int i = 0; i < args.Length; i++)
            {
                if (Options[args[i]].HasValue)
                {
                    Option o = Options[args[i]].Value;

                    ArgumentsValues optionArgs = new ArgumentsValues();
                    for (int x = 0; x < o.Arguments.Count; x++)
                    {
                        i++;
                        Argument a = o.Arguments[x];
                        if (!a.isValid(args[i]))
                            throw new ArgumentNotValidException(a, j);

                        optionArgs.Add(a.Name, args[i]);
                    }
                    options.Add(o.ShortKey, optionArgs);
                    continue;
                }

                if (UnlimitedArguments)
                {
                    arguments.Add(j.ToString(), args[i]);
                    j++;
                }
                else
                {
                    if (j < RequiredArguments.Count)
                    {
                        if (!RequiredArguments[j].isValid(args[i]))
                            throw new ArgumentNotValidException(RequiredArguments[j], j);

                        arguments.Add(RequiredArguments[j].Name, args[i]);
                        j++;
                    }
                    else
                    {
                        if (!OptionalArguments[k].isValid(args[i]))
                            throw new ArgumentNotValidException(OptionalArguments[k], j + k);

                        arguments.Add(OptionalArguments[k].Name, args[i]);
                        k++;
                    }
                }
            }

            Action(arguments, options);
        }

        protected abstract void Action(ArgumentsValues arguments, OptionsValues options);

        public class ArgumentsDescriptions : List<Argument>
        {
        }

        public class OptionsDescriptions : List<Option>
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