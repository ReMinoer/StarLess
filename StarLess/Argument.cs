using System;
using System.Collections.Generic;

namespace StarLess
{
    public struct Argument
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public string Description { get; private set; }
        public List<Validator> Validators { get; set; }

        public Argument(string name, Type type, string description, params Validator[] validators)
            : this()
        {
            Name = name;
            Type = type;
            Description = description;

            Validators = new List<Validator> {Validator.TryParse(name, type)};
            Validators.AddRange(validators);
        }

        public bool IsValid(string s)
        {
            if (s == null) throw new ArgumentNullException("s");
            var result = true;
            foreach (var v in Validators)
            {
                result = result && v.Test.Invoke(s);
            }
            return result;
        }

        public string GetUnvalidMessage()
        {
            var result = "";
            var coma = false;
            foreach (var v in Validators)
            {
                result += (coma ? ", " : "") + v.UnvalidMessage;
                coma = true;
            }
            return result;
        }
    }
}