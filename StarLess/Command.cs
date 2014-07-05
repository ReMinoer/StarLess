using System.Linq;
using StarLess.Exceptions;

namespace StarLess
{
    // TODO : Check if an optional argument appears twice
    // TODO : Check if an argument is a mistake
    public abstract class Command : AbstractCommand
    {
        public ArgumentsList RequiredArguments { get; private set; }
        public ArgumentsList OptionalArguments { get; private set; }

        public override sealed ArgumentsList Arguments
        {
            get
            {
                var result = new ArgumentsList();
                result.AddRange(RequiredArguments);
                result.AddRange(OptionalArguments);
                return result;
            }
        }

        protected Command(string keyword, string description)
            : base(keyword, description)
        {
            RequiredArguments = new ArgumentsList();
            OptionalArguments = new ArgumentsList();
        }

        protected override sealed void CheckValidity(string[] args, out ArgumentsValues arguments, out OptionsValues options)
        {
            arguments = new ArgumentsValues(Arguments);
            options = new OptionsValues();

            var argsCount = 0;
            for (var i = 0; i < args.Length; i++)
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

            var j = 0;
            var k = 0;
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

                if (j < RequiredArguments.Count)
                {
                    if (!RequiredArguments[j].IsValid(args[i]))
                        throw new ArgumentNotValidException(RequiredArguments[j], j);

                    arguments.Add(RequiredArguments[j].Name, args[i]);
                    j++;
                }
                else
                {
                    if (!OptionalArguments[k].IsValid(args[i]))
                        throw new ArgumentNotValidException(OptionalArguments[k], j + k);

                    arguments.Add(OptionalArguments[k].Name, args[i]);
                    k++;
                }
            }
        }

        public override sealed string CompleteDescription()
        {
            var description = Keyword;

            foreach (var a in RequiredArguments)
                description += " " + a.Name;

            if (OptionalArguments.Any())
            {
                description += " (";
                foreach (var a in OptionalArguments)
                    description += " " + a.Name;
                description += " )";
            }

            if (Options.Any())
            {
                description += " -[";
                foreach (var o in Options)
                    description += " " + o.ShortKey;
                description += " ]";
            }

            description += "\n\nDESCRIPTION : \n";
            description += "\t" + Description + "\n";

            if (RequiredArguments.Any())
                description += "\nARGUMENTS :\n";

            foreach (var a in RequiredArguments)
                description += "\t" + a.Name + " : " + a.Description + "\n";

            if (OptionalArguments.Any())
                description += "\nOPTIONAL ARGUMENTS :\n";

            foreach (var a in OptionalArguments)
                description += "\t" + a.Name + " : " + a.Description + "\n";

            if (Options.Any())
                description += "\nOPTIONS :\n";

            foreach (var o in Options)
                description += "\t-" + o.ShortKey + "/--" + o.LongKey + " : " + o.Description + "\n";

            return description;
        }
    }
}