using System;
using System.Collections.Generic;

namespace StarLess
{
    public interface IArgument
    {
        string Name { get; }
        Type Type { get; }
        string Description { get; }
        List<Validator> Validators { get; set; }

        bool IsValid(string s);
        string GetUnvalidMessage();
    }
}