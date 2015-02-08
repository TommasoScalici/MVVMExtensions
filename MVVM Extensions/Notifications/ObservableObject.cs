using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace TommasoScalici.MVVMExtensions.Notifications
{
    /// <summary>
    /// Simple implementation of the <see cref="INotifyPropertyChanged"/> interface. Extend this class to have a bindable ready to use object. Use <see cref="RaisePropertyChanged(string)"/> and <see cref="Set{T}(ref T, T, string)"/> methods to refresh/notify your properties back to the UI. The class has the <see cref="DataContractAttribute"/> applied so you can also easily serialize objects derived from it.
    /// </summary>
    [DataContract]
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the status (enabled / disabled) of property notification for this object.
        /// </summary>
        public bool IsNotifying { get; set; } = true;

        /// <summary>
        /// Raise the <see cref="PropertyChanged"/> event for the named property.
        /// </summary>
        /// <param name="propertyName">The name of the property of which <see cref="PropertyChanged"/> event will be raised.</param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (IsNotifying)
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raise the <see cref="PropertyChanged"/> event for all properties on the <see cref="ObservableObject"/>.
        /// </summary>
        public void RaiseAllPropertyChanged()
        {
            if (IsNotifying)
            {
                foreach (var property in GetType().GetRuntimeProperties())
                    RaisePropertyChanged(property.Name);
            }
        }

        /// <summary>
        /// Sets a new value for the property and the backing store field raising a PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">The type of the field of which set the new value.</typeparam>
        /// <param name="field">The reference to the field of which set the new value.</param>
        /// <param name="newValue">The new value for the field.</param>
        /// <param name="propertyName">The name of the property of which <see cref="PropertyChanged"/> event will be raised. You don't need to pass it if the method is called in the set method of a property.</param>
        /// <returns></returns>
        protected bool Set<T>(ref T field, T newValue = default(T), [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
    }
}
