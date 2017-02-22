using System;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public class CommandEventArgs : EventArgs
    {
        public CommandEventArgs(object parameter) => Parameter = parameter;


        public object Parameter { get; private set; }
    }


    public class CommandEventArgs<T> : EventArgs
    {
        public CommandEventArgs(T parameter) => Parameter = parameter;


        public T Parameter { get; private set; }
    }
}
