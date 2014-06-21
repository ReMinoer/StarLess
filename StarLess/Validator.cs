using System;

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
    }
}