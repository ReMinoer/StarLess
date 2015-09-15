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

        static public Validator TryParse<T>(string name)
        {
            var v = new Validator
            {
                Test = s =>
                {
                    try
                    {
                        TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(s);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                },
                UnvalidMessage = string.Format("{0} is not a valid {1} !", name, typeof(T).Name)
            };
            return v;
        }

        static public Validator ValueMin<T>(string name, T min, bool minExclude = false) where T : IComparable<T>
        {
            var v = new Validator
            {
                Test = s =>
                {
                    var value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(s);

                    if (value == null)
                        return false;

                    return minExclude ? value.CompareTo(min) > 0 : value.CompareTo(min) >= 0;
                },
                UnvalidMessage =
                    string.Format("{0} should be greater than {1} {2} !", name, min, minExclude ? "(exclude)" : "")
            };
            return v;
        }

        static public Validator ValueMax<T>(string name, T max, bool maxExclude = false) where T : IComparable<T>
        {
            var v = new Validator
            {
                Test = s =>
                {
                    var value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(s);

                    if (value == null)
                        return false;

                    return maxExclude ? value.CompareTo(max) < 0 : value.CompareTo(max) <= 0;
                },
                UnvalidMessage =
                    string.Format("{0} should be lower than {1} {2} !", name, max, maxExclude ? "(exclude)" : "")
            };
            return v;
        }

        static public Validator ValueRange<T>(string name, T min, T max, bool minExclude = false,
            bool maxExclude = false) where T : IComparable<T>
        {
            var v = new Validator
            {
                Test = s =>
                {
                    var value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(s);

                    if (value == null)
                        return false;

                    return (minExclude ? value.CompareTo(min) > 0 : value.CompareTo(min) >= 0)
                           && (maxExclude ? value.CompareTo(max) < 0 : value.CompareTo(max) <= 0);
                },
                UnvalidMessage =
                    string.Format("{0} should be between {1} {2} and {3} {4} !", name, min,
                        minExclude ? "(exclude)" : "", max, maxExclude ? "(exclude)" : "")
            };
            return v;
        }
    }
}