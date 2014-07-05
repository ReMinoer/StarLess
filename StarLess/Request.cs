﻿namespace StarLess
{
    internal struct Request
    {
        public string Command { get; set; }
        public string[] Arguments { get; set; }

        public Request(params string[] args)
            : this()
        {
            if (args.Length == 0)
            {
                Command = "";
                Arguments = new string[0];
            }
            else
            {
                Command = args[0];

                Arguments = new string[args.Length - 1];
                for (var i = 0; i < Arguments.Length; i++)
                    Arguments[i] = args[i + 1];
            }
        }

        public Request(string line)
            : this(line.Split(new[] { ' ' }))
        {
        }
    }
}