using System;

namespace TommasoScalici.MVVMExtensions
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class StringValueAttribute : Attribute
    {
        readonly string value;


        public StringValueAttribute(string value) => this.value = value;


        public string StringValue => value;
    }
}
