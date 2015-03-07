using System;

namespace TommasoScalici.MVVMExtensions
{
    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    public sealed class StringValueAttribute : Attribute
    {
        private readonly string value;


        public StringValueAttribute(string value)
        {
            this.value = value;
        }


        public string StringValue { get { return value; } }
    }
}
