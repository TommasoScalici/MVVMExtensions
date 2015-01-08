using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TommasoScalici.MVVMExtensions.Notifications
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the property notification for this object.
        /// </summary>
        public bool IsNotifying { get; set; } = true;

        /// <summary>
        /// Raise the <see cref="PropertyChanged"/> event for the named property.
        /// </summary>
        /// <param name="propertyName">The name of the property of which <see cref="PropertyChanged"/> event will be raised.</param>
        /// <param name="broadcast">If true, it will raise <see cref="PropertyChanged"/> recursively for all <see cref="ObservableObject"/>.</param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null, bool broadcast = false)
        {
            if (IsNotifying)
            {
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

                if (broadcast)
                {
                    foreach (var property in GetType().GetRuntimeProperties())
                        if (property.PropertyType.GetTypeInfo().IsSubclassOf(typeof(ObservableObject)))
                            RaisePropertyChanged(property.Name, broadcast);
                }
            }
        }

        /// <summary>
        /// Raise the <see cref="PropertyChanged"/> event for all properties on this <see cref="ObservableObject"/>.
        /// </summary>
        /// <param name="broadcast">If true, it will raise <see cref="PropertyChanged"/> recursively for all <see cref="ObservableObject"/>.</param>
        public void RaiseAllPropertyChanged(bool broadcast = false)
        {
            if (IsNotifying)
            {
                foreach (var property in GetType().GetRuntimeProperties())
                    RaisePropertyChanged(property.Name, broadcast);
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
