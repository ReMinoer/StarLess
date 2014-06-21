namespace Diese.ConsoleInterface
{
    public struct Argument
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Validator[] Validators { get; set; }

        public Argument(string name, string description, params Validator[] validators)
            : this()
        {
            Name = name;
            Description = description;
            Validators = validators;
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
                result += (coma ? ", " : "") + v.MessageIfUnvalid;
                coma = true;
            }
            return result;
        }
    }
}