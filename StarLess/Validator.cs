using System;

namespace Diese.ConsoleInterface
{
    public struct Validator
    {
        public Func<string, bool> Test { get; set; }
        public string MessageIfUnvalid { get; set; }

        public Validator(Func<string, bool> test, string msgIfUnvalid)
            : this()
        {
            Test = test;
            MessageIfUnvalid = msgIfUnvalid;
        }
    }
}