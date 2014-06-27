using System.Linq;
using StarLess.Exceptions;

namespace StarLess
{
    public abstract class UnlimitedCommand : AbstractCommand
    {
        protected string _parameterName = "query";

        public UnlimitedCommand(string keyword, string description)
            : base(keyword, description)
        {
        }

        protected override sealed void CheckValidity(string[] args, out ArgumentsValues arguments, out OptionsValues options)
        {
            arguments = new ArgumentsValues();
            options = new OptionsValues();

            int j = 0;
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

                arguments.Add(j.ToString(), args[i]);
                j++;
            }
        }

        public override sealed string CompleteDescription()
        {
            string description = Keyword;

            description += " " + _parameterName + " ...";

            if (Options.Any())
            {
                description += " -[";
                foreach (Option o in Options)
                    description += " " + o.ShortKey;
                description += " ]";
            }

            description += "\n\nDESCRIPTION : \n";
            description += "\t" + Description + "\n";

            if (Options.Any())
                description += "\nOPTIONS :\n";

            foreach (Option o in Options)
                description += "\t-" + o.ShortKey + "/--" + o.LongKey + " : " + o.Description + "\n";

            return description;
        }
    }
}