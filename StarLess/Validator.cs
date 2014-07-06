using System;
using System.ComponentModel;

namespace StarLess
{
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

        static public Validator TryParse(string name, Type type)
        {
            var v = new Validator {Test = s =>
            {
                try
                {
                    TypeDescriptor.GetConverter(type).ConvertFromString(s);
                }
                catch
                {
                    return false;
                }
                return true;
            },
                UnvalidMessage = name + " is not a valid " + type.Name + " !"};
            return v;
        }
    }
}