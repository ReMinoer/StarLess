using System;
using System.Collections.Generic;

namespace StarLess
{
    public struct Argument
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Description { get; set; }
        public List<Validator> Validators { get; set; }

        public Argument(string name, Type type, string description, params Validator[] validators)
            : this()
        {
            Name = name;
            Type = type;
            Description = description;

            Validators = new List<Validator>();
            Validators.Add(Validator.TryParse(name, type));
            Validators.AddRange(validators);
        }

        public bool isValid(string s)
        {
            bool result = true;
            foreach (Validator v in Validators)
            {
                result = result && v.Test.Invoke(s);
            }
            return result;
        }

        public string getUnvalidMessage()
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