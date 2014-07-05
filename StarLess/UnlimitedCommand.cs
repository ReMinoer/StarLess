using System.Globalization;
using System.Linq;
using StarLess.Exceptions;

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
        protected Argument Argument;

        protected UnlimitedCommand(string keyword, string description)
            : base(keyword, description)
        {
        }

        protected override sealed void CheckValidity(string[] args, out ArgumentsValues arguments, out OptionsValues options)
        {
            arguments = new ArgumentsValues(Arguments);
            options = new OptionsValues();

            var j = 0;
            for (var i = 0; i < args.Length; i++)
            {
                if (Options[args[i]].HasValue)
                {
                    var o = Options[args[i]].Value;

                    var optionArgs = new ArgumentsValues(o.Arguments);
                    foreach (var argument in o.Arguments)
                    {
                        i++;
                        var a = argument;
                        if (!a.IsValid(args[i]))
                            throw new ArgumentNotValidException(a, j);

                        optionArgs.Add(a.Name, args[i]);
                    }
                    options.Add(o.ShortKey, optionArgs);
                    continue;
                }

                arguments.Add(j.ToString(CultureInfo.InvariantCulture), args[i]);
                j++;
            }
        }

        public override sealed string CompleteDescription()
        {
            var description = Keyword;

            description += " " + Argument.Name + "...";

            if (Options.Any())
            {
                description += " -[";
                foreach (var o in Options)
                    description += " " + o.ShortKey;
                description += " ]";
            }

            description += "\n\nDESCRIPTION : \n";
            description += "\t" + Description + "\n";

            if (Options.Any())
                description += "\nOPTIONS :\n";

            foreach (var o in Options)
                description += "\t-" + o.ShortKey + "/--" + o.LongKey + " : " + o.Description + "\n";

            return description;
        }
    }
}