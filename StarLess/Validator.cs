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

        public static Validator TryParse<T>(string name)
        {
            Validator v = new Validator();
            v.Test = s =>
            {
                try { TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(s); }
                catch { return false; }
                return true;
            };
            v.UnvalidMessage = name + " is not a valid " + typeof(T).Name + " !";
            return v;
        }

        public static Validator TryParseEnum<T>(string name) where T : struct
        {
            Validator v = new Validator();
            v.Test = s =>
                {
                    T result;
                    return Enum.TryParse(s, out result);
                };
            v.UnvalidMessage = name + " is not a valid " + typeof(T).Name + " !";
            return v;
        }
    }
}