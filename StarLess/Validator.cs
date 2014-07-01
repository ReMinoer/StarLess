using System;
using System.ComponentModel;

namespace StarLess
{
    // TODO : others default validator
    public struct Validator
    {
        public Func<string, bool> Test { get; set; }
        public string UnvalidMessage { get; set; }

        public Validator(Func<string, bool> test, string unvalidMessage)
            : this()
        {
            Test = test;
            UnvalidMessage = unvalidMessage;
        }

        public static Validator TryParse(string name, Type type)
        {
            Validator v = new Validator();
            v.Test = s =>
            {
                try { TypeDescriptor.GetConverter(type).ConvertFromString(s); }
                catch { return false; }
                return true;
            };
            v.UnvalidMessage = name + " is not a valid " + type.Name + " !";
            return v;
        }
    }
}