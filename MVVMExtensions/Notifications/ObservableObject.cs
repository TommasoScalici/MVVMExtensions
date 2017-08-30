using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

using PropertyChanged;


namespace TommasoScalici.MVVMExtensions.Notifications
{
    [AddINotifyPropertyChangedInterface]
    public abstract class ObservableObject
    {
        Dictionary<string, object> properties = new Dictionary<string, object>();


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// This method manually raise the <see cref="PropertyChanged"></see> event for the named property./>
        /// </summary>
        /// <param name="propertyName">Name of the property.s</param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// This method manually raise the <see cref="PropertyChanged"></see> event for all the properties of this object.
        /// </summary>
        public void RaiseAllPropertyChanged()
        {
            foreach (var property in GetType().GetRuntimeProperties())
                RaisePropertyChanged(property.Name);
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
    }
}
