using System.Globalization;
using System.Linq;
using StarLess.Exceptions;
using StarLess.Interfaces;

namespace StarLess
{
    public abstract class UnlimitedCommand : AbstractCommand
    {
        public override sealed ArgumentsList Arguments
        {
            get
            {
                var result = new ArgumentsList {Argument};
                return result;
            }
        }
        protected IArgument Argument;

        protected UnlimitedCommand(string keyword, string description)
            : base(keyword, description) {}

        protected override sealed void CheckValidity(string[] args, out ArgumentsValues arguments,
                                                     out OptionsValues options)
        {
            var argumentsDictionary = new ArgumentsDictionary();
            var optionsDictionary = new OptionsDictionary();

            int j = 0;
            for (int i = 0; i < args.Length; i++)
            {
                if (Options[args[i]].HasValue)
                {
                    Option o = Options[args[i]].Value;

                    var optionArgs = new ArgumentsDictionary();
                    foreach (IArgument argument in o.Arguments)
                    {
                        i++;
                        IArgument a = argument;
                        if (!a.IsValid(args[i]))
                            throw new ArgumentNotValidException(a, j);

                        optionArgs.Add(a.Name, args[i]);
                    }
                    optionsDictionary.Add(o.Key, new ArgumentsValues(optionArgs));
                    continue;
                }

                argumentsDictionary.Add(j.ToString(CultureInfo.InvariantCulture), args[i]);
                j++;
            }

            arguments = new ArgumentsValues(argumentsDictionary);
            options = new OptionsValues(optionsDictionary);
        }

        public override sealed string CompleteDescription()
        {
            string description = Keyword;

            description += " " + Argument.Name + "...";

            if (Options.Any())
            {
                description += " -[";
                foreach (Option o in Options)
                    description += " " + o.Key.Short;
                description += " ]";
            }

            description += "\n\nDESCRIPTION : \n";
            description += "\t" + Description + "\n";

            if (Options.Any())
                description += "\nOPTIONS :\n";

            foreach (Option o in Options)
                description += string.Format("\t{0}/{1} : {2}\n", o.Key.ShortFormated, o.Key.LongFormated, o.Description);

            return description;
        }
    }
}