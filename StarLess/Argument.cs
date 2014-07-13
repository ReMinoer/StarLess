using System;
using System.Collections.Generic;
using StarLess.Interfaces;

namespace StarLess
{
    public struct Argument<T> : IArgument
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public string Description { get; private set; }
        public List<Validator> Validators { get; set; }

        public Argument(string name, string description, params Validator[] validators)
            : this()
        {
            Name = name;
            Type = typeof(T);
            Description = description;

            Validators = new List<Validator> {Validator.TryParse<T>(name)};
            Validators.AddRange(validators);
        }

        public bool IsValid(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            bool result = true;
            foreach (Validator v in Validators)
                result = result && v.Test.Invoke(s);
            return result;
        }

        public string GetUnvalidMessage()
        {
            string result = "";
            bool coma = false;
            foreach (Validator v in Validators)
            {
                result += (coma ? ", " : "") + v.UnvalidMessage;
                coma = true;
            }
            return result;
        }
    }
}